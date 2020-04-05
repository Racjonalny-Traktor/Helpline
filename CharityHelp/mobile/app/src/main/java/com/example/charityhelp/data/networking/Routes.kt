package com.example.charityhelp.data.networking

import android.location.Location
import com.example.charityhelp.data.model.CallModel
import com.example.charityhelp.data.model.LocationModel
import io.reactivex.Completable
import io.reactivex.Observable
import io.reactivex.Single
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST
import retrofit2.http.Path

interface Routes {

    @POST("group/join")
    fun sendToken(@Header("X-Token") token: String, @Body newToken: String): Completable

    @POST("Calls/{maxDistanceInKm}")
    fun getCalls(@Path("maxDistanceInKm") maxDistanceInKm: Int, @Body location: LocationModel)
            : Single<ArrayList<CallModel>>
}