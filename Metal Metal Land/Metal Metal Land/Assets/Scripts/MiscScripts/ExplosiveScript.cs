using UnityEngine;
using System.Collections;

public class ExplosiveScript : MonoBehaviour {

    public static void detonateArray(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag.ToLower() == "environment")
            { 

                collider.GetComponent<CellBehaviourScript>().explode();

            }//end if environment

            else if (collider.tag.ToLower() == "player")
            {
                
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

    }

}
