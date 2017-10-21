using UnityEngine;
using System.Collections;

public class DebugTestScripts : MonoBehaviour {

    float speed = 10.0f;
    Rigidbody2D rigBod;
    GameObject playerCharacter;
    

    // Use this for initialization
    void Start () {
        playerCharacter = this.gameObject;
        rigBod = playerCharacter.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            Debug.Log("kbm jump");
            rigBod.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);

        }//end else if

        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        //moveDir.y = Input.GetAxis("Vertical");
        playerCharacter.transform.position += (moveDir * speed * Time.deltaTime);
        if (Input.GetKeyDown("a"))
        {
            Debug.Log("kbm going left");
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
/*            Debug.Log("kbm going right");
            Vector3 moveDir = Vector3.zero;
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
        
    
        //x = x * Input.GetAxis("Mouse X");
        //y = y * Input.GetAxis("Mouse Y");

        //Debug.Log("X val: " + x + "\tY val: " + y);
	}

    void onCollisionEnter2d(Collision2D coll) {
        //if(coll.gameObject.tag == "Environment")
        //{
            Debug.Log("Crazy Diamond > Killer Queen");

        //}//end if


    }//end collisionenter2d
    
}
