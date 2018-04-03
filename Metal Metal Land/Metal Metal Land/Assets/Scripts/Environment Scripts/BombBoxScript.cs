//script to control bomb box explosions, fuses, volatility etc.

using UnityEngine;
using System.Collections;

public class BombBoxScript : MonoBehaviour {

    public bool detonating;
    //TODO Bring in feature where if box falls, it will detonate when it hits ground
    Rigidbody2D bombBoxRigBox;
    CircleCollider2D cirCol;

	// Use this for initialization
	void Start () {
        detonating = false;
        bombBoxRigBox = gameObject.GetComponent<Rigidbody2D>();
        cirCol = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void detonateBomb()
    {
        detonating = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag.ToLower() == "environment")
            {
                Debug.Log("Count incremented");

                collider.GetComponent<CellBehaviourScript>().explode();

            }//end if environment

            else if (collider.tag.ToLower() == "player") {
                Debug.Log("Player Killed");
                collider.GetComponent<PlayerGameController>().killPlayer();

            } //end else if player

            else if (collider.tag.ToLower() == "livealtar")
            {
                Destroy(collider);

            }//end else if livealtar

            else if (collider.tag.ToLower() == "inertaltar")
            {
                Destroy(collider);

            }//end else if inertaltar

            else if (collider.gameObject.tag.ToLower() == "bombbox")
            {
                //verify bomb is not currently exploding in order to avoid stack overflow errors
                if(collider.gameObject.GetComponent<BombBoxScript>().detonating == false)
                {
                    collider.gameObject.GetComponent<BombBoxScript>().detonateBomb();

                }//end if to check if bombs 
                
            }// end else if bombbox
        }//end foreach 
        
        Destroy(this.gameObject);

    }//end detonateBomb

}
