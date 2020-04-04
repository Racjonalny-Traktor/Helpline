package com.example.charityhelp.ui.main.fragment.adapter

import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentStatePagerAdapter
import androidx.fragment.app.ListFragment
import com.example.charityhelp.ui.main.MainActivity
import com.example.charityhelp.ui.main.fragment.home.HomeFragment
import com.example.charityhelp.ui.main.fragment.map.MapFragment
import com.example.charityhelp.ui.main.fragment.profile.ProfileFragment

class MainFragmentsAdapter(fm: FragmentManager, val mainActivity: MainActivity): FragmentStatePagerAdapter(fm) {

    private val FRAGMENTS_COUNT = 4

    override fun getItem(position: Int): Fragment {
        return when(position){
            0 -> HomeFragment()
            1 -> ListFragment()
            2 -> MapFragment()
            else -> ProfileFragment()
        }
    }

    override fun getPageTitle(position: Int): CharSequence? {
        return when(position){
            0 -> "Home"
            1 -> "List"
            2 -> "Map"
            else -> "Profile"
        }
    }

    override fun getCount() = FRAGMENTS_COUNT
}