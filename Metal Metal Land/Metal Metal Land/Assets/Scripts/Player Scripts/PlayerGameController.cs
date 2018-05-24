﻿using UnityEngine;
using System.Collections;

//script which is used to control the players general game details and overall behaviour
public class PlayerGameController : MonoBehaviour {

    int playerNumber;
    bool alive;
    GameObject deathPointVert;
    public BoxCollider2D headPoint;

	// Use this for initialization
	void Awake () {
        deathPointVert = GameObject.FindGameObjectWithTag("BoundaryPoint");

	}//end awake

    void Start()
    {
        alive = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<PlayerWeaponPickup>().enabled = true;

    }//end start
	
	// Update is called once per frame
	void Update () {
	    if(alive == false)
        {
            transform.eulerAngles = (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f));
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerWeaponPickup>().enabled = false;
            gameObject.GetComponentInChildren<GroundDetector>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().changeAnimationState(0);
        }

        if(Time.frameCount% 60 == 0 && alive)
        {
            if(transform.position.y < deathPointVert.transform.position.y)
            {
                
                killPlayer();
                

            }

        }//end time

	}

    public void killPlayer()
    {
        alive = false;
        
    }//end kill

    public bool getAlive()
    {
        return alive;

    }//end getAlive

    public void setAlive(bool alive)
    {
        this.alive = alive;

    }

    public int getPlayerNumber()
    {
        return playerNumber;

    }//end int

    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;

    }

}
