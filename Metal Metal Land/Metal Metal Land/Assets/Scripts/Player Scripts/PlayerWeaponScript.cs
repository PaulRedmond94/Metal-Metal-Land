using UnityEngine;
using System.Collections;

public class PlayerWeaponScript : MonoBehaviour {

    //declare variables
    string[] weapons = new string[] { "Pistol", "m16", "Minigun", "Shotgun", "Rocket Launcher", "Sword", "Axe" };
    string currentWeapon;
    int weaponLoaded;
    float fireRateTimer, fireRateCap;
    bool firingEnabled;
    bool semiAutoFire;
    GameObject bullet;

    // Use this for initialization
    void Start () {
        weaponLoaded = 0;
        currentWeapon = weapons[weaponLoaded];
        firingEnabled = true;
        fireRateTimer = 0.0f;
        fireRateCap = 0.0f;
        semiAutoFire = true;
        
    }//end start

    // Update is called once per frame
void Update () {
        if (!firingEnabled) {
            if (fireRateTimer < fireRateCap)
            {
                fireRateTimer += Time.deltaTime;

            }//end if
            else
            {
                firingEnabled = true;
            }//end else
        }//end if
        if (Input.GetKeyDown("x"))
        {
            Debug.Log("Previous Weapon: " + currentWeapon);
            weaponLoaded += 1;
            currentWeapon = weapons[weaponLoaded];
            Debug.Log("Current Weapon: " + currentWeapon);
            Debug.Log("\nWeaponLoaded Val: " + weaponLoaded + "\t" + weapons.Length);
            if (weaponLoaded >= weapons.Length - 1)
            {
                weaponLoaded = -1;
            }//end if

        }//end else if x
        if (Input.GetKey("z"))
        {
            if (firingEnabled)
            {
                Debug.Log(getCurrentDir());
                Debug.Log(currentWeapon + " fired");
                if (currentWeapon.ToLower() == "pistol")
                {
                    if (semiAutoFire)
                    {
                        Debug.Log("BANG");
                        //delete me
                        ExecuteAfterTime(5.0f);
                        //set semiAutoFire to false to ensure user can't spam fire
                        semiAutoFire = false;

                        //function to stall firing of gun
                        resetCap(0, 0.15f);

                        //generate a knockback from firing the pistol
                        generateKnockback(0.5f);

                        //fix this so that it also depends on time since last bullet fired
                        float angularOffset = Random.Range(-5.0f, 5.0f);
                        Vector3 positionOffset = new Vector3(2.0f, 0, 0);
                        fireBullet(angularOffset, positionOffset);
                    }//end if
                 
                }//end if weapon is currently the pistol

                if (currentWeapon.ToLower() == "shotgun")
                {
                    if (semiAutoFire)
                    {
                        //set semiAutoFire to false to ensure that the user can't hold down the fire button
                        semiAutoFire = false;

                        //function to stall firing of gun
                        resetCap(0, 1.25f);

                        //generate knockback for user
                        generateKnockback(4.0f);
                        for (int i = 0; i < 8; i++)
                        {
                            float angularOffset = Random.Range(-30.0f, 30.0f);
                            Vector3 positionOffset = new Vector3(2.0f, 0, 0);
                            fireBullet(angularOffset, positionOffset);

                        }//end for

                    }//end if semiautofire

                }//end if

                if(currentWeapon.ToLower() == "m16")
                {
                    if (semiAutoFire)
                    {
                        //disable bullet spam for non auto weapon
                        semiAutoFire = false;

                        //function to stall firing of gun
                        resetCap(0, 0.75f);

                        float burstCap = 0.5f, burstTimer;
                        
                        for (int i = 0; i< 3; i++){
                            //reset burstTime
                            burstTimer = 0.0f;

                            //generate a knockback from firing the pistol
                            generateKnockback(0.5f);

                            //fix this so that it also depends on time since last bullet fired
                            float angularOffset = Random.Range(-15.0f, 15.0f);
                            Vector3 positionOffset = new Vector3(2.0f, 0, 0);
                            fireBullet(angularOffset, positionOffset);

                        }//end for
                        
                    }//end if semiAutoFire
                }//end if m16

                Debug.Log(currentWeapon + " curr");

            }//end if firingEnabled

        }//end if input = z

        if (Input.GetKeyUp("z"))
        {
            semiAutoFire = true;

        }//end if user lets go of fire weapon

    }//end update

    void fireBullet(float angularOffset, Vector3 positionOffset)
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

    void resetCap(float fireRateTimer, float fireRateCap)
    {
        firingEnabled = false;
        this.fireRateTimer = fireRateTimer;
        this.fireRateCap = fireRateCap;

    }//end resetCap

    void generateKnockback(float force)
    {
        float knockbackMod = this.gameObject.GetComponent<PlayerMovement>().getKnockbackModifier();
        force = force * knockbackMod;
        Rigidbody2D playerRigBod2d = this.gameObject.GetComponent<Rigidbody2D>();
        playerRigBod2d.AddForce(new Vector2((force* (-getCurrentDir())),force*0.4f),ForceMode2D.Impulse);

    }//end generateKnockback

    public void setCurrentWeapon(string currentWeapon)
    {
        this.currentWeapon = currentWeapon;
    }//end setCurrentWeapon

    //SOURCED function format (Function declaration and return value) from: https://answers.unity.com/questions/796881/c-how-can-i-let-something-happen-after-a-small-del.html
    IEnumerator ExecuteAfterTime(float time)
    {
        Debug.Log("Before delay");
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Debug.Log("Delay activated");
    }


}
