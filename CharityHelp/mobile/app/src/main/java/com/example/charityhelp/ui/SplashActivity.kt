package com.example.charityhelp.ui

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle

class SplashActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        openMainActivity()
    }

    private fun openMainActivity() {
        startActivity(Intent(this,MainActivity::class.java))
        finish()
    }
}
