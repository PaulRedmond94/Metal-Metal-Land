﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

    //controls game logic for each round

    bool player1Alive;
    bool player2Alive;

    int scoreCap;
    int player1Score;
    int player2Score;

    bool deathDetected;
    bool loadLevel;

    //sudden death related bools
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
        catch(System.NullReferenceException nre)
        {
            //this catch is here in the event that the game has loaded the characters too slowly

        }

        //if sudden death is enabled, drop a bomb every 5 seconds from a random position
        if(suddenDeath && Time.frameCount%300 == 0)
        {
            dropBomb();
            
        }//end if
        

	}

    public void roundVictory()
    {
        Debug.Log("In round victory");
        
        foreach(GameObject charObj in players)
        {
            if (charObj.GetComponent<PlayerGameController>().getAlive())
            {
                Debug.Log(charObj.name);
                if(charObj.name.Replace("(Clone)","") == StaticScript.player1Character)
                {
                    StaticScript.player1Score++;
                    Debug.Log("Player 1 gets a point");
                }
                else
                {
                    StaticScript.player2Score++;
                    Debug.Log("Player 2 gets a point");

                }

                //check to see if the game should end, if it should load victory screen
                if(StaticScript.player1Score == StaticScript.roundCount
                    || StaticScript.player2Score == StaticScript.roundCount)
                {


                }//end if

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

    void dropBomb()
    {
        //bool that determines if a bomb was dropped
        bool bombDropped = false;

        //gets array of current cells
        GameObject[,] cellArray = gameObject.GetComponent<ProceduralGenScript>().getLandCellArray();

        //gets bombbox prefab
        GameObject bombBox = Resources.Load("Objects/BombBox") as GameObject;
        
        while(bombDropped == false)
        {
            int subRand = Random.Range(0, gameObject.GetComponent<ProceduralGenScript>().getTerrXLength() + 1);

            try
            {
                //loop through column rows to determine if there is a block to drop a bomb on
                for (int i = 0; i < gameObject.GetComponent<ProceduralGenScript>().getTerrYLength(); i++)
                {
                    if (cellArray[subRand, i] != null)
                    {
                        Debug.Log(cellArray[subRand, i].transform.position);
                        GameObject droppingBomb = Instantiate(bombBox, gameObject.transform.position + cellArray[subRand, i].gameObject.transform.position + new Vector3(8, 10), gameObject.transform.rotation) as GameObject;
                        droppingBomb.GetComponent<BombBoxScript>().setFalling(true);

                        bombDropped = true;
                        Debug.Log("Drop the bombshell");
                        break;

                    }//end if

                }//end for loop to go through column

            }
            catch(System.IndexOutOfRangeException ioore)
            {
                Debug.LogError("You've run out of cells, game should end soon...");

            }//end catch
            
        }//end while

    }//end dropBomb
    

}
