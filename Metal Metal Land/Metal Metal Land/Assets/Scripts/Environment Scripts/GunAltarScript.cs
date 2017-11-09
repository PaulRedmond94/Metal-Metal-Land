/*
    This is a script which is designed to generate guns for the user based on the name of the object. 

*/

using UnityEngine;
using System.Collections;

public class GunAltarScript : MonoBehaviour {

    string strGunOffering;
    float gunTimer;
    float gunRespawnTime;
    bool gunSpawned;
    GameObject gunOffering;

	// Use this for initialization
	void Start () {
        strGunOffering = this.gameObject.name;
        strGunOffering = strGunOffering.Substring(8, this.gameObject.name.Length - 8);
        Debug.Log(strGunOffering);
        //TODO: Insert code to spawn a gun above the gunaltar to show what the altar contains

        /*
        //position is off
        positionOffset = this.gameObject.transform.GetChild(0).transform.position;

        bullet = (GameObject)Instantiate(Resources.Load("BulletPrefab"), /*this.gameObject.transform.position + positionOffset, Quaternion.Euler(0, 0, angularOffset));
        bullet.GetComponent<BulletScript>().setDir(getCurrentDir());
        bullet.GetComponent<BulletScript>().setTimeToLive(currentWeapon);*/
        respawnGun();
        
	}
	
	// Update is called once per frame
	void Update () {
        //respawn gun after set time
        if (!gunSpawned)
        {
            gunTimer += Time.deltaTime;

            if (gunTimer > gunRespawnTime)
            {
                respawnGun();

            }//end if

        }//end if gunSpawned


	}//end update

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag.ToLower() == "player")
        {

            if (Input.GetKey("j"))
            {
                try
                {
                    collision.gameObject.GetComponent<PlayerWeaponScript>().setCurrentWeapon(strGunOffering);
                    Debug.Log("Player has now equipped: " + strGunOffering);
                    Destroy(gunOffering.gameObject);
                    gunSpawned = false;
                }//end try

                catch (MissingComponentException mce)
                {
                    Debug.Log("Not a valid player");
                }//end catch

            }//end if key j is pressed

        }//end if

    }//end onTriggerStay

    void respawnGun()
    {
        //vector3 for placing object slightly above the center of the altar
        Vector3 vertOffset = new Vector3(0, 0.6f);

        gunOffering = (GameObject)Instantiate(Resources.Load("GunStandInPrefab"),this.transform.position + vertOffset, this.transform.rotation);
        gunSpawned = true;
        gunRespawnTime = 5.0f;
        gunTimer = 0.0f;

    }//end respawnGun
}
