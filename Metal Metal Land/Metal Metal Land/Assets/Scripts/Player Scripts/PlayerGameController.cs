using UnityEngine;
using System.Collections;

public class PlayerGameController : MonoBehaviour {

    int playerNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void killPlayer()
    {
        Debug.Log("You have been killed");

    }//end kill

    public int getPlayerNumber()
    {
        return playerNumber;

    }//end int

    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;

    }

}
