﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

     public LevelManager levelManager;

	// Use this for initialization
	void Start () {
        
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onTriggerEnter2D(Collider2D other)
    {
        if(other.name == "ferret")
        {
            Debug.Log("Activated Checkpoint " + transform.position);
        }
    }
}


