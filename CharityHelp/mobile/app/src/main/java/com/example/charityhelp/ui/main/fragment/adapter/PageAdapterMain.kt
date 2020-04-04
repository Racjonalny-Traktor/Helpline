package com.example.charityhelp.ui.main.fragment.adapter

import android.content.Context
import android.graphics.Color
import android.graphics.Typeface
import android.view.LayoutInflater
import android.view.View
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.fragment.app.FragmentPagerAdapter
import com.example.charityhelp.R
import com.example.charityhelp.ui.main.MainActivity
import com.example.charityhelp.ui.main.fragment.home.HomeFragment
import com.example.charityhelp.ui.main.fragment.list.ListFragment
import com.example.charityhelp.ui.main.fragment.map.MapFragment
import com.example.charityhelp.ui.main.fragment.profile.ProfileFragment
import com.mikepenz.google_material_typeface_library.GoogleMaterial
import com.mikepenz.iconics.IconicsDrawable
import com.mikepenz.iconics.typeface.IIcon
import kotlinx.android.synthetic.main.custom_tab.view.*

class PageAdapterMain(fm: FragmentManager, val mainActivity: MainActivity): FragmentPagerAdapter(fm) {

    private val iconSize = 20
    private val tabIcons = ArrayList<IIcon>()

    init {
        tabIcons.add(GoogleMaterial.Icon.gmd_home)
        tabIcons.add(GoogleMaterial.Icon.gmd_list)
        tabIcons.add(GoogleMaterial.Icon.gmd_map)
        tabIcons.add(GoogleMaterial.Icon.gmd_person)
    }


    override fun getItem(position: Int): Fragment {
        when(position){
            0 -> return HomeFragment(mainActivity)
            1 -> return ListFragment(mainActivity)
            2 -> return MapFragment(mainActivity)
            3 -> return ProfileFragment(mainActivity)
        }
        return HomeFragment(mainActivity)
    }

    override fun getCount(): Int {
        return 4
    }

    fun getTabView(position: Int) : View {
        val view = LayoutInflater.from(mainActivity).inflate(R.layout.custom_tab, null)
        view.tabIcon.icon = IconicsDrawable(mainActivity)
            .icon(tabIcons[position])
            .color(mainActivity.resources.getColor(R.color.orange))
            .sizeDp(iconSize)

        when(position){
            0 -> setTabColor(true,view,mainActivity)
            else -> setTabColor(false,view,mainActivity)
        }

        return view
    }

    fun setTabColor(selected: Boolean, view: View, ctx: Context){
        when(selected){
            true -> {
                view.tabIcon.icon.sizeDp(iconSize)
                view.tabIcon.icon.color(mainActivity.resources.getColor(R.color.orange))
            }
            false ->{
                view.tabIcon.icon.sizeDp(iconSize)
                view.tabIcon.icon.color(mainActivity.resources.getColor(R.color.paleBlue))
            }
        }
    }
}