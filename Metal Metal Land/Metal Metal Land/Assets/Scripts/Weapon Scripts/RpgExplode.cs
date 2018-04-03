using UnityEngine;
using System.Collections;

public class RpgExplode : MonoBehaviour {

    CircleCollider2D cirCol;

    // Use this for initialization
    void Start () {
        cirCol = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Your head a splode");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag.ToLower() == "environment")
            {
                Debug.Log("Count incremented");

                collider.GetComponent<CellBehaviourScript>().explode();

            }//end if environment

            else if (collider.tag.ToLower() == "player")
            {
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
                collider.gameObject.GetComponent<BombBoxScript>().detonateBomb();

            }// end else if bombbox

        }//end for each

        Destroy(this.gameObject);
        
    }//end OnCollisionEnter2D
}
