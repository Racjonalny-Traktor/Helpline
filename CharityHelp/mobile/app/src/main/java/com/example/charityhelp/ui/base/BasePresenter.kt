package com.example.charityhelp.ui.base

import com.example.charityhelp.data.RetrofitRest
import io.reactivex.disposables.CompositeDisposable

open class BasePresenter {
    protected val compositeDisposable = CompositeDisposable()
    protected val networkingService = RetrofitRest().networkService

    open fun onDestroy(){
        compositeDisposable.dispose()
    }
}