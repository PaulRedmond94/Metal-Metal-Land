﻿using UnityEngine;
using System.Collections;

public class SpikeCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag.ToLower() == "player")
        {
            Debug.Log("You is dead");

        }

    }//end onCollisionEnter2D

}