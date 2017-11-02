using UnityEngine;
using System.Collections;

public class DebugTestScripts : MonoBehaviour {

    float acceleration = 15.0f;
    Rigidbody2D rigBod;
    GameObject playerCharacter;
    bool faceLeft;
    public Vector2 initialPos;

    string[] weapons = new string[] { "Pistol", "AK-47", "Minigun", "Shotgun", "Rocket Launcher", "Sword", "Axe" };
    string currentWeapon;
    int weaponLoaded;

    // Use this for initialization
    void Start () {
        playerCharacter = this.gameObject;
        rigBod = playerCharacter.GetComponent<Rigidbody2D>();
        faceLeft = true;
        initialPos = this.transform.position;
        weaponLoaded = 0;
        currentWeapon = weapons[weaponLoaded];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            Debug.Log("kbm jump");
            rigBod.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);

        }//end else if

        /*          OLD MOVEMENT CODE
        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        //moveDir.y = Input.GetAxis("Vertical");
        playerCharacter.transform.position += (moveDir * speed * Time.deltaTime);

        */
        // NEW MOVEMENT CODE
        float dir = Input.GetAxis("Horizontal");
        Vector2 maxSpeed = new Vector2(10.0f, 0.0f);
        if (maxSpeed.x > Mathf.Abs(rigBod.velocity.x))
        {
            rigBod.AddForce(Vector2.right * acceleration * dir);

        }//end if

        //Debug messages for speed
        //Debug.Log("Abs Vel: " + Mathf.Abs(rigBod.velocity.x));
        //Debug.Log("Base Vel: " + rigBod.velocity);
        //Debug.Log("Max Speed: " + maxSpeed.x);



        if (Input.GetKeyUp("a")||Input.GetKeyUp("d"))
        {
            //rigBod.velocity.x= rigBod.velocity.x*0.2f;
            Vector2 modVelocity = rigBod.velocity;
            modVelocity.x = (modVelocity.x * 0.2f);
            rigBod.velocity = modVelocity;

        }
        
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("kbm going left");
            faceLeft = true;
            //Vector2 newPos = this.transform.position;
            //newPos.x++;
            //this.transform.position = newPos;

        }//end if kbm go left

        else if (Input.GetKeyDown("s"))
        {
            Debug.Log("kbm going down");

        }//end else if kbm down

        else if (Input.GetKey("d"))
        {
            faceLeft = false;
            Debug.Log("kbm going right");
/*            Vector3 moveDir = Vector3.zero;
            moveDir.x = Input.GetAxis("Horizontal");
            this.gameObject.transform.position += moveDir + speed * Time.deltaTime;
            */
            //use for dash code later on
            //playerCharacter.GetComponent<Rigidbody2D>().AddForce(new Vector2(15, 0), ForceMode2D.Impulse);

        }//end else if kbm right

        

        else if (Input.GetKeyDown("down"))
        {
            Debug.Log("Weapon picked up");

        }//end else if

        else if (Input.GetKeyDown("left"))
        {
            Debug.Log("Weapon fired");

        }//end else if

        else if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Controller Jump Detected");

        }//end if
        else if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("controller going horizontal");
        }
        else if (Input.GetButtonDown("Vertical"))
        {
            Debug.Log("controller Going vertical");

        }

        else if (Input.GetKeyDown("g"))
        {
            respawn();

        }//end else if
        
        else if (Input.GetKeyDown("z"))
        {
            //method to call old bullet code
            fireBullet(faceLeft);
            GameObject rayHitObj = new GameObject();
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.left);
            Debug.DrawLine(transform.position, rayHit.point, Color.red);



        }
        else if (Input.GetKeyDown("x"))
        {
            Debug.Log("Previous Weapon: " + currentWeapon);
            weaponLoaded += 1;
            currentWeapon = weapons[weaponLoaded];
            Debug.Log("Current Weapon: " + currentWeapon);
            Debug.Log("\nWeaponLoaded Val: " + weaponLoaded + "\t" + weapons.Length);
            if(weaponLoaded >= weapons.Length-1)
            {
                weaponLoaded = -1;
            }//end if

        }//end else if x
    
        //x = x * Input.GetAxis("Mouse X");
        //y = y * Input.GetAxis("Mouse Y");

        //Debug.Log("X val: " + x + "\tY val: " + y);
	}

    public void fireBullet(bool faceLeft)
    {
        int dir = 0;
        if (faceLeft == true)
        {
            dir = -1;

        }//end if
        else if (faceLeft == false) 
        {
            dir = 1;

        }//end else
        Debug.Log(dir);

        if(1==1)
        {
            GameObject bullet;
            Vector3 offset = new Vector3(dir, 0);
            bullet = (GameObject) Instantiate(Resources.Load("BulletPrefab"), transform.position+offset, transform.rotation);
            bullet.GetComponent<BulletScript>().setDir(dir);
            
        }
       /* if (2 == 2)
        {
            GameObject bullet;
            Vector3 offset = new Vector3(dir, 0);
            
            //bullet = (Instantiate(Resources.Load("BulletPrefab", transform.position, transform.rotation)) as GameObject;
          
            bullet = (GameObject)Instantiate(Resources.Load("BulletPrefab"), transform.position + offset, transform.rotation*=Quaternion.AngleAxis(45,transform.position));
            Debug.Log(transform.rotation *= Quaternion.Euler(45, 0, 0));
            Debug.Log(transform.rotation);
            bullet.GetComponent<BulletScript>().setDir(dir);

        }*/
        


    }//end fireBullet
    
    public void respawn()
    {
        this.transform.position = initialPos;

    }



}
