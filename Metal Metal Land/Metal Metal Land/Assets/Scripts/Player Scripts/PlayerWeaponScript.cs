using UnityEngine;
using System.Collections;

public class PlayerWeaponScript : MonoBehaviour {

    //declare variables
    string[] weapons = new string[] { "Pistol", "m16", "Minigun", "Shotgun", "Rocket Launcher", "Sword", "Axe" };
    string currentWeapon;
    int weaponLoaded, frameCount;
    float fireRateTimer, fireRateCap, minigunSpoolTime;
    bool firingEnabled;
    bool semiAutoFire;
    bool isKamikazee;
    GameObject bullet;


    int bulletsFired;

    // Use this for initialization
    void Start () {
        weaponLoaded = 0;
        frameCount = 0;
        currentWeapon = weapons[weaponLoaded];
        firingEnabled = true;
        fireRateTimer = 0.0f;
        fireRateCap = 0.0f;
        semiAutoFire = true;
        bulletsFired = 0;
        minigunSpoolTime = 0.0f;
        isKamikazee = false;
        
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

        //TODO Remove this statement as it is a debug statement that isn't really needed
        if (Input.GetKeyDown("x"))
        {
            weaponLoaded += 1;
            currentWeapon = weapons[weaponLoaded];
            Debug.Log("Current Weapon: " + currentWeapon);
            if (weaponLoaded >= weapons.Length - 1)
            {
                weaponLoaded = -1;
            }//end if

        }//end else if x
        if (Input.GetKey("z"))
        {
            if (firingEnabled)
            {
                Debug.Log(currentWeapon + " fired");
                if (currentWeapon.ToLower() == "pistol")
                {
                    if (semiAutoFire)
                    {
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

                else if (currentWeapon.ToLower() == "shotgun")
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
                            float angularOffset = (Random.Range(-30.0f, 30.0f) * 0.8f) ;
                            Vector3 positionOffset = new Vector3(2.0f, 0, 0);
                            fireBullet(angularOffset, positionOffset);

                        }//end for

                    }//end if semiautofire

                }//end if

                else if(currentWeapon.ToLower() == "m16")
                {
                    if (semiAutoFire)
                    {
                        //disable bullet spam for non auto weapon
                        semiAutoFire = false;

                        //function to stall firing of gun
                        resetCap(0, 0.5f);

                        //float burstCap = 0.5f, burstTimer;

                        for (int i = 0; i < 3; i++) {
                            Invoke("autoBurstFire", (0.2f * i));

                        }//end for
                        
                    }//end if semiAutoFire

                }//end if m16                

                else if(currentWeapon.ToLower() == "minigun")
                {
                    frameCount++;

                    if (frameCount == 30)
                    {
                        resetCap(0, 0.0f);
                        Invoke("autoFire", 0.2f);

                    }//end if fire has been held for 1 second

                    else if (frameCount == 75)
                    {
                        resetCap(0, 0.0f);
                        Invoke("autoFire", 0.2f);

                    }//end if fire has been held for almost 2 seconds

                    else if (frameCount == 105)
                    {
                        resetCap(0, 0.0f);
                        Invoke("autoFire", 0.2f);

                    }//end if fire has been held for 160 frames

                    else if (frameCount == 125)
                    {
                        resetCap(0, 0.0f);
                        Invoke("autoFire", 0.2f);

                    }//end else if

                    else if(frameCount == 140)
                    {
                        resetCap(0, 0.0f);
                        Invoke("autoFire", 0.2f);

                    }
                    //fire at full speed
                    if(frameCount >= 160)
                    {
                        resetCap(0, 0.125f);
                        Invoke("autoFire", 0.2f);

                    }//end if

                    Debug.Log("Frame count " + frameCount);

                }//end else if minigun

                else if(currentWeapon.ToLower() == "axe")
                {
                    /*isKamikazee = true;
                    while (isKamikazee)
                    {
                        this.getC
                        if (this.GetComponent<Rigidbody2D>().velocity == new Vector2(0,0))
                        {
                            this.GetComponent<SpawnerScript>().respawn();
                            isKamikazee = false;

                        }//end if

                    }//end while
                    */

                }//end else if axe*/

            }//end if firingEnabled

        }//end if input = z

        if (Input.GetKeyUp("z"))
        {
            semiAutoFire = true;
            minigunSpoolTime = 0.0f;
            frameCount = 0;

        }//end if user lets go of fire weapon

    }//end update

    void autoBurstFire()
    {
        //generate a knockback from firing the pistol
        generateKnockback(0.5f);

        //fix this so that it also depends on time since last bullet fired
        float angularOffset = Random.Range(-15.0f, 15.0f);
        Vector3 positionOffset = new Vector3(2.0f, 0, 0);
        fireBullet(angularOffset, positionOffset);

    }//end autoBurstFire

    void autoFire()
    {
        bulletsFired++;

        Debug.Log("autofire bang: " + bulletsFired);
        float angularOffset = Random.Range(-20.0f, 20.0f);
        Vector3 positionOffset = new Vector3(2.0f, 0, 0);
        fireBullet(angularOffset, positionOffset);

        generateKnockback(0.8f);

    }//end autoFire

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
    
    void fireBullet(float angularOffset, Vector3 positionOffset)
    {
        //position is off
        positionOffset = this.gameObject.transform.GetChild(0).transform.position;

        bullet = (GameObject)Instantiate(Resources.Load("BulletPrefab"), positionOffset, Quaternion.Euler(0, 0, angularOffset));
        bullet.GetComponent<BulletScript>().setDir(getCurrentDir());
        bullet.GetComponent<BulletScript>().setTimeToLive(currentWeapon);
    }//end fireBullet

    int getCurrentDir()
    {
        //bool isLeft = this.gameObject.GetComponent<PlayerMovement>().getFaceLeft();
        bool isLeft = true;
        if (isLeft)
        {
            return -1;

        }//end if
        else
        {
            return 1;

        }//end else
    }//end getCurrentDir

    
    public void setCurrentWeapon(string currentWeapon)
    {
        this.currentWeapon = currentWeapon;

    }//end setCurrentWeapon
    

}//end class
