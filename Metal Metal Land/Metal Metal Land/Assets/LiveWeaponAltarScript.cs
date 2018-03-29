using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiveWeaponAltarScript : MonoBehaviour {

    //load references to all guns
    List<GameObject> weapons = new List<GameObject>();
    GameObject revolver;
    GameObject altarWeapon;
	// Use this for initialization
	void Start () {

        revolver = Resources.Load("Objects/Weapons/OcelotsRevolvers") as GameObject;
        //Instantiate(revolver, transform.position, transform.rotation);
        weapons.Add(revolver);
        //Debug.Log(eapons.Count);

        altarWeapon = revolver;

        SpriteRenderer weaponSprite = GetComponentInChildren<SpriteRenderer>();

        weaponSprite.sprite = revolver.GetComponent<SpriteRenderer>().sprite;

        /*Vector3 childObj = this.transform.GetChild(0).position;
        Destroy(transform.GetChild(0));
        GameObject altarGun = Instantiate(revolver, transform.GetChild(0).position, this.transform.rotation) as GameObject;
        altarGun.transform.parent = this.gameObject.transform;
        
       */

        Debug.Log("I'm awake");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject getAltarWeapon()
    {
        return altarWeapon;

    }//end getAltarWeapon
}
