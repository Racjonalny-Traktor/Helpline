package com.example.charityhelp.ui

import android.app.Application
import android.util.Log
import android.widget.Toast

import com.example.charityhelp.R
import com.google.firebase.messaging.FirebaseMessaging

class CharityHelpApp: Application() {
    private val TAG = "CharityHelpApp"
    override fun onCreate() {
        super.onCreate()

        FirebaseMessaging.getInstance().subscribeToTopic("help")
            .addOnCompleteListener { task ->
                var msg = "siema"
                if (!task.isSuccessful) {
                    msg = "fail"
                }
                Log.d(TAG, msg)
                Toast.makeText(baseContext, msg, Toast.LENGTH_SHORT).show()
            }

    }

    override fun onTerminate() {
        super.onTerminate()
    }
}