using UnityEngine;
using System.Collections;

public class ShotgunPelletBehaviour : MonoBehaviour {
    //apply force to bullet
    int dir;
    Rigidbody2D bulletRigBod2d;

    // Use this for initialization
    void Start () {
        dir = 1;

        bulletRigBod2d = gameObject.GetComponent<Rigidbody2D>();
        bulletRigBod2d.AddForce(transform.right * (10) *dir, ForceMode2D.Impulse);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //only cound collisions if they're not other shotgun pellets   
        if(coll.gameObject.tag.ToLower() != "ammunition")
        {
            if (coll.gameObject.tag.ToLower() == "environment")
            {
                coll.gameObject.GetComponent<CellBehaviourScript>().decreaseCellHealth();
                Destroy(gameObject);

            }//end if

            else if (coll.gameObject.tag.ToLower() == "bombbox")
            {
                coll.gameObject.GetComponent<BombBoxScript>().detonateBomb();
                Destroy(gameObject);

            }//end else if

            else if (coll.gameObject.tag.ToLower() == "player")
            {
                coll.gameObject.GetComponent<PlayerGameController>().killPlayer();
                Destroy(gameObject);

            }//end else if

            else
            {
                Debug.Log(coll.gameObject.name);
                Destroy(gameObject);

            }//end else 

        }//end if not shotgun pellet
        


    }//end On2DCollisionEnter

    public void setDir(int dir)
    {
        this.dir = dir;

    }

}
