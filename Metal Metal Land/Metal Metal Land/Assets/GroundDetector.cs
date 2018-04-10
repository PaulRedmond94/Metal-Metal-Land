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
        if (coll.gameObject.tag.ToLower() == "environment" || coll.gameObject.tag.ToLower() == "bombbox") 
        {
            Debug.Log("touched the ground");
            if (!gameObject.GetComponentInParent<PlayerMovement>().getGrounded())
                gameObject.GetComponentInParent<PlayerMovement>().setGrounded(true);
        }
        

        if(coll.gameObject.tag.ToLower() == "player")
        {
            coll.gameObject.GetComponent<PlayerGameController>().killPlayer();

        }
    }
}
