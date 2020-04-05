package com.example.charityhelp.ui.base

import android.content.Intent
import android.content.IntentSender
import android.net.Uri
import android.os.Bundle
import android.os.PersistableBundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import com.example.charityhelp.R
import com.example.charityhelp.ui.utils.PermissionsHelper
import com.google.android.gms.common.api.ResolvableApiException
import com.google.android.gms.location.LocationRequest
import com.google.android.gms.location.LocationServices
import com.google.android.gms.location.LocationSettingsRequest
import io.reactivex.Single
import kotlinx.android.synthetic.main.activity_main.*
import org.jetbrains.anko.doAsync
import org.jetbrains.anko.uiThread

open class BaseActivity: AppCompatActivity() {

    private val REQUEST_CHECK_SETTINGS = 1905
    private var mPermissionStatus = false
    private val TAG = "MainActivity"

    private val mPermissionsHelper = PermissionsHelper(this)

    fun checkLocationSettings(): Single<String> {
        return Single.create { emitter ->
            val builder = LocationSettingsRequest.Builder()
                .addLocationRequest(LocationRequest().apply {
                    interval = 1000
                    fastestInterval = 1000
                    priority = LocationRequest.PRIORITY_HIGH_ACCURACY
                })


            val settingsClient = LocationServices.getSettingsClient(this)
            val taskCheckSettings = settingsClient.checkLocationSettings(builder.build())


            taskCheckSettings.addOnSuccessListener{
                Log.d(TAG, "OnSuccessListener")
                emitter.onSuccess("success")
            }

            taskCheckSettings.addOnFailureListener { exception ->
                if (exception is ResolvableApiException){
                    Log.d(TAG,"OnFailureListener")
                    try {
                        exception.startResolutionForResult(this, REQUEST_CHECK_SETTINGS)
                        doAsync {
                            while (!mPermissionStatus){
                                Thread.sleep(100)
                            }
                            uiThread {
                                emitter.onSuccess("success")
                            }
                        }
                    } catch (sendEx: IntentSender.SendIntentException) {
                    }
                }
            }
        }

    }

    override fun onCreate(savedInstanceState: Bundle?, persistentState: PersistableBundle?) {
        super.onCreate(savedInstanceState, persistentState)
    }

    fun checkPermission(permission: String): Single<String> {
        return mPermissionsHelper.checkPermission(permission)
    }

}