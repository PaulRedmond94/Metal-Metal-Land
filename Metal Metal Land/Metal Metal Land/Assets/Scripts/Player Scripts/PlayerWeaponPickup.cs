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

    // Use this for initialization
    void Start()
    {
        //pickupPosition = gameObject.GetComponentInChildren<Transform>().transform.position;
        weaponPickedUp = false;
        weaponDroppable = false;
        actionCanHappen = true;
        assignAxis();

    }

    // Update is called once per frame
    void Update()
    {
        //code to drop weapon
        if (Input.GetAxis(playerPickupAxis)  == 1 && weaponPickedUp == true && weaponDroppable == true && actionCanHappen == true)
        {
            actionCanHappen = false;
            Debug.Log("Drop gun");

            //throw gun away
            Destroy(weaponSlot.gameObject);
            Invoke("allowWeaponPickUp", 2f);
            

        }//else if 

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "LiveAltar")
        {
            if (Input.GetAxis(playerPickupAxis) == 1 && weaponPickedUp == false && actionCanHappen == true)
            {
                actionCanHappen = false;
                Debug.Log("Pickup gun");
                weaponPickedUp = true;
                pickupPosition = gameObject.GetComponentInChildren<Transform>().transform.position;
                weaponSlot = transform.GetChild(0).gameObject;
                GameObject equipItem = collider.GetComponentInChildren<ItemObjectReference>().equipReference;
                weaponSlot = Instantiate(equipItem, transform.GetChild(0).transform.position, transform.rotation) as GameObject;
                //weaponSlot.transform.position = pickupPosition;
                weaponSlot.transform.parent = this.transform;
                
                //reset altar to inert
                collider.GetComponent<LiveWeaponAltarScript>().resetAltar();
                Invoke("allowWeaponDrop", 2f);


            }//end if

        }


    }//end onTriggerEnter

    void allowWeaponDrop()
    {
        weaponDroppable = true;
        Debug.Log("Weapon can now be dropped");
        actionCanHappen = true;

    }

    void allowWeaponPickUp()
    {
        weaponPickedUp = false;
        Debug.Log("Weapon can now be picked up");
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

    /*public void assignAxis(int playerNum)
    {
        if (playerNum == 1)
        {
            playerPickupAxis = "p1_pickup";
            playerFire = "p1_fire";
        }
        else
        {
            playerPickupAxis = "p1_move";
            playerFire = "p1_jump";

        }
    }*/
    public string getFireAxis()
    {
        return playerFire;

    }
}
