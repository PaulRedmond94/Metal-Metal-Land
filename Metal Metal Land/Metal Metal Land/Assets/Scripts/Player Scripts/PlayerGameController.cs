using UnityEngine;
using System.Collections;

public class PlayerGameController : MonoBehaviour {

    int playerNumber;
    bool alive;

	// Use this for initialization
	void Start () {
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void killPlayer()
    {
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
