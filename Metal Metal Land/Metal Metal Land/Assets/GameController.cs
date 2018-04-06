using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //controls game logic for each round

    bool player1Alive;
    bool player2Alive;

    int scoreCap;
    int player1Score;
    int player2Score;

    bool deathDetected;

	// Use this for initialization
	void Start () {
        player1Alive = true;
        player2Alive = true;

        scoreCap = StaticScript.roundCount;
        

	}
	
	// Update is called once per frame
	void Update () {
	    

	}

    void killPlayer(int player)
    {
        if (player == 1)
        {
            player1Alive = false;

        }
        else if(player == 2)
        {
            player2Alive = false;

        }
        Debug.Log("Player death detected");
        if (!deathDetected)
        {
            deathDetected = true;
            Invoke("roundVictory", 3.0f);
            
        }
        

    }//end killPlayer

    public void roundVictory()
    {
        if (player1Alive)
            StaticScript.player1Score++;

        else if (player2Alive)
            StaticScript.player2Score++;
        
        Debug.Log("Round is over");

    }

}
