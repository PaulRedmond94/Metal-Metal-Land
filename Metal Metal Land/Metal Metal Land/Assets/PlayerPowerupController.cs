using UnityEngine;
using System.Collections;

public class PlayerPowerupController : MonoBehaviour {


    Material originalMaterial, negativeMaterial;

	// Use this for initialization
	void Start () {


        originalMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        negativeMaterial = Resources.Load<Material>("Shaders/NegativeMaterial");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("i"))
        {
            activatePowerup("dio");

        }

    }

    //activate powerup is called when player touches powerup object, it passes the first 3 letters of the powerup name
    public void activatePowerup(string power)
    {
        //if the power is skyhunter wings (bigger jumps)
        if (power.ToLower() == "sky")
        {
            gameObject.GetComponent<PlayerMovement>().setJumpModifier(1.5f);

        }//end if power is skyhunter wings

        //if the power is essence of dio (time stop)
        else if (power.ToLower() == "dio")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject frozenPlayer;
            foreach (GameObject player in players)
            {

                Debug.Log(gameObject.name + ", " + player.name);

                if (player.name != gameObject.name)
                {
                    player.GetComponent<Rigidbody2D>().isKinematic = true;
                    player.GetComponent<SpriteRenderer>().material = negativeMaterial;
                    frozenPlayer = player;
                }
            }
        }
        //if the power is run to the hills (faster run speed)
        else if (power.ToLower() == "run")
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
