﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGameScript : MonoBehaviour {

    Button beginButton;
    GameObject roundCounter;

	// Use this for initialization
	void Start () {
        beginButton = this.gameObject.GetComponent<Button>();
        beginButton.onClick.AddListener(beginGame);
        roundCounter = GameObject.Find("RoundValText");

    }//end start
	
	// Update is called once per frame
	void Update () {
	

	}

    void beginGame()
    {
        //reset player scores and tell loading manager to load up
        int rounds;

        //code will succesfully parse 10-50 rounds, however, a val of 51 changes the value to "endless",
        //this causes the catch to trigger and set the rounds to max
        try
        {
            rounds = int.Parse(roundCounter.GetComponent<Text>().text);
        }
        catch (System.FormatException formatException)
        {
            rounds = int.MaxValue;
            
        }//end catch

        StaticScript.roundCount = rounds;
        StaticScript.player1Score = 0;
        StaticScript.player2Score = 0;
        StaticScript.nextSceneToLoad = "Scenes/procGenScene";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end beginGame
}
