using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {

    GameObject bullet;
    bool canFire;
    Vector2 shootingPosition;

	// Use this for initialization
	void Start () {
        bullet = Resources.Load("Objects/Ammo/BulletPrefab") as GameObject;
        canFire = true;        
        
	}//end start
	
	// Update is called once per frame
	void Update () {
        if(this.transform.parent!= null)
        {
            if (Input.GetKey("n") && this.transform.parent.transform.parent.tag == "Player")
            {
                fireWeapon();
                //Debug.DrawRay(, Vector2.right, Color.green, 1.0f, false);

            }//end if

        }//end if
        

	}//end update

    void fireWeapon()
    {
        int gunAmmo;

        if(tag.ToLower() == "revolver")
        {
            //fire revolver blast
            //Instantiate(bullet, GetComponentInChildren<Transform>().transform.position, GetComponentInChildren<Transform>().transform.rotation);
            
            //function to handle recoil, knockback, fireability, bullet durability
            //fireBullet();
            if (canFire)
            {
                fireBullet(0,1.0f,0.25f,0);

            }
       

        }//end if

        else if(tag.ToLower() == "pistol")
        {


        }//fire pistol blast

        else if (tag.ToLower() == "m16")
        {


        }//fire m16 blast

        else if (tag.ToLower() == "shotgun")
        {


        }//fire shotgun blast

        else if (tag.ToLower() == "rpg")
        {
            fireRpg(5.0f);

        }       

    }//end fireWeapon

    void fireBullet(float recoilVal, float knockbackVal, float reLoadTime, int bulletDurability)
    {
        //if dir is 1, char is facing left, otherwise they're facing right
        int dir = gameObject.GetComponentInParent<PlayerMovement>().getFaceLeft();

        //get Vector2 to determine where to shoot from
        shootingPosition = transform.GetChild(0).transform.position;

        //disable firing
        canFire = false;
        float knockbackMod = this.gameObject.GetComponentInParent<PlayerMovement>().getKnockbackModifier();
        knockbackVal= knockbackVal * knockbackMod;
        Rigidbody2D playerRigBod2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
        //playerRigBod2d.AddForce(new Vector2((knockbackVal * -dir), knockbackVal * 0.6f), ForceMode2D.Impulse);

        //instantiate bullet
        GameObject firedBullet = Instantiate(bullet, shootingPosition, transform.rotation) as GameObject;

        //set bullet to face right way
        if(dir == -1)
        {
            firedBullet.GetComponent<Transform>().rotation = new Quaternion(0, 180, 0, 0);

        }//end if

        //apply force to bullet
        Rigidbody2D bulletRigBod2d = firedBullet.GetComponent<Rigidbody2D>();
        bulletRigBod2d.AddForce(transform.right * (20), ForceMode2D.Impulse);

        Debug.Log(dir);


        Destroy(firedBullet, 2.0f);

        Invoke("allowFiring", reLoadTime);

    }//end fireBullet

    void fireRpg(float knockbackVal)
    {
        

    }//end fireRpg

    void allowFiring()
    {
        canFire = true;

    }
}
