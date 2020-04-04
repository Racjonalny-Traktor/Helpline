package com.example.charityhelp.ui.main

import android.Manifest
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.charityhelp.R
import com.example.charityhelp.ui.main.fragment.HomeFragment
import com.example.charityhelp.ui.main.fragment.ListFragment
import com.example.charityhelp.ui.main.fragment.MapFragment
import com.example.charityhelp.ui.main.fragment.ProfileFragment
import com.example.charityhelp.ui.main.fragment.adapter.MainFragmentsAdapter
import com.karumi.dexter.Dexter
import com.karumi.dexter.PermissionToken
import com.karumi.dexter.listener.PermissionDeniedResponse
import com.karumi.dexter.listener.PermissionGrantedResponse
import com.karumi.dexter.listener.PermissionRequest
import com.karumi.dexter.listener.single.PermissionListener
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
