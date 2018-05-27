using UnityEngine;
using System.Collections;

/*
Script which is used to control:
    player movement (running, jumping etc.)
    player animations for previously mentioned movement states


*/

public class PlayerMovement : MonoBehaviour {

    //variables to manipulate movement
    float maxSpeedXVal;
    float jumpForceYVal;
    float acceleration;
    Vector2 maxSpeed, stopChar;

    //varaibles to manipulate knockback, jump and speed modifications while moving or with powerups
    float knockbackModifier, speedModifier, jumpModifier;

    Rigidbody2D playerRigBod2d;

    bool faceLeft, grounded;

    bool canMosh, currentlyMoshing;
    
    //variables to manipulate animation
    Animator characterAnimator;
    int currentAnimationState;

    Vector3 offSet;

    //controller items
    string playerMove, playerJump, playerMosh;
    Renderer charRenderer;

    // Use this for initialization
    void Start () {
        maxSpeedXVal = 4.0f;
        jumpForceYVal = 2.2f;
        acceleration = 4.0f;
        knockbackModifier = 1.0f;
        speedModifier = 1.0f;
        jumpModifier = 1.0f;
        faceLeft = false;
        grounded = true;
        maxSpeed = new Vector2(maxSpeedXVal, 0.0f);
        playerRigBod2d = this.GetComponent<Rigidbody2D>();
        characterAnimator = this.GetComponent<Animator>();
        currentAnimationState = 0;
        changeAnimationState(0);
        assignAxis();

        canMosh = true;

    }//end start

    // Update is called once per frame
    void FixedUpdate() {

        //only allow player to move at all if they're alive
        if (gameObject.GetComponent<PlayerGameController>().getAlive())
        {
            //code to control jumping
            //make character jump
            if (grounded && (Input.GetAxis(playerJump) == 1))
            {
                grounded = false;
                playerRigBod2d.AddForce(new Vector2(0, jumpForceYVal * jumpModifier), ForceMode2D.Impulse);
                knockbackModifier = 1.5f;
                changeAnimationState(2);

            }//end else if

            //check to see if on the ground, if so, re-enable ability to jump
            if (grounded)
            {
                changeAnimationState(0);
                knockbackModifier = 1.0f;

            }// end if not grounded

            //end code to control jumping

            //code to control running
            int dir = (int)Input.GetAxisRaw(playerMove);

            //function to apply force to character sprite to make them move
            try
            {
                if (maxSpeed.x > Mathf.Abs(playerRigBod2d.velocity.x))
                {
                    //move character
                    playerRigBod2d.AddForce((Vector2.right * acceleration * dir) * speedModifier);

                }//end else if

            }//end try
            catch (System.NullReferenceException nre)
            {
                //this is a catch used in the event that the character is touching air

            }//end catch 

            //character is no longer running, make the character stand still
            if (Input.GetAxis(playerMove) > -0.5f && Input.GetAxis(playerMove) < 0.5f)
            {
                stopChar = playerRigBod2d.velocity;
                stopChar.x = 0;

                if (grounded)
                {
                    changeAnimationState(0);

                }//end if character is grounded

            }//end if

            //make character face the left
            if (Input.GetAxis(playerMove) == -1)
            {
                if (!faceLeft)
                {
                    this.transform.rotation = (Quaternion.Euler(0, 180, 0));

                }//end if
                faceLeft = true;

            }// end if to make character face left

            //make character face to the right
            else if (Input.GetAxis(playerMove) == 1)
            {
                if (faceLeft)
                {
                    this.transform.rotation = (Quaternion.Euler(0, 0, 0));

                }
                faceLeft = false;

            }//end else if to make character to face right


            //make character sprite run
            if ((Input.GetAxis(playerMove) == 1 || Input.GetAxis(playerMove) == -1) && grounded)
            {
                changeAnimationState(1);

            }//end if

            //code to control moshing ability
            if (canPlayerMosh())
            {
                canMosh = false;
                currentlyMoshing = true;
                Invoke("allowMosh", 1.0f);

                playerRigBod2d.AddForce(new Vector2(((jumpForceYVal) * getFaceLeft()), jumpForceYVal / 2), ForceMode2D.Impulse);


            }//end if

        }//end if player is alive

    }//end update

    //function to detect moshing
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (currentlyMoshing)
        {
            if(coll.gameObject.tag.ToLower() == "environment")
            {
                coll.gameObject.GetComponent<CellBehaviourScript>().decreaseCellHealth(2);
                Debug.Log("Please stop moshing, the ground can only take so much");


            }//end if coll is environment
            currentlyMoshing = false;

        }//end currentlyMoshing

    }

    //function which changes sprites current animation style
    public void changeAnimationState(int state)
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

    public void assignAxis()
    {
        if (gameObject.name == StaticScript.player1Character + "(Clone)")
        {
            playerMove = "p1_move";
            playerJump = "p1_jump";
            playerMosh = "p1_mosh";

        }
        else
        {
            playerMove = "p2_move";
            playerJump = "p2_jump";
            playerMosh = "p2_mosh";

        }//end else 

    }//end assignAxis

    public bool getGrounded()
    {
        return grounded;

    }

    //function to determine if mosh ability should be usable,
    //primarily used to dramatically shorten the if statement conditions because it was pretty verbose
    bool canPlayerMosh()
    {
        //if statement to see if player presses fire button, has no weapon and cooldown is over for mosh ability
        if (Input.GetAxis(playerMosh) == 1
            && canMosh)
        {
            return true;

        }
        else
        {
            return false;

        }

    }//end canPlayerMosh

    void allowMosh()
    {
        canMosh = true;

    }//end void


    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;

    }//end setGrounded

    public void setSpeedModifier(float speedModifier)
    {
        this.speedModifier = speedModifier;

    }//end set speed modifier

    public void setJumpModifier(float jumpModifier)
    {
        this.jumpModifier = jumpModifier;

    }//end set jump modifer

}
