package com.example.charityhelp.ui.main

import android.graphics.Color
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.viewpager.widget.PagerAdapter
import com.example.charityhelp.R
import com.example.charityhelp.ui.main.fragment.home.HomeFragment
import com.example.charityhelp.ui.main.fragment.list.ListFragment
import com.example.charityhelp.ui.main.fragment.map.MapFragment
import com.example.charityhelp.ui.main.fragment.profile.ProfileFragment
import com.example.charityhelp.ui.main.fragment.adapter.PageAdapterMain
import com.google.android.material.tabs.TabLayout
import kotlinx.android.synthetic.main.activity_main.*


class MainActivity : AppCompatActivity() {
    private val TAG = "MainActivity"

    private lateinit var mHomeFragment: HomeFragment
    private lateinit var mListFragment: ListFragment
    private lateinit var mMapFragment: MapFragment
    private lateinit var mProfileFragment: ProfileFragment

    private lateinit var mFragmentAdapter: PageAdapterMain

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        initTabLayout()
    }

    private fun initTabLayout() {
        mFragmentAdapter = PageAdapterMain(supportFragmentManager,this)
        viewPager.adapter = mFragmentAdapter
        tabLayout.setupWithViewPager(viewPager)
        tabLayout.setSelectedTabIndicatorColor(Color.TRANSPARENT)

        for (i in 0..tabLayout.tabCount-1) {
            val tab = tabLayout.getTabAt(i)
            tab!!.customView = mFragmentAdapter.getTabView(i)
        }

        tabLayout.addOnTabSelectedListener(object: TabLayout.OnTabSelectedListener{
            override fun onTabReselected(tab: TabLayout.Tab?) {
            }
            override fun onTabUnselected(tab: TabLayout.Tab?) {
                mFragmentAdapter.setTabColor(false,tab!!.customView!!,applicationContext)
            }
            override fun onTabSelected(tab: TabLayout.Tab?) {
                mFragmentAdapter.setTabColor(true,tab!!.customView!!,applicationContext)
            }
        })
        viewPager.currentItem = 0
    }
}
