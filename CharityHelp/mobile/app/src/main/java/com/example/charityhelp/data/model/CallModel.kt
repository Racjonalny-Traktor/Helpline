package com.example.charityhelp.data.model

import com.google.gson.annotations.SerializedName

data class CallModel (

    @SerializedName("id") val id : Int,
    @SerializedName("distanceString") val distanceString : String,
    @SerializedName("distance") val distance : Int,
    @SerializedName("latitude") val latitude : Double,
    @SerializedName("longitude") val longitude : Double,
    @SerializedName("address") val address : String,
    @SerializedName("phoneNumber") val phoneNumber : String,
    @SerializedName("createdAt") val createdAt : String
)