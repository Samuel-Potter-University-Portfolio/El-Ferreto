﻿using UnityEngine;
using System.Collections;
using System;

public class movement : MonoBehaviour {

    private Rigidbody2D body;
    //Stores last y accelerometer states where 0 is the current state
    private float[] jump_state = new float[10];

    public float jump_threshold = 0.2f;
    public float speed = 20f;
    public float jump_height = 10f;

    public bool keyboard_controlled = false;
    private bool touching_ground = false;


    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        body = GetComponent<Rigidbody2D>();
    }

    void Update () 
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 movement;

        if (keyboard_controlled)
        {
            movement = new Vector2(Input.GetAxis("Horizontal"), 0);
        }
        else
        {
            movement = new Vector2(Input.acceleration.x, 0);
            pollJumpStates();
        }
        
        //Jump, if the player has tried to jump and is touching the ground
        if (hasJumped() && touching_ground) 
        {
            jump(body);
        }

        body.velocity += (movement * Time.deltaTime * speed);
    }

    //Shift every value in the array and add current state
    void pollJumpStates() 
    {
        float current_state = Input.acceleration.y;

        for (int i = jump_state.Length-1; i > 0; i--) 
        {
            jump_state[i] = jump_state[i-1];
        }

        jump_state[0] = current_state;
    }

    void jump(Rigidbody2D body) 
    {
        //If the player is on the ground and they jump
        body.velocity+= new Vector2(0f, jump_height);
        touching_ground = false;
    }

    //Has the player tried to jump
    bool hasJumped() 
    {
        if (keyboard_controlled) return Input.GetKeyDown(KeyCode.W);

        if (getJumpMagnitude() >= jump_threshold)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    float getJumpMagnitude() 
    {
        return Math.Abs(jump_state[0] - jump_state[jump_state.Length-1]);    
    }

    void OnCollisionEnter2D(Collision2D collision_object) 
    {
        //If the player is touching buildings, it will be classed as on the ground
        if (collision_object.gameObject.tag == "building") {
            touching_ground = true;
        }
    }

    void OnGUI() 
    {
        GUI.Label(new Rect(10, 10, 1000, 100), "Debug:\n" + body.position + "\n" + Input.acceleration + "\n" + (int)(getJumpMagnitude()*100)/100f);
    }
}