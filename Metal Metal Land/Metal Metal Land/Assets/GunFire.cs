using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {

    GameObject bullet;
    bool canFire;

	// Use this for initialization
	void Start () {
        bullet = Resources.Load("Objects/Ammo/BulletPrefab") as GameObject;
        canFire = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(this.transform.parent!= null)
        {
            if (Input.GetKey("n") && this.transform.parent.tag == "Player")
            {
                fireWeapon();
                //Debug.DrawRay(, Vector2.right, Color.green, 1.0f, false);
                


            }//end if

        }//end if
        

	}//end update

    void fireWeapon()
    {
        if(tag.ToLower() == "revolver")
        {
            //fire revolver blast
            //Instantiate(bullet, GetComponentInChildren<Transform>().transform.position, GetComponentInChildren<Transform>().transform.rotation);
            Debug.Log("Revolver fired");
            //function to handle recoil, knockback, fireability, bullet durability
            //fireBullet();
            if (canFire)
            {
                fireBullet(0,0.1f,0.25f,0);

            }


        }//end if

        else if(tag.ToLower() == "pistol")
        {


        }//fire pistol blast

    }//end fireWeapon

    void fireBullet(float recoilVal, float knockbackVal, float reLoadTime, int bulletDurability)
    {
        //disable firing
        allowFiring();
        float knockbackMod = this.gameObject.GetComponentInParent<PlayerMovement>().getKnockbackModifier();
        knockbackVal= knockbackVal * knockbackMod;
        Rigidbody2D playerRigBod2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
        playerRigBod2d.AddForce(new Vector2((knockbackVal * -(this.gameObject.GetComponentInParent<PlayerMovement>().getFaceLeft())), knockbackVal * 0.4f), ForceMode2D.Impulse);

        
        Invoke("allowFiring", reLoadTime);

    }//end fireBullet

    void allowFiring()
    {
        canFire = true;

    }
}
