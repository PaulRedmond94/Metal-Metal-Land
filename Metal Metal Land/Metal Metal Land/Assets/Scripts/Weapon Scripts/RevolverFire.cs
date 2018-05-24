using UnityEngine;
using System.Collections;

public class RevolverFire : MonoBehaviour
{

    GameObject bullet;
    bool canFire;
    Vector2 shootingPosition;
    string fireAxis;

    // Use this for initialization
    void Start()
    {
        bullet = Resources.Load("Objects/Ammo/BulletPrefab") as GameObject;
        canFire = true;
        fireAxis = transform.parent.transform.parent.GetComponent<PlayerWeaponPickup>().getFireAxis();

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(fireAxis) > 0 && this.transform.parent.transform.parent.tag == "Player")
        {
            if (canFire && GetComponentInParent<PlayerGameController>().getAlive())
            {
                fireBullet(0, 1.0f, 0.75f, 0);

            }
            //Debug.DrawRay(, Vector2.right, Color.green, 1.0f, false);

        }//end if


    }//end update

    void fireBullet(float recoilVal, float knockbackVal, float reLoadTime, int bulletDurability)
    {
        //if dir is 1, char is facing left, otherwise they're facing right
        int dir = gameObject.GetComponentInParent<PlayerMovement>().getFaceLeft();

        //get Vector2 to determine where to shoot from
        shootingPosition = transform.GetChild(0).transform.position;

        //disable firing
        canFire = false;

        //calculate knockback
        float knockbackMod = this.gameObject.GetComponentInParent<PlayerMovement>().getKnockbackModifier();
        knockbackVal = knockbackVal * knockbackMod;
        Rigidbody2D playerRigBod2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
        //playerRigBod2d.AddForce(new Vector2((knockbackVal * -dir), knockbackVal * 0.6f), ForceMode2D.Impulse);

        //instantiate bullet
        GameObject firedBullet = Instantiate(bullet, shootingPosition, transform.rotation) as GameObject;

        //set bullet to face right way
        if (dir == -1)
        {
            firedBullet.GetComponent<Transform>().rotation = new Quaternion(0, 180, 0, 0);

        }//end if

        //apply force to bullet
        Rigidbody2D bulletRigBod2d = firedBullet.GetComponent<Rigidbody2D>();
        bulletRigBod2d.AddForce(transform.right * (20), ForceMode2D.Impulse);

        Destroy(firedBullet, 1.0f);

        Invoke("allowFiring", reLoadTime);

    }//end fireBullet

    void allowFiring()
    {
        canFire = true;

    }
}
