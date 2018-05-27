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

    }

    //activate powerup is called when player touches powerup object, it passes the first 3 letters of the powerup name
    public void activatePowerup(string power)
    {
        //if the power is skyhunter wings (bigger jumps)
        if (power.ToLower() == "sky")
        {
            gameObject.GetComponent<PlayerMovement>().setJumpModifier(1.5f);
            StartCoroutine("deactivatePowerup", "sky"); 

        }//end if power is skyhunter wings

        //if the power is essence of dio (time stop)
        else if (power.ToLower() == "dio")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject frozenPlayer = null;
            foreach (GameObject player in players)
            {
                if (player.name != gameObject.name)
                {
                    player.GetComponent<Rigidbody2D>().isKinematic = true;
                    player.GetComponent<SpriteRenderer>().material = negativeMaterial;
                    frozenPlayer = player;
                }
            }

            StartCoroutine("deactivatePowerup", frozenPlayer);

        }//end if dio powerup

        //if the power is run to the hills (faster run speed)
        else if (power.ToLower() == "run")
        {
            gameObject.GetComponent<PlayerMovement>().setSpeedModifier(1.5f);
            StartCoroutine("deactivatePowerup", "run");

        }//end if power is run to the hills

    }//end activatePowerup

    //function to revert power up changes
    IEnumerator deActivatePowerup(string power)
    {
        yield return new WaitForSeconds(5.0f);
        if (power.ToLower() == "sky")
        {
            gameObject.GetComponent<PlayerMovement>().setJumpModifier(1.0f);

        }//end if sky
        else if (power.ToLower() == "run")
        {
            gameObject.GetComponent<PlayerMovement>().setSpeedModifier(1.0f);

        }//end else if

    }//end deactivatePowerup

    IEnumerator deactivatePowerup(GameObject victim)
    {
        yield return new WaitForSeconds(5.0f);
        victim.GetComponent<Rigidbody2D>().isKinematic = false;
        victim.GetComponent<SpriteRenderer>().material = originalMaterial;

    }
}
