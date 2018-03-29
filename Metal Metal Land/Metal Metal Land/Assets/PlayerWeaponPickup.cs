using UnityEngine;
using System.Collections;

public class PlayerWeaponPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "LiveAltar")
        {
            if (Input.GetKey("m"))
            {
                Debug.Log("Picked up weapon");
                GameObject weaponSlot = transform.GetChild(0).gameObject;
                weaponSlot = Instantiate(collider.GetComponent<LiveWeaponAltarScript>().getAltarWeapon(),transform.GetChild(0).transform.position,transform.rotation) as GameObject;
                weaponSlot.transform.parent = this.transform;

            }//end if

        }


    }//end onTriggerEnter
}
