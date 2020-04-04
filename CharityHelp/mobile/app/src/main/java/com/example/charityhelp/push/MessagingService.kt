package com.example.charityhelp.push

import android.util.Log
import com.example.charityhelp.data.RetrofitRest
import com.google.firebase.messaging.FirebaseMessagingService
import com.google.firebase.messaging.RemoteMessage
import io.reactivex.disposables.CompositeDisposable

class MessagingService: FirebaseMessagingService() {

    private val TAG = "MessagingService"

    val networkingService = RetrofitRest().networkService

    private val cd = CompositeDisposable()

    override fun onNewToken(token: String) {
        super.onNewToken(token)

        //Sending new token to the server
        cd.add(networkingService.sendToken("zbc",token).subscribe({
            Log.d(TAG, "success!")
        },{t: Throwable ->
            Log.d(TAG, t.toString())
            }
        ))
    }

    override fun onMessageReceived(remoteMessage: RemoteMessage) {
        Log.d(TAG, "From: ${remoteMessage.from}")

        remoteMessage.notification?.let {
            Log.d(TAG, "Message Notification Body: ${it.body}")
        }

    }

}