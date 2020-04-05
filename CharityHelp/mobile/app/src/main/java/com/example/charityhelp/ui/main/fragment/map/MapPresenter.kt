package com.example.charityhelp.ui.main.fragment.map

import com.example.charityhelp.data.model.CallModel
import com.example.charityhelp.data.model.LocationModel
import com.example.charityhelp.ui.base.BasePresenter
import com.example.charityhelp.ui.main.fragment.list.ListFragment
import com.example.charityhelp.ui.utils.SchedulerProvider
import kotlinx.android.synthetic.main.fragment_list.*

class MapPresenter(val mapFragment: MapFragment): BasePresenter() {
    fun onCreate(){
        compositeDisposable.add(networkingService.getCalls(-1, LocationModel(0.0f,0.0f))
            .subscribeOn(SchedulerProvider.io())
            .observeOn(SchedulerProvider.ui())
            .subscribe({list: ArrayList<CallModel>? ->
                list?.let {
                    mapFragment.refresh(it)
                }
            },{t: Throwable? ->

            }))
    }
}