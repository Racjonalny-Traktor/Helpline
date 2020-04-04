package com.example.charityhelp.data.networking

import io.reactivex.Completable
import io.reactivex.Single
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface Routes {

    @POST("group/join")
    fun sendToken(@Header("X-Token") token: String, @Body newToken: String): Completable
}