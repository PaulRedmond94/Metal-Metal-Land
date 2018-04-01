using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D (Collision2D coll)
    {
        if(coll.gameObject.tag.ToLower() == "environment")
        {
            coll.gameObject.GetComponent<CellBehaviourScript>().decreaseCellHealth();
            Destroy(gameObject);

        }

    }//end On2DCollisionEnter

}
