package com.example.charityhelp.ui.main.fragment.list

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.charityhelp.R
import com.example.charityhelp.data.model.CallModel
import kotlinx.android.synthetic.main.item_action.view.*

class ListAdapter(val players: ArrayList<CallModel>, val listFragment: ListFragment)
    : RecyclerView.Adapter<ListAdapter.ActionViewHolder>() {
    override fun getItemCount() = players.size

    override fun onBindViewHolder(holder: ActionViewHolder, position: Int) {
        holder.bind(players[position])
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ActionViewHolder {
        val view =  LayoutInflater.from(listFragment.context)
            .inflate(R.layout.item_action,parent,false)
        return ActionViewHolder(view)
    }

    inner class ActionViewHolder(itemView: View): RecyclerView.ViewHolder(itemView) {
        fun bind(callModel: CallModel){
            itemView.textViewDate.text = callModel.createdAt
            if (callModel.createdAt.length > 15){
                itemView.textViewDate.text = callModel.createdAt.substring(11,16)
            }
            itemView.textTitle.text = callModel.address
        }
    }

    fun refresh(list: List<CallModel>) {
        players.clear()
        players.addAll(list)
        notifyDataSetChanged()
    }
}