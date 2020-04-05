package com.example.charityhelp.ui.main.fragment.map

import android.location.Location
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.example.charityhelp.R
import com.example.charityhelp.data.model.CallModel
import com.example.charityhelp.ui.main.MainActivity
import com.mapbox.mapboxsdk.camera.CameraPosition
import com.mapbox.mapboxsdk.camera.CameraUpdateFactory
import com.mapbox.mapboxsdk.geometry.LatLng
import com.mapbox.mapboxsdk.maps.MapboxMap
import com.mapbox.mapboxsdk.maps.Style
import com.mapbox.mapboxsdk.plugins.annotation.Symbol
import com.mapbox.mapboxsdk.plugins.annotation.SymbolManager
import com.mapbox.mapboxsdk.plugins.annotation.SymbolOptions
import kotlinx.android.synthetic.main.fragment_map.view.*
import java.util.ArrayList


class MapFragment(val mainActivity: MainActivity) : Fragment() {

    private val TAG = "MapFragment"

    var symbolManager: SymbolManager? = null
    var map: MapboxMap? = null

    private val MAKI_ICON_CAFE = "cafe-15"
    private val mSymbolsPeople = ArrayList<Symbol>()

    lateinit var mMapPresenter: MapPresenter

    private var needToMoveCamera = true

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

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        mMapPresenter = MapPresenter(this)
        mMapPresenter.onCreate()
    }

    fun moveCamera(location: Location){
        val position = CameraPosition.Builder()
            .target(LatLng(location.latitude, location.longitude)) // Sets the new camera position
            .zoom(17.0) // Sets the zoom
            .tilt(30.0) // Set the camera tilt
            .build() // Creates a CameraPosition from the builder

        map?.let {
            needToMoveCamera = !needToMoveCamera
            it.animateCamera(
                CameraUpdateFactory
                    .newCameraPosition(position), 3000
            )
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        needToMoveCamera = true
    }

    fun setUserLocation(location: Location) {
        if (needToMoveCamera){
            moveCamera(location)
        }

    }

    fun refresh(list: ArrayList<CallModel>) {
        for(symbol in mSymbolsPeople){
            symbolManager?.delete(symbol)
        }
        mSymbolsPeople.clear()
        for (el in list){
            symbolManager?.let {
                val symbol: Symbol? = it.create(SymbolOptions()
                    .withLatLng(LatLng(el.latitude, el.longitude))
                    .withIconImage(MAKI_ICON_CAFE)
                    .withIconSize(2.0f))
                symbol?.let {
                    mSymbolsPeople.add(symbol)
                }
            }
        }
    }

}
