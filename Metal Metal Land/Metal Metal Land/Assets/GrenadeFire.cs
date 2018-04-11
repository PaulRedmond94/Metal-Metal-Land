using UnityEngine;
using System.Collections;

public class GrenadeFire : MonoBehaviour {

    GameObject liveGrenadePrefab;
    bool canFire;
    Vector2 shootingPosition;
    string playerFireAxis;

    // Use this for initialization
    void Start()
    {
        liveGrenadePrefab = Resources.Load("Objects/Ammo/LiveGrenade") as GameObject;
        canFire = true;
        playerFireAxis = transform.parent.transform.parent.GetComponent<PlayerWeaponPickup>().getFireAxis();

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(playerFireAxis)>0 && transform.parent.transform.parent.tag == "Player" && canFire == true && GetComponentInParent<PlayerGameController>().getAlive())
        {
            //shootingPosition = GetComponentInChildren<Transform>().transform.position;
            shootingPosition = transform.GetChild(0).transform.position;

            GameObject liveGrenade = Instantiate(liveGrenadePrefab, shootingPosition, gameObject.transform.rotation) as GameObject;
            Rigidbody2D grenadeRigBod2D = liveGrenade.GetComponent<Rigidbody2D>();
            grenadeRigBod2D.AddForce((transform.right * grenadeRigBod2D.mass) * 5.0f + (transform.up*grenadeRigBod2D.mass) * 4.0f, ForceMode2D.Impulse);
            //Debug.DrawRay(shootingPosition, Vector2.right, Color.green, 1.0f, false);
            canFire = false;
            Invoke("enableFire", .3f);

        }//end if

    }
    void enableFire()
    {
        canFire = true;

    }
}
