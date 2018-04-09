using UnityEngine;
using System.Collections;

public class ShotgunFire : MonoBehaviour
{

    GameObject shotgunPellet;
    bool canFire;
    Vector2 shootingPosition;
    float shotgunSpread;
    int pelletCount;
    string fireAxis;

    // Use this for initialization
    void Start()
    {
        shotgunPellet = Resources.Load("Objects/Ammo/BuckshotPrefab") as GameObject;
        shotgunSpread = 50.0f;
        canFire = true;
        pelletCount = 5;
        fireAxis = transform.parent.transform.parent.GetComponent<PlayerWeaponPickup>().getFireAxis();

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(fireAxis) > 0 && this.transform.parent.transform.parent.tag == "Player" && canFire == true)
        {
            shootingPosition = GetComponentInChildren<Transform>().transform.position;
            fireBullet(0, 5.0f, 1, 10);

            //Debug.DrawRay(shootingPosition, Vector2.right, Color.green, 1.0f, false);
            canFire = false;
            Invoke("enableFire", 2.0f);

        }//end if

    }

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

        //instantiate shotgun pellets
        for (int i = 0; i< pelletCount; i++)
        {

            float angularOffset = (Random.Range(-shotgunSpread, shotgunSpread) * 0.8f);
            //set pellet to face right way
            if (dir == -1)
            {
                angularOffset = 180 - angularOffset;

            }//end if
            GameObject firedPellet = Instantiate(shotgunPellet, shootingPosition, Quaternion.Euler(0, 0, angularOffset)) as GameObject;

            Destroy(firedPellet, 1.0f);

        }//end for

    }//end fireBullet

    void enableFire()
    {
        canFire = true;

    }

}
