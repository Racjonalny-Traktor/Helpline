package com.example.charityhelp.ui.main.fragment.list

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

import com.example.charityhelp.R
import com.example.charityhelp.data.model.CallModel
import com.example.charityhelp.ui.main.MainActivity
import kotlinx.android.synthetic.main.fragment_list.*


class ListFragment(val mainActivity: MainActivity) : Fragment() {

    lateinit var mActionsListAdapter: ListAdapter
    lateinit var mListPresenter: ListPresenter

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? {
        return inflater.inflate(R.layout.fragment_list, container, false)
    }

    private fun initActionsList(){
        mActionsListAdapter = ListAdapter(ArrayList(),this)
        recyclerListActions.adapter = mActionsListAdapter
        recyclerListActions.layoutManager = LinearLayoutManager(this.context, RecyclerView.VERTICAL,false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        initActionsList()

        mListPresenter = ListPresenter(this)
        mListPresenter.onCreate()
    }

    override fun onDestroyView() {
        super.onDestroyView()
        mListPresenter.onDestroy()
    }

    fun refresh(list: List<CallModel>) {
        mActionsListAdapter.refresh(list)
    }
}
