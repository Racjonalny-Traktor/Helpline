package com.example.charityhelp.ui.main

import android.content.IntentSender
import android.graphics.Color
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.viewpager.widget.PagerAdapter
import com.example.charityhelp.R
import com.example.charityhelp.data.RetrofitRest
import com.example.charityhelp.ui.base.BaseActivity
import com.example.charityhelp.ui.main.fragment.home.HomeFragment
import com.example.charityhelp.ui.main.fragment.list.ListFragment
import com.example.charityhelp.ui.main.fragment.map.MapFragment
import com.example.charityhelp.ui.main.fragment.profile.ProfileFragment
import com.example.charityhelp.ui.main.fragment.adapter.PageAdapterMain
import com.google.android.gms.common.api.ResolvableApiException
import com.google.android.gms.location.LocationRequest
import com.google.android.gms.location.LocationServices
import com.google.android.gms.location.LocationSettingsRequest
import com.google.android.material.tabs.TabLayout
import io.reactivex.Single
import kotlinx.android.synthetic.main.activity_main.*
import org.jetbrains.anko.doAsync
import org.jetbrains.anko.uiThread


class MainActivity : BaseActivity() {
    private val TAG = "MainActivity"

    lateinit var mHomeFragment: HomeFragment
    lateinit var mListFragment: ListFragment
    lateinit var mMapFragment: MapFragment
    lateinit var mProfileFragment: ProfileFragment

    private lateinit var mFragmentAdapter: PageAdapterMain
    private lateinit var mMainPresenter: MainPresenter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        mMainPresenter = MainPresenter(this)

        initFragments()
        initTabLayout()

        mMainPresenter.onCreate()
    }

    private fun initFragments() {
        mHomeFragment = HomeFragment(this)
        mListFragment = ListFragment(this)
        mMapFragment = MapFragment(this)
        mProfileFragment = ProfileFragment(this)
    }

    override fun onDestroy() {
        super.onDestroy()
        mMainPresenter.onDestroy()
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
        viewPager.currentItem = 2
    }


}
