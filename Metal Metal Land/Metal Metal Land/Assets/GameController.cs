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
    AsyncOperation loadingLevelCoRoutine;
    GameObject[] players;

	// Use this for initialization
	void Awake () {
        player1Alive = true;
        player2Alive = true;

        scoreCap = StaticScript.roundCount;
        Invoke("findPlayers", 1);
        
        deathDetected = false;

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


        }
        

	}

    /*   void killPlayer(int player)
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
       */
    public void roundVictory()
    {
        Debug.Log("In round victory");
        loadingLevelCoRoutine.allowSceneActivation = true;
        /*foreach(GameObject charObj in players)
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

        if (player1Alive)
            StaticScript.player1Score++;

        else if (player2Alive)
            StaticScript.player2Score++;
        
        Debug.Log("Round is over");
        */



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
    

}
