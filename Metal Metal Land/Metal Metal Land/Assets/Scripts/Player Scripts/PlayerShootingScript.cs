/*
This is a script designed to implement shooting behaviours for the player character.
This includes but is not limited to generate bullets depending on the current gun

*/
using UnityEngine;
using System.Collections;

public class PlayerShootingScript : MonoBehaviour {

    GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void fireBullet(string currentWeapon, float angularOffset, Vector3 positionOffset)
    {
        //position is off
        positionOffset = this.gameObject.transform.GetChild(0).transform.position;

        bullet = (GameObject)Instantiate(Resources.Load("BulletPrefab"), /*this.gameObject.transform.position +*/ positionOffset, Quaternion.Euler(0, 0, angularOffset));
        bullet.GetComponent<BulletScript>().setDir(getCurrentDir());
        bullet.GetComponent<BulletScript>().setTimeToLive(currentWeapon);
    }//end fireBullet

    int getCurrentDir()
    {
        bool isLeft = this.gameObject.GetComponent<PlayerMovement>().getFaceLeft();
        if (isLeft)
        {
            return -1;

        }//end if
        else
        {
            return 1;

        }//end else
    }//end getCurrentDir

}
