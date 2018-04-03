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

        }
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnCollisionEnter2D (Collision2D coll)
    {
        while (durability > 0)
        {
            Debug.Log(coll.gameObject.name);
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
           
            else if(coll.gameObject.tag.ToLower() == "Player")
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
