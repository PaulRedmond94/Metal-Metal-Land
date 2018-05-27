//Script which controls detecting whether a player is touching the ground or not 
//also includes the code for killing opposing player by jumping on their head


using UnityEngine;
using System.Collections;

public class GroundDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.GetComponentInParent<PlayerGameController>().getAlive())
        {
            if (coll.gameObject.tag.ToLower() == "environment"
            || coll.gameObject.tag.ToLower() == "bombbox"
            || coll.gameObject.tag.ToLower() == "livegrenade")
            {
                if (!gameObject.GetComponentInParent<PlayerMovement>().getGrounded())
                    gameObject.GetComponentInParent<PlayerMovement>().setGrounded(true);
            }


            else if (coll.gameObject.tag.ToLower() == "player")
            {
                if (coll.gameObject.GetComponentInParent<PlayerGameController>().getAlive())
                {
                    coll.gameObject.GetComponent<PlayerGameController>().killPlayer();

                }

                gameObject.GetComponentInParent<PlayerMovement>().setGrounded(true);
            }//end if object is player

        }//if player is alive
        
    }//end onCollsionEnter2D
}
