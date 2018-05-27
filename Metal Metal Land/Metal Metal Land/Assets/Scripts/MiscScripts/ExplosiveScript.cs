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
                collider.GetComponent<LiveWeaponAltarScript>().removeAltar();

            }//end else if livealtar

            else if (collider.tag.ToLower() == "inertaltar")
            {
                collider.GetComponent<InertWeaponAltarScript>().removeAltar();

            }//end else if inertaltar

            else if (collider.gameObject.tag.ToLower() == "bombbox")
            {
                //verify bomb is not currently exploding in order to avoid stack overflow errors
                if (!collider.gameObject.GetComponent<BombBoxScript>().detonating)
                    collider.gameObject.GetComponent<BombBoxScript>().detonateBomb();

            }// end else if bombbox

            else if (collider.gameObject.tag.ToLower() == "powerup")
            {
                Destroy(collider.gameObject);

            }

        }//end for each

    }

}
