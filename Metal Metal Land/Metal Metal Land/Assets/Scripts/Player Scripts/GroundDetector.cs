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
        if (coll.gameObject.tag.ToLower() == "environment"
            || coll.gameObject.tag.ToLower() == "bombbox"
            || coll.gameObject.tag.ToLower() == "livegrenade") 
        {
            if (!gameObject.GetComponentInParent<PlayerMovement>().getGrounded())
                gameObject.GetComponentInParent<PlayerMovement>().setGrounded(true);
        }
        

        else if(coll.gameObject.tag.ToLower() == "player")
        {
            if (coll.gameObject.GetComponent<PlayerGameController>().getAlive())
            {
                coll.gameObject.GetComponent<PlayerGameController>().killPlayer();

            }

            gameObject.GetComponentInParent<PlayerMovement>().setGrounded(true);
        }
    }
}
