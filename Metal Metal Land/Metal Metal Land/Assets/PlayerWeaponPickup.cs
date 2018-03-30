﻿using UnityEngine;
using System.Collections;

public class PlayerWeaponPickup : MonoBehaviour {

    bool weaponPickedUp, weaponDroppable;

    GameObject weaponSlot;

    // Use this for initialization
    void Start () {
        weaponPickedUp = false;
        weaponDroppable = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "LiveAltar")
        {
            if (Input.GetKey("m") && !(weaponPickedUp))
            {
                weaponPickedUp = true;
                weaponSlot = transform.GetChild(0).gameObject;
                weaponSlot = Instantiate(collider.GetComponent<LiveWeaponAltarScript>().getAltarWeapon(),transform.GetChild(0).transform.position,transform.rotation) as GameObject;
                weaponSlot.transform.parent = this.transform;

                //reset altar to inert
                collider.GetComponent<LiveWeaponAltarScript>().resetAltar();

                Invoke("allowWeaponDrop", 0.5f);
                

            }//end if

            else if (Input.GetKey("m") && weaponPickedUp && weaponDroppable)
            {
                //throw gun away
                weaponDroppable = false;
                Destroy(weaponSlot.gameObject);

                Invoke("allowWeaponPickUp", 0.5f);

            }//else if 

        }


    }//end onTriggerEnter
    
    void allowWeaponDrop()
    {
        weaponDroppable = true;

    }

    void allowWeaponPickUp()
    {
        weaponPickedUp = false;

    }
}
