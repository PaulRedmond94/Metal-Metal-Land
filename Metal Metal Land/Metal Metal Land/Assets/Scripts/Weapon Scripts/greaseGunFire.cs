using UnityEngine;
using System.Collections;

public class greaseGunFire : MonoBehaviour {

    GameObject bullet;
    bool canFire;
    float bulletSpread;
    Vector2 shootingPosition;
    string fireAxis;

    // Use this for initialization
    void Start()
    {
        bullet = Resources.Load("Objects/Ammo/BulletPrefab") as GameObject;
        canFire = true;
        fireAxis = transform.parent.transform.parent.GetComponent<PlayerWeaponPickup>().getFireAxis();
        bulletSpread = 10.0f;

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(fireAxis) == 1
            && this.transform.parent.transform.parent.tag == "Player"
            && GetComponentInParent<PlayerGameController>().getAlive())
        {
            if (canFire)
            {
                //fireBullet(0, 1.0f, 0.25f, 0);

                //if dir is 1, char is facing left, otherwise they're facing right
                int dir = gameObject.GetComponentInParent<PlayerMovement>().getFaceLeft();

                //get Vector2 to determine where to shoot from
                shootingPosition = transform.GetChild(0).transform.position;

                //disable firing
                canFire = false;

                //calculate knockback
                /*float knockbackMod = this.gameObject.GetComponentInParent<PlayerMovement>().getKnockbackModifier();
                knockbackVal = knockbackVal * knockbackMod;
                Rigidbody2D playerRigBod2d = this.gameObject.GetComponentInParent<Rigidbody2D>();
                //playerRigBod2d.AddForce(new Vector2((knockbackVal * -dir), knockbackVal * 0.6f), ForceMode2D.Impulse);
                */
                //instantiate bullet
                float angularOffset = (Random.Range(-bulletSpread, bulletSpread) * 0.8f);
                if (dir == -1)
                {
                    angularOffset = 180 - angularOffset;

                }//end if
                GameObject firedBullet = Instantiate(bullet, shootingPosition, Quaternion.Euler(0, 0, angularOffset)) as GameObject;

                Debug.Log(dir);


                Destroy(firedBullet, 0.75f);

                Invoke("allowFiring", 0.2f);


            }
            //Debug.DrawRay(, Vector2.right, Color.green, 1.0f, false);

        }//end if


    }//end update

    void fireBullet(float recoilVal, float knockbackVal, float reLoadTime, int bulletDurability)
    {


    }//end fireBullet

    void allowFiring()
    {
        canFire = true;

    }
}
