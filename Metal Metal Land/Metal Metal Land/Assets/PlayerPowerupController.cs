using UnityEngine;
using System.Collections;

public class PlayerPowerupController : MonoBehaviour {

    public bool runToTheHillsActive;
    public bool skyhunterWingsActive;
    public bool essenceOfDIOActive;
    public bool fatBottomedBulletsActive;

	// Use this for initialization
	void Start () {
        runToTheHillsActive = false;
        skyhunterWingsActive = false;
        essenceOfDIOActive = false;
        fatBottomedBulletsActive = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("i"))
        {
            Debug.Log("You can fly now");
            activatePowerup("sky");

        }//end if

        else if (Input.GetKeyDown("o"))
        {
            Debug.Log("You can run now");
            activatePowerup("run");

        }//end if

        else if (Input.GetKeyDown("p"))
        {
            Debug.Log("ZA WARUUUUUUUDO");
            activatePowerup("dio");

        }


    }

    //activate powerup is called when player touches powerup object, it passes the first 3 letters of the powerup name
    public void activatePowerup(string power)
    {
        //if the power is skyhunter wings (bigger jumps)
        if(power.ToLower() == "sky")
        {
            gameObject.GetComponent<PlayerMovement>().setJumpModifier(1.5f);

        }//end if power is skyhunter wings
        /*
        //if the power is fat bottom bullets (larger projectiles)
        else if(power.ToLower() == "fat")
        {


        }
        */
        //if the power is essence of dio (time stop)
        else if(power.ToLower() == "dio")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players)
            {
                if (gameObject.name.Contains("Ailbhe"))
                {
                    if (player.name != gameObject.name)
                    {
                        player.GetComponent<SpriteRenderer>().color = new Color(255,0,0);

                    }//end if
                }

                

            }//end foreach to cycle through

        }//end if power is essence of dio
        //if the power is run to the hills (faster run speed)
        else if(power.ToLower() == "run")
        {
            gameObject.GetComponent<PlayerMovement>().setSpeedModifier(1.5f);

        }//end if power is run to the hills

    }//end activatePowerup

    //function to revert power up changes
    void deActivatePowerup(string power)
    {
        if(power.ToLower() == "sky")
        {
            gameObject.GetComponent<PlayerMovement>().setJumpModifier(1.0f);

        }//end if sky
        else if(power.ToLower()== "run")
        {
            gameObject.GetComponent<PlayerMovement>().setSpeedModifier(1.0f);

        }//end else if
        else if (power.ToLower() == "dio")
        {


        }//end else if

    }
}
