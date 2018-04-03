﻿using UnityEngine;
using System.Collections;

public class rpgFire : MonoBehaviour {

    GameObject rpg;
    Vector2 shootingPosition;
    bool canFire;

	// Use this for initialization
	void Start () {
        rpg = Resources.Load("Objects/Ammo/RpgPrefab") as GameObject;
        canFire = true;

	}//end start
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("n") && this.transform.parent.transform.parent.tag == "Player" && canFire == true)
        {
            shootingPosition = GetComponentInChildren<Transform>().transform.position;
            Debug.Log("Choc's Away Curly");
            fireRocket();
            //Debug.DrawRay(shootingPosition, Vector2.right, Color.green, 1.0f, false);

        }//end if


	}

    void fireRocket()
    {
        canFire = false;
        //if dir is 1, char is facing left, otherwise they're facing right
        int dir = gameObject.GetComponentInParent<PlayerMovement>().getFaceLeft();

        //get Vector2 to determine where to shoot from
        shootingPosition = transform.GetChild(0).transform.position;

        float knockbackMod = gameObject.GetComponentInParent<PlayerMovement>().getKnockbackModifier();
        float knockbackVal = 5.0f * knockbackMod;
        Rigidbody2D playerRigBod2D = gameObject.GetComponentInParent<Rigidbody2D>();

        //instiate rocket
        GameObject firedRocket = Instantiate(rpg, shootingPosition, transform.rotation) as GameObject;

        //set rocket to face right way
        if (dir == -1)
        {
            firedRocket.GetComponent<Transform>().rotation = new Quaternion(0, 180, 0, 0);

        }//end if

        //apply force to bullet
        Rigidbody2D rocketRigBod2d = firedRocket.GetComponent<Rigidbody2D>();
        rocketRigBod2d.AddForce(transform.right * (20), ForceMode2D.Impulse);

        Debug.Log(dir);


        Destroy(firedRocket, 2.0f);

        Invoke("allowFiring",2.0f);

    }//end fireWeapon

    void allowFiring()
    {
        canFire = true;

    }
}
