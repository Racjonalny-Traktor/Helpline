package com.example.charityhelp.ui.main

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.charityhelp.R
import com.example.charityhelp.ui.main.fragment.home.HomeFragment
import com.example.charityhelp.ui.main.fragment.list.ListFragment
import com.example.charityhelp.ui.main.fragment.map.MapFragment
import com.example.charityhelp.ui.main.fragment.profile.ProfileFragment
import com.example.charityhelp.ui.main.fragment.adapter.MainFragmentsAdapter
import kotlinx.android.synthetic.main.activity_main.*


class MainActivity : AppCompatActivity() {
    private val TAG = "MainActivity"

    private lateinit var mHomeFragment: HomeFragment
    private lateinit var mListFragment: ListFragment
    private lateinit var mMapFragment: MapFragment
    private lateinit var mProfileFragment: ProfileFragment

    private lateinit var mFragmentAdapter: MainFragmentsAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        initTabLayout()
    }

    private fun initTabLayout() {
        mFragmentAdapter = MainFragmentsAdapter(supportFragmentManager,this)
        viewPager.adapter = mFragmentAdapter
        tabLayout.setupWithViewPager(viewPager)
    }
}
