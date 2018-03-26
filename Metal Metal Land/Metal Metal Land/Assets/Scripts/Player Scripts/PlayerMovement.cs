using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float acceleration = 3.0f;
    float knockbackModifier, speedModifier;
    Rigidbody2D playerRigBod2d;
    GameObject playerCharacter;
    bool faceLeft, grounded;
    Vector2 maxSpeed;
    Animator characterAnimator;
    int currentAnimationState;

    // Use this for initialization
    void Start () {
        playerCharacter = this.gameObject;
        playerRigBod2d = playerCharacter.GetComponent<Rigidbody2D>();
        faceLeft = false;
        grounded = true;
        knockbackModifier = 1.0f;
        speedModifier = 1.0f;
        maxSpeed = new Vector2(5.0f, 0.0f);
        characterAnimator = this.GetComponent<Animator>();
        currentAnimationState = 0;
        changeAnimationState(0);

    }//end start
	
    //TODO modify jumping controls so that they 

	// Update is called once per frame
	void Update () {

        //make character jump
        if (grounded && (Input.GetKeyDown("w") || Input.GetKeyDown("space")))
        {
            Debug.Log("kbm jump");
            playerRigBod2d.AddForce(new Vector2(0, 1.75f), ForceMode2D.Impulse);
            grounded = false;
            knockbackModifier = 1.5f;
            Debug.Log("Can't jump");
            changeAnimationState(2);

        }//end else if

        //check to see if on the ground, if so, re-enable ability to jump
        if (!grounded)
        {
            if (playerRigBod2d.velocity.y == 0)
            {
                Debug.Log("Can jump again");
                changeAnimationState(0);
                grounded = true;
                knockbackModifier = 1.0f;

            }//end if

        }// end if not grounded


        float dir = Input.GetAxis("Horizontal");
        if (maxSpeed.x > Mathf.Abs(playerRigBod2d.velocity.x))
        {
            playerRigBod2d.AddForce((Vector2.right * acceleration * dir)*speedModifier);

        }//end if

        //Debug messages for speed
        //Debug.Log("Abs Vel: " + Mathf.Abs(rigBod.velocity.x));
        //Debug.Log("Base Vel: " + rigBod.velocity);
        //Debug.Log("Max Speed: " + maxSpeed.x);

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            //rigBod.velocity.x= rigBod.velocity.x*0.2f;
            Vector2 modVelocity = playerRigBod2d.velocity;
            modVelocity.x = 0;
            playerRigBod2d.velocity = modVelocity;
            changeAnimationState(0);

        }//end if

        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Left");
            if(!faceLeft)
            {
                Debug.Log("Switch Left");
                this.transform.rotation = (Quaternion.Euler(0, 180, 0));

            }//end if
            faceLeft = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            Debug.Log("Right");
            if (faceLeft)
            {
                Debug.Log("Switch right");
                this.transform.rotation = (Quaternion.Euler(0, 0, 0));
                
            }
            faceLeft = false;

        }//end else if

        if((Input.GetKey("a") || Input.GetKey("d"))&& grounded)
        {
            changeAnimationState(1);            

        }

        if (Input.GetKeyDown("s"))
        {
            Debug.Log("Crouching");
            knockbackModifier = 0.75f;
            speedModifier = 0.5f;
        }//end if
        if (Input.GetKeyUp("s"))
        {
            knockbackModifier = 1.0f;
            Debug.Log("Not crouching");
            speedModifier = 1.0f;
        }//end if

    }//end update

    public bool getFaceLeft()
    {
        return this.faceLeft;

    }//end getFaceLeft

    public float getKnockbackModifier()
    {
        return knockbackModifier;

    }//end getKnockbackModifier

    void changeAnimationState(int state)
    {
        if (currentAnimationState == state)
            return;

        switch (state)
        {
            case 0:
                characterAnimator.SetInteger("animationState", 0);
                Debug.Log("now standing still");
                break;

            case 1:
                characterAnimator.SetInteger("animationState", 1);
                Debug.Log("now running");
                break;

            case 2:
                characterAnimator.SetInteger("animationState", 2);
                Debug.Log("now jumping");
                break;

        }//end switch

        currentAnimationState = state;

    }//end changeAnimationState

}
