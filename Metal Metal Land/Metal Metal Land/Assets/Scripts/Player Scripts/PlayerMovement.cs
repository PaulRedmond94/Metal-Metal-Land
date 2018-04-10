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

    //controller items
    string playerMove, playerJump;
    Renderer charRenderer;
    CircleCollider2D characterFootBox;

    

    // Use this for initialization
    void Start () {
        maxSpeedXVal = 4.0f;
        jumpForceYVal = 2.2f;
        acceleration = 4.0f;
        knockbackModifier = 1.0f;
        speedModifier = 1.0f;

        faceLeft = false;
        grounded = true;

        maxSpeed = new Vector2(maxSpeedXVal, 0.0f);

        playerRigBod2d = this.GetComponent<Rigidbody2D>();
        offSet = new Vector3(this.GetComponent<BoxCollider2D>().size.x, 0);
        characterAnimator = this.GetComponent<Animator>();
        characterFootBox = gameObject.GetComponent<CircleCollider2D>();
        currentAnimationState = 0;
        changeAnimationState(0);
        assignAxis();



    }//end start

    // Update is called once per frame
    void Update() {

        //code to control jumping
        //make character jump
        if (grounded && (Input.GetAxis(playerJump)== 1))
        {
            grounded = false;
            playerRigBod2d.AddForce(new Vector2(0,jumpForceYVal), ForceMode2D.Impulse);
            //Bryan's Suggestion
            //playerRigBod2d.AddForce(new Vector2(0, Time.deltaTime * jumpForceYVal), ForceMode2D.Impulse);
            
            knockbackModifier = 1.5f;
            changeAnimationState(2);

        }//end else if

        //check to see if on the ground, if so, re-enable ability to jump
        if (grounded)
        {
            changeAnimationState(0);
            //grounded = true;
            knockbackModifier = 1.0f;

        }// end if not grounded

        //end code to control jumping

        //code to control running
        int dir = (int)Input.GetAxisRaw(playerMove);

        // line for testing potential wall collisions
        //Debug.DrawRay(transform.position+(offSet * dir), ((Vector2.left) * 1f) * dir, Color.green, 3.0f, false);

        //function to apply force to character sprite to make them move
        //raycast used to determine if character is going to run into a wall. If so, don't let them put force into it
        RaycastHit2D wallDetect = new RaycastHit2D();
        Debug.DrawRay(new Vector2((transform.position.x),transform.position.y+5.0f), (Vector2.left * 1f) * -dir, Color.green, 3.0f, false);
        wallDetect = Physics2D.Raycast(transform.position + (offSet/2 * dir), (Vector2.left * 0.15f) * dir);
        try
        {
            if (wallDetect.collider.gameObject.tag == "Environment")
            {
                //Debug.Log(wallDetect.collider);
                Vector2 stopChar = playerRigBod2d.velocity;
                stopChar.x = 0;
                playerRigBod2d.velocity = stopChar;

            }//end if

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
        if (Input.GetAxis(playerMove)> -0.5f && Input.GetAxis(playerMove) < 0.5f)
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
        if (Input.GetAxis(playerMove)== -1)
        {
            if(!faceLeft)
            {
                this.transform.rotation = (Quaternion.Euler(0, 180, 0));

            }//end if
            faceLeft = true;

        }// end if to make character face left
        
        //make character face to the right
        else if (Input.GetAxis(playerMove)== 1)
        {
            if (faceLeft)
            {
                this.transform.rotation = (Quaternion.Euler(0, 0, 0));
                
            }
            faceLeft = false;

        }//end else if to make character to face right


        //make character sprite run
        if((Input.GetAxis(playerMove) == 1 || Input.GetAxis(playerMove) == -1) && grounded)
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

    public void assignAxis()
    {
        //Debug.Log(gameObject.name + " " + StaticScript.player1Character+ "(Clone)");
        if (gameObject.name == StaticScript.player1Character + "(Clone)")
        {
            playerMove = "p1_move";
            playerJump = "p1_jump";

        }
        else
        {
            playerMove = "p2_move";
            playerJump = "p2_jump";

        }

    }

    /*public void assignAxis(int playerNum)
    {
        if (playerNum == 1)
        {
            playerMove = "p1_move";
            playerJump = "p1_jump"; 

        }
        else
        {
            playerMove = "p1_move";
            playerJump = "p1_jump";

        }

    }
    */

    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;

    }//end setGrounded

    public bool getGrounded()
    {
        return grounded;

    }

}
