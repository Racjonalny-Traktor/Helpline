package com.example.charityhelp.ui.main

import android.Manifest
import android.location.Location
import android.util.Log
import com.example.charityhelp.ui.base.BasePresenter
import com.example.charityhelp.ui.main.MainActivity
import com.example.charityhelp.ui.utils.LocationProvider

class MainPresenter(private val mView: MainActivity): BasePresenter(){
    private val mLocationProvider = LocationProvider(mView)

    private val TAG = "MainPresenter"

    fun onCreate(){
        compositeDisposable.add(mView.checkPermission(Manifest.permission.ACCESS_FINE_LOCATION)
            .flatMap { t: String -> mView.checkLocationSettings() }
            .subscribe({t: String? ->
                observeLocation()
            },{t: Throwable? ->
                Log.d(TAG, t.toString())
            }))
    }

    private fun observeLocation(){
        compositeDisposable.add(mLocationProvider.observeLocation()
            .subscribe({location: Location? ->
                Log.d(TAG, "new locatiob")
                location?.let {
                    mView.mMapFragment.setUserLocation(location)
                }
            }, {t: Throwable? ->
                Log.d(TAG, t.toString())
            }))
    }

    fun onDestroy(){
        mLocationProvider.stopObservingLocation()
    }
}