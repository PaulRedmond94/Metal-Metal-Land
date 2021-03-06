﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour {

    //gameobjects for player character icons
    public GameObject player1Image, player2Image;
    
    //gameobjects for player character names
    public GameObject player1Name, player2Name;

    //gameobjects for current player weapon
    public GameObject player1Weapon, player2Weapon;

    //gameobject/variable to control initial game countdown
    public GameObject countDownText;
    int countdownVal = 3;

    //gameobjects to show player scores
    public GameObject player1Score, player2Score;


    //array to hold reference to players object
    GameObject[] players;

    //Objects/variables for sudden death alert
    public GameObject suddenDeathText;

	// Use this for initialization
	void Start () {
        player1Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterIcons/" + StaticScript.player1Character);
        player2Image.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterIcons/" + StaticScript.player2Character);

        player1Name.GetComponent<Text>().text = "Player 1: " + StaticScript.player1Character;
        player2Name.GetComponent<Text>().text = "Player 2: " + StaticScript.player2Character;

        player1Score.GetComponent<Text>().text = "Score: " + StaticScript.player1Score;
        player2Score.GetComponent<Text>().text = "Score: " + StaticScript.player2Score;

        //initally hide sudden death text
        suddenDeathText.GetComponent<Text>().enabled = false;  

    }//end start
	
	// Update is called once per frame
	void Update () {

        if (Time.frameCount % 45 == 0)
        {
            countdownVal--;

        }//end if 


        if (countdownVal >= 1)
        {
            if (players == null)
            {
                players = GameObject.FindGameObjectsWithTag("Player");

            }//end if

            //disable player movement
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerMovement>().enabled = false;

            }
            countDownText.GetComponent<Text>().text = countdownVal + "";

        }//end if

        else if (countdownVal == 0)
        {
            countDownText.GetComponent<Text>().text = "GO!!!";

        }//end else if

        else if (countdownVal < 0)
        {
            bool hasMovementBeenReEnabled = false;
            if (!hasMovementBeenReEnabled)
            {
                foreach (GameObject player in players)
                {
                    player.GetComponent<PlayerMovement>().enabled = true;

                }//end foreach

            }//end if
            
            Destroy(countDownText.gameObject);

            hasMovementBeenReEnabled = true;
        }

    }

    public void updateWeapon(string playerObjectName, string weapon)
    {
        string currentWeaponText = "Current Weapon: ";
        if(playerObjectName == StaticScript.player1Character)
        {
            player1Weapon.GetComponent<Text>().text = currentWeaponText + weapon;

        }//end if
        else if(playerObjectName == StaticScript.player2Character)
        {
            player2Weapon.GetComponent<Text>().text = currentWeaponText + weapon;

        }//end else if

    }//end updateWeapon

    public void activateSuddenDeath()
    {
        suddenDeathText.GetComponent<Text>().enabled = true;

    }//end beginSuddenDeath

}
