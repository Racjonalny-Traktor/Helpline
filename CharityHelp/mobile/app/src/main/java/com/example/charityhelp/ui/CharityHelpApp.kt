package com.example.charityhelp.ui

import android.app.Application
import android.util.Log
import android.widget.Toast

import com.example.charityhelp.R
import com.google.firebase.messaging.FirebaseMessaging
import com.mapbox.mapboxsdk.Mapbox

class CharityHelpApp: Application() {
    private val TAG = "CharityHelpApp"
    override fun onCreate() {
        super.onCreate()

        Mapbox.getInstance(this, "pk.eyJ1Ijoia3Bla2FsYSIsImEiOiJjazhtODVkaHQwNjU3M2xtZHZnbmF0Zm1mIn0.BsSAe87gNEGucKLM7SACDA")


        FirebaseMessaging.getInstance().subscribeToTopic("help")
            .addOnCompleteListener { task ->
                var msg = "siema"
                if (!task.isSuccessful) {
                    msg = "fail"
                }
                Log.d(TAG, msg)
            }

    }

    override fun onTerminate() {
        super.onTerminate()
    }
}