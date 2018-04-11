using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    //controls game logic for each round

    bool player1Alive;
    bool player2Alive;

    int scoreCap;
    int player1Score;
    int player2Score;

    bool deathDetected;
    bool loadLevel;
    bool suddenDeath;
    AsyncOperation loadingLevelCoRoutine;
    GameObject[] players;

	// Use this for initialization
	void Awake () {
        player1Alive = true;
        player2Alive = true;

        scoreCap = StaticScript.roundCount;
        Invoke("findPlayers", 1);
        
        deathDetected = false;
        suddenDeath = false;

        StartCoroutine(loadNextLevel());

    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            if (Time.frameCount % 60 == 0)
            {
                foreach (GameObject playChar in players)
                {
                    if (playChar.GetComponent<PlayerGameController>().getAlive() == false)
                    {
                        if (deathDetected == false)
                        {
                            deathDetected = true;
                            Invoke("roundVictory", 3);

                        }
                    }

                }

            }

        }
        catch(NullReferenceException nre)
        {
            //this catch is here in the event that the game has loaded the characters too slowly

        }

        //if sudden death is enabled, drop a bomb every 3 seconds from a random position
        if(suddenDeath && Time.frameCount % 180 == 0)
        {
            Debug.Log("bomb dropped, rip");
            
        }//end if
        

	}

    public void roundVictory()
    {
        Debug.Log("In round victory");
        
        foreach(GameObject charObj in players)
        {
            if (charObj.GetComponent<PlayerGameController>().getAlive())
            {
                if(charObj.name + "(Clone)" == StaticScript.player1Character)
                {
                    StaticScript.player1Score++;
                    Debug.Log("Player 1 gets a point");
                }
                else
                {
                    StaticScript.player2Score++;
                    Debug.Log("Player 2 gets a point");

                }

            }

        }
        
        Debug.Log("Round is over");
        
        //load new level
        loadingLevelCoRoutine.allowSceneActivation = true;

    }

    IEnumerator loadNextLevel()
    {
        loadingLevelCoRoutine = SceneManager.LoadSceneAsync(StaticScript.nextSceneToLoad);
        loadingLevelCoRoutine.allowSceneActivation = false;
        yield return null;

    }//end loadNextLevel

    void findPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerGameController>().setAlive(true);

        }

    }

    public void beginSuddenDeath()
    {
        suddenDeath = true;

    }
    

}
