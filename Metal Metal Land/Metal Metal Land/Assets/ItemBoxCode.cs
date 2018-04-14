using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBoxCode : MonoBehaviour {

    float objectFloatSpeed;

    // Use this for initialization
    void Start () {
        objectFloatSpeed = 0.5f;

	}
	
	// Update is called once per frame
	void Update () {
        //make object float down at consistent rate
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-0.5f);

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //if player touches the powerup, activate powerup for them
        if (coll.gameObject.tag.ToLower() == "player")
        {
            coll.gameObject.GetComponent<PlayerPowerupController>().activatePowerup(gameObject.name.Substring(0, 3));

        }//end if

    }//end OnTriggerEnter2D

}
