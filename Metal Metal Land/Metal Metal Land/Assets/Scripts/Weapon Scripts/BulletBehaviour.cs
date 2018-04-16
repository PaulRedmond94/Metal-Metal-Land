using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

    public int durability;

	// Use this for initialization
	void Start () {
        //if bullet durability is not specified, set it to 1
        if (durability.Equals(null) || durability == 0)
        {
            durability = 1;

            //apply force to bullet
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * (20), ForceMode2D.Impulse);

        }
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnCollisionEnter2D (Collision2D coll)
    {
        while (durability > 0)
        {

            if (coll.gameObject.tag.ToLower() == "environment")
            {
                coll.gameObject.GetComponent<CellBehaviourScript>().decreaseCellHealth();
                durability--;
                continue;

            }

            else if (coll.gameObject.tag.ToLower() == "bombbox")
            {
                coll.gameObject.GetComponent<BombBoxScript>().detonateBomb();
                durability = 0;
                break;

            }
           
            else if(coll.gameObject.tag.ToLower() == "player")
            {
                coll.gameObject.GetComponent<PlayerGameController>().killPlayer();
                durability--;

            }//end else if

            else
            {
                Debug.Log(coll.gameObject.name);
                durability--;
            }
            
        }

        if(durability == 0)
        {
            Destroy(gameObject);

        }
        

    }//end On2DCollisionEnter

}
