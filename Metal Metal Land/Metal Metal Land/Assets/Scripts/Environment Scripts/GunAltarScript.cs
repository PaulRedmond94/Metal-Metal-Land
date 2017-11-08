using UnityEngine;
using System.Collections;

public class GunAltarScript : MonoBehaviour {

    string gunOffering;

	// Use this for initialization
	void Start () {
        gunOffering = this.gameObject.name;
        gunOffering = gunOffering.Substring(8, this.gameObject.name.Length - 8);
        Debug.Log(gunOffering);
        //TODO: Insert code to spawn a gun above the gunaltar to show what the altar contains
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}//end update

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.ToLower() == "player")
        {

            if (Input.GetKeyDown("j"))
            {
                try
                {
                    collision.gameObject.GetComponent<PlayerWeaponScript>().setCurrentWeapon(gunOffering);
                    Debug.Log("Player has now equipped: " + gunOffering);
                }//end try

                catch (MissingComponentException mce)
                {
                    Debug.Log("Not a valid player");
                }//end catch

            }//end if key j is pressed



        }//end if

    }//end onTriggerStay
}
