using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using charity.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace charity.FCM
{
    public static class NotificationFactory
    {
        public static Task<string> NotifyAsync(Message message)
        {
            //ensure high priority & ttl
            message.Android = _androidOptions;
            return Messaging.SendAsync(message);
        }

        public static Task<string> NotifyAboutHelpAsync(NearestCall help, string deviceId = null)
        {
            return NotifyAsync(new Message
            {
                Notification = new Notification
                {
                    Title = "There's a person who needs your help!",
                    Body = $"Hi! There is an elderly person asking for your help. Only {help.DistanceString} away."
                },
                Topic = "help",
                Token = deviceId,
                Data = new Dictionary<string, string>
                {
                    {nameof(help.Distance), help.Distance.ToString("F1")},
                    {nameof(help.DistanceString), help.DistanceString},
                    {nameof(help.Id), help.Id.ToString()}
                }
            });
        }

        public static async Task NotifyAssignedVolunteerFirst(NearestCall help, string deviceId)
        {
            if (deviceId == null)
            {
                await NotifyAboutHelpAsync(help);
                return;
            }

            var notifyOthersDelay = TimeSpan.FromMinutes(5);

            await NotifyAboutHelpAsync(help, deviceId);
            DelayedNotificationQueue.Enqueue(new NotifyQueueCall
            {
                Call = help,
                CallAt = help.CreatedAt + notifyOthersDelay
            });
        }



        #region notify queue

        private static ConcurrentQueue<NotifyQueueCall> _notifyQueue;

        public static ConcurrentQueue<NotifyQueueCall> DelayedNotificationQueue =>
            _notifyQueue ??= new ConcurrentQueue<NotifyQueueCall>();


        public class NotifyQueueCall
        {
            public NearestCall Call { get; set; }
            public DateTime CallAt { get; set; }
        }

        #endregion

        #region firebase fcm

        private const string _projectId = "charity-help-797fd";

        private static readonly AndroidConfig _androidOptions = new AndroidConfig
        {
            Priority = Priority.High,
            TimeToLive = TimeSpan.FromHours(1)
        };

        private static FirebaseApp _app;

        public static FirebaseApp App => _app ??= FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("fcm.json"),
            ProjectId = _projectId
        }, _projectId);

        public static FirebaseMessaging Messaging => FirebaseMessaging.GetMessaging(App);

        #endregion
    }
}