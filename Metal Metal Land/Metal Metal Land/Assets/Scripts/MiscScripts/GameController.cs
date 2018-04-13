using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    bool skipBombDrop;


    AsyncOperation loadingLevelCoRoutine;
    GameObject[] players;

    List<GameObject> powerups = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        player1Alive = true;
        player2Alive = true;

        scoreCap = StaticScript.roundCount;
        Invoke("findPlayers", 1);
        
        deathDetected = false;
        suddenDeath = false;
        skipBombDrop = false;

        //powerups = Resources.LoadAll("Objects/Powerups") as List<GameObject>;
        foreach (GameObject powerupPrefab in Resources.LoadAll("Objects/Powerups"))
        {
            powerups.Add(powerupPrefab);

        }//end foreach

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
        if(suddenDeath && Time.frameCount%300 == 0 && !skipBombDrop)
        {
            dropItem(true);
            
        }//end if

        //used in the event that someone uses Dio's Essence Powerup to stop time
        if (skipBombDrop)
        {
            skipBombDrop = false;

        }

        //every 10 seconds potentially drop a powerup
        if(Time.frameCount%60 == 0)
        {
            if (1 == 1)
            {
                dropItem(false);

            }//end if

        }


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
                    StaticScript.nextSceneToLoad = "Scenes/VictoryScene";
                    SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

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

    void dropItem(bool droppingBomb)
    {
        //bool that determines if a bomb was dropped
        bool itemDropped = false;

        //gets array of current cells
        GameObject[,] cellArray = gameObject.GetComponent<ProceduralGenScript>().getLandCellArray();

        //gets bombbox prefab
        
        
        while(itemDropped == false)
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
                        if (droppingBomb)
                        {
                            GameObject bombBox = Resources.Load("Objects/BombBox") as GameObject;
                            GameObject droppedBomb = Instantiate(bombBox, gameObject.transform.position + cellArray[subRand, i].gameObject.transform.position + new Vector3(8, 10), gameObject.transform.rotation) as GameObject;
                            droppedBomb.GetComponent<BombBoxScript>().setFalling(true);

                        }//end if
                        else
                        {
                            GameObject droppedItem = Instantiate(getRandomPowerup(), gameObject.transform.position + cellArray[subRand, i].gameObject.transform.position + new Vector3(8, 10), gameObject.transform.rotation) as GameObject;

                        }//end else

                        itemDropped = true;

                        break;

                    }//end if

                }//end for loop to go through column

            }//end try
            catch(System.IndexOutOfRangeException ioore)
            {
                //only occurs when bombs are dropping
                Debug.LogError("You've run out of cells, game should end soon...");

            }//end catch
            
        }//end while

    }//end dropBomb

    GameObject getRandomPowerup()
    {
        return powerups[Random.Range(0, powerups.Count + 1)];

    }//end generatePowerup

    public void setSkipBombDrop()
    {
        skipBombDrop = true;

    }//end skipBomb
    

}
