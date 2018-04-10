using UnityEngine;
using System.Collections;

public class PlayerGameController : MonoBehaviour {

    int playerNumber;
    bool alive;
    GameObject deathPointVert;
    public BoxCollider2D headPoint;

	// Use this for initialization
	void Awake () {
        alive = true;
        deathPointVert = GameObject.FindGameObjectWithTag("BoundaryPoint");
	}
	
	// Update is called once per frame
	void Update () {
	    if(alive == false)
        {
            transform.eulerAngles = (new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90.0f));
        }

        if(Time.frameCount% 60 == 0)
        {
            if(transform.position.y < deathPointVert.transform.position.y)
            {
                
                killPlayer();
                

            }

        }//end time

	}

    public void killPlayer()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerMovement>().changeAnimationState(0);
        Debug.Log("You have been killed");
        alive = false;

    }//end kill

    public bool getAlive()
    {
        return alive;

    }//end getAlive

    public void setAlive(bool alive)
    {
        this.alive = alive;

    }

    public int getPlayerNumber()
    {
        return playerNumber;

    }//end int

    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;

    }

}
