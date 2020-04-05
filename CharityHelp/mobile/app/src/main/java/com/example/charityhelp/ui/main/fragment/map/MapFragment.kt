package com.example.charityhelp.ui.main.fragment.map

import android.location.Location
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.charityhelp.R
import com.example.charityhelp.ui.main.MainActivity
import com.mapbox.mapboxsdk.camera.CameraPosition
import com.mapbox.mapboxsdk.camera.CameraUpdate
import com.mapbox.mapboxsdk.camera.CameraUpdateFactory
import com.mapbox.mapboxsdk.geometry.LatLng
import com.mapbox.mapboxsdk.maps.MapboxMap
import com.mapbox.mapboxsdk.maps.Style
import com.mapbox.mapboxsdk.plugins.annotation.Symbol
import com.mapbox.mapboxsdk.plugins.annotation.SymbolManager
import com.mapbox.mapboxsdk.plugins.annotation.SymbolOptions
import kotlinx.android.synthetic.main.fragment_map.view.*
import java.lang.NullPointerException


class MapFragment(val mainActivity: MainActivity) : Fragment() {

    private val TAG = "MapFragment"

    var symbolManager: SymbolManager? = null
    var map: MapboxMap? = null

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        val view = inflater.inflate(R.layout.fragment_map, container, false)

        view.mapView.onCreate(savedInstanceState)
        view.mapView.getMapAsync { mapboxMap ->
            map = mapboxMap
            mapboxMap.setStyle(Style.MAPBOX_STREETS) { style: Style ->
                symbolManager = SymbolManager(view.mapView, mapboxMap, style)
            }
        }

        return view
    }

    fun moveCamera(location: Location){
        val position = CameraPosition.Builder()
            .target(LatLng(location.latitude, location.longitude)) // Sets the new camera position
            .zoom(17.0) // Sets the zoom
            .tilt(30.0) // Set the camera tilt
            .build() // Creates a CameraPosition from the builder

        map?.animateCamera(
            CameraUpdateFactory
                .newCameraPosition(position), 3000
        )
    }

    fun setUserLocation(location: Location) {
        moveCamera(location)


        symbolManager?.let {
            val symbol: Symbol? = it.create(SymbolOptions()
                .withLatLng(LatLng(location.latitude, location.longitude))
                .withIconSize(2.0f))
        }

    }

}
