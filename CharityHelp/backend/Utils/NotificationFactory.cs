using System;
using System.Threading.Tasks;
using charity.Controllers;
using charity.Models;
using charity.Utils;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace charity
{
    public static class NotificationFactory
    {
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

        public static Task<string> NotifyAsync(Message message)
        {
            //ensure high priority & ttl
            message.Android = _androidOptions;
            return Messaging.SendAsync(message);
        }

        public static Task<string> NotifyAboutHelpAsync(NearestCall help)
        {
            return NotifyAsync(new Message
            {
                Notification =
                {
                    Title = "There's a person who needs your help!",
                    Body = $"Hi! There is an elderly person asking for your help. Only {help.DistanceString} away."
                }
            });
        }
      
    }
}