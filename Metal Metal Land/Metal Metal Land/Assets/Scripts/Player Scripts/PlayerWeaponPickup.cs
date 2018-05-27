using UnityEngine;
using System.Collections;

/*
    Script which is concerned with controlling players ability to pickup and drop weapons from weapon altars



*/

public class PlayerWeaponPickup : MonoBehaviour
{

    public bool weaponPickedUp;
    bool weaponDroppable, actionCanHappen;

    GameObject weaponSlot;
    Vector2 pickupPosition;
    Vector2 equipPosition;
    string playerPickupAxis, playerFire;

    GameObject uiController;

    // Use this for initialization
    void Start()
    {
        //pickupPosition = gameObject.GetComponentInChildren<Transform>().transform.position;
        weaponPickedUp = false;
        weaponDroppable = false;
        actionCanHappen = true;
        assignAxis();

        uiController = GameObject.FindGameObjectWithTag("InGameLiveUI");

    }

    // Update is called once per frame
    void Update()
    {
        //code to drop weapon
        if (Input.GetAxis(playerPickupAxis)  == 1 && weaponPickedUp == true && weaponDroppable == true && actionCanHappen == true)
        {
            actionCanHappen = false;
            updateUI("Nothing");

            //throw gun away
            Destroy(weaponSlot.gameObject);
            Invoke("allowWeaponPickUp", 0.5f);
            

        }//else if 

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "LiveAltar")
        {
            //if player can pick up a weapon
            if (Input.GetAxis(playerPickupAxis) == 1 && weaponPickedUp == false && actionCanHappen == true)
            {
                //stop user from automatically dropping weapon due to frame count
                actionCanHappen = false;
                weaponPickedUp = true;
                //get reference to sprites hand
                weaponSlot = transform.GetChild(0).gameObject;
                GameObject equipItem = collider.GetComponentInChildren<ItemObjectReference>().equipReference;
                //get name of weapon and update UI as needed
                string equipName = equipItem.name.Replace("Clone", "");
                updateUI(equipName);
                //spawn weapon in hand and assign it as a child to this object
                weaponSlot = Instantiate(equipItem, weaponSlot.transform.position, transform.rotation) as GameObject;
                weaponSlot.transform.parent = this.transform;
                //reset altar to inert
                collider.GetComponent<LiveWeaponAltarScript>().resetAltar();
                Invoke("allowWeaponDrop", 0.5f);

            }//end if player input

        }//end if collider

    }//end onTriggerStay

    void allowWeaponDrop()
    {
        weaponDroppable = true;
        actionCanHappen = true;

    }

    void allowWeaponPickUp()
    {
        weaponPickedUp = false;
        actionCanHappen = true;

    }

    public void assignAxis()
    {
        if (gameObject.name == StaticScript.player1Character + "(Clone)")
        {
            playerPickupAxis = "p1_pickup";
            playerFire = "p1_fire";
        }
        else
        {
            playerPickupAxis = "p2_pickup";
            playerFire = "p2_fire";

        }

    }

    public string getFireAxis()
    {
        return playerFire;

    }

    void updateUI(string weapon)
    {
        string playerObjectName = gameObject.name.Replace("(Clone)","");
        uiController.GetComponent<InGameUIController>().updateWeapon(playerObjectName, weapon);

    }

}
