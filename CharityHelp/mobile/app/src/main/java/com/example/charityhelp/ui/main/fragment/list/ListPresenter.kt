package com.example.charityhelp.ui.main.fragment.list

import com.example.charityhelp.data.model.CallModel
import com.example.charityhelp.data.model.LocationModel
import com.example.charityhelp.ui.base.BasePresenter
import com.example.charityhelp.ui.utils.SchedulerProvider
import kotlinx.android.synthetic.main.fragment_list.*

class ListPresenter(val listFragment: ListFragment): BasePresenter() {

    fun onCreate(){
        compositeDisposable.add(networkingService.getCalls(-1, LocationModel(0.0f,0.0f))
            .subscribeOn(SchedulerProvider.io())
            .observeOn(SchedulerProvider.ui())
            .subscribe({list: ArrayList<CallModel>? ->
                list?.let {
                    listFragment.refresh(it)
                    listFragment.textViewSize?.text = list.size.toString()
                }
            },{t: Throwable? ->

            }))
    }
}