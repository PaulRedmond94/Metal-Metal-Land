using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //variables to manipulate movement
    float maxSpeedXVal;
    float jumpForceYVal;
    float acceleration;
    Vector2 maxSpeed, stopChar;

    //varaibles to manipulate knockback and speed modifications while moving
    float knockbackModifier, speedModifier;

    Rigidbody2D playerRigBod2d;

    bool faceLeft, grounded;
    
    //variables to manipulate animation
    Animator characterAnimator;
    int currentAnimationState;

    Vector3 offSet;

    // Use this for initialization
    void Start () {
        maxSpeedXVal = 4.0f;
        jumpForceYVal = 2.0f;
        acceleration = 4.0f;
        knockbackModifier = 1.0f;
        speedModifier = 1.0f;

        faceLeft = false;
        grounded = true;

        maxSpeed = new Vector2(maxSpeedXVal, 0.0f);

        playerRigBod2d = this.GetComponent<Rigidbody2D>();
        offSet = new Vector3(this.GetComponent<BoxCollider2D>().size.x, 0);
        characterAnimator = this.GetComponent<Animator>();
        
        currentAnimationState = 0;
        changeAnimationState(0);
        

    }//end start

    // Update is called once per frame
    void Update() {

        //code to control jumping
        //make character jump
        if (grounded && (Input.GetKeyDown("w") || Input.GetKeyDown("space")))
        {
            playerRigBod2d.AddForce(new Vector2(0, jumpForceYVal), ForceMode2D.Impulse);
            grounded = false;
            knockbackModifier = 1.5f;
            changeAnimationState(2);

        }//end else if

        //check to see if on the ground, if so, re-enable ability to jump
        if (!grounded && playerRigBod2d.velocity.y == 0)
        {
            changeAnimationState(0);
            grounded = true;
            knockbackModifier = 1.0f;

        }// end if not grounded

        //end code to control jumping

        //code to control running
        float dir = Input.GetAxisRaw("Horizontal");

        // line for testing potential wall collisions
        //Debug.DrawRay(transform.position+(offSet * dir), ((Vector2.left) * 1f) * dir, Color.green, 3.0f, false);

        //function to apply force to character sprite to make them move
        //raycast used to determine if character is going to run into a wall. If so, don't let them put force into it
        RaycastHit2D wallDetect = new RaycastHit2D();
        wallDetect = Physics2D.Raycast(transform.position + (offSet * dir), (Vector2.left * 0.25f) * dir);
        try
        {
            if (wallDetect.collider.gameObject.tag == "Environment")
            {
                //Debug.Log(wallDetect.collider);
                Vector2 stopChar = playerRigBod2d.velocity;
                stopChar.x = 0;
                playerRigBod2d.velocity = stopChar;

            }//end if

            else if (maxSpeed.x > Mathf.Abs(playerRigBod2d.velocity.x))
            {
                playerRigBod2d.AddForce((Vector2.right * acceleration * dir) * speedModifier);

            }//end else if

        }//end try
        catch (System.NullReferenceException nre)
        {
            //this is a catch used in the event that the character is touching air

        }//end catch 
        
        //character is no longer running, make the character stand still
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            //rigBod.velocity.x= rigBod.velocity.x*0.2f;
            stopChar = playerRigBod2d.velocity;
            stopChar.x = 0;

            if (grounded)
            {
                changeAnimationState(0);

            }//end if character is grounded

        }//end if

        //make character face the left
        if (Input.GetKeyDown("a"))
        {
            if(!faceLeft)
            {
                this.transform.rotation = (Quaternion.Euler(0, 180, 0));

            }//end if
            faceLeft = true;

        }// end if to make character face left
        
        //make character face to the right
        else if (Input.GetKeyDown("d"))
        {
            if (faceLeft)
            {
                this.transform.rotation = (Quaternion.Euler(0, 0, 0));
                
            }
            faceLeft = false;

        }//end else if to make character to face right


        //make character sprite run
        if((Input.GetKey("a") || Input.GetKey("d"))&& grounded)
        {
            changeAnimationState(1);            

        }//end if

        //character is now crouching, reduce speed and knockback
        if (Input.GetKeyDown("s"))
        {
            Debug.Log("Crouching");
            knockbackModifier = 0.75f;
            speedModifier = 0.5f;
        }//end if

        //character is now standing, return knockback modifier to normal state
        if (Input.GetKeyUp("s"))
        {
            knockbackModifier = 1.0f;
            Debug.Log("Not crouching");
            speedModifier = 1.0f;
        }//end if

    }//end update

    //function which changes sprites current animation style
    void changeAnimationState(int state)
    {
        
        if (currentAnimationState == state)
            return;

        switch (state)
        {
            //character is standing still
            case 0:
                characterAnimator.SetInteger("animationState", 0);
                currentAnimationState = 0;
                break;
            
            //character is running
            case 1:
                characterAnimator.SetInteger("animationState", 1);
                currentAnimationState = 1;
                break;
            
            // character is jumping
            case 2:
                characterAnimator.SetInteger("animationState", 2);
                currentAnimationState = 2;
                break;

        }//end switch

        currentAnimationState = state;

    }//end changeAnimationState


    //getters and setters
    public int getFaceLeft()
    {
        //facing left
        if (faceLeft)
        {
            return -1;

        }//end if

        //facing right
        else
        {
            return 1;
        }

    }//end getFaceLeft

    public float getKnockbackModifier()
    {
        return knockbackModifier;

    }//end getKnockbackModifier



}
