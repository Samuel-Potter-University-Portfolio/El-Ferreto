﻿using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//Variable for enabling/disabling pause menu
	public GameObject GameMenu;

	private bool paused = false;
	
	void Start(){
	
		GameMenu.SetActive (true);
		Time.timeScale = 1;

	}

	public void Restart(){

		GameMenu.SetActive (false);
		Application.LoadLevel("default"); //Game loads the first scene

    }

    public void goToShop()
    {
        Application.LoadLevel("shop"); 
    }



    public void Exit(){

		Application.Quit ();

	}
}
