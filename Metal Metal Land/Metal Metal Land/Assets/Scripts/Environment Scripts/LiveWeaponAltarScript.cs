﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiveWeaponAltarScript : MonoBehaviour {

    //load references to all guns
    List<GameObject> weapons = new List<GameObject>();
    GameObject revolver;
    GameObject rpg;
    GameObject sniper;
    GameObject shotgun;
    GameObject grenade;
    GameObject altarWeapon;
    GameObject altarItem;
    GameObject inertAltar;
	// Use this for initialization
	void Start () {

        revolver = Resources.Load("Objects/Weapons/Spawns/RevolversItem") as GameObject;
        rpg = Resources.Load("Objects/Weapons/Spawns/RpgItem") as GameObject;
        sniper = Resources.Load("Objects/Weapons/Spawns/SniperItem") as GameObject;
        shotgun = Resources.Load("Objects/Weapons/Spawns/shotgunItem") as GameObject;
        grenade = Resources.Load("Objects/Weapons/Spawns/grenadeItem") as GameObject;

        weapons.Add(revolver);
        //Debug.Log(eapons.Count);


        //decide what weapon the altar will use
        //altarWeapon = revolver;
        //altarWeapon = rpg;
        //altarWeapon = sniper;
        //altarWeapon = shotgun;
        altarWeapon = grenade;

        inertAltar = Resources.Load("Objects/waltar_inert") as GameObject;

        SpriteRenderer weaponSprite = altarWeapon.gameObject.GetComponent<SpriteRenderer>();

        //weaponSprite.sprite = revolver.GetComponent<SpriteRenderer>().sprite;
        //weaponSprite.sprite = rpg.GetComponent<SpriteRenderer>().sprite;
        altarItem = Instantiate(altarWeapon, transform.position, transform.rotation) as GameObject;
        altarItem.transform.parent = gameObject.transform;

        /*Vector3 childObj = this.transform.GetChild(0).position;
        Destroy(transform.GetChild(0));
        GameObject altarGun = Instantiate(revolver, transform.GetChild(0).position, this.transform.rotation) as GameObject;
        altarGun.transform.parent = this.gameObject.transform;
        
       */
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void resetAltar()
    {
        Instantiate(inertAltar, transform.position, transform.rotation);
        Destroy(gameObject);

    }//end resetAltar

    public GameObject getAltarWeapon()
    {
        return altarWeapon;

    }//end getAltarWeapon

    public void destroyAltar()
    {


    }

}