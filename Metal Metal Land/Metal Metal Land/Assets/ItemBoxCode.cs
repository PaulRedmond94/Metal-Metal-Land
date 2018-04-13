using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemBoxCode : MonoBehaviour {

    float objectFloatSpeed;
    List<GameObject> freezePlayers;
    GameObject john;
    Material originalMaterial;
    Material negativeMaterial;

    // Use this for initialization
    void Start () {
        objectFloatSpeed = 0.5f;
        originalMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        negativeMaterial = Resources.Load<Material>("Shaders/NegativeMaterial");
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.25f)
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-0.25f);

        if (Input.GetKeyDown("a"))
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = !gameObject.GetComponent<Rigidbody2D>().isKinematic;
            john = GameObject.FindGameObjectWithTag("BombBox");
            john.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<SpriteRenderer>().material = negativeMaterial;
            Invoke("timeMovesAgain", 3.0f);

            

        }
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.ToLower() == "player")
        {
            //coll.gameObject.GetComponent<PlayerPowerUpController>()

        }//end if

    }//end OnTriggerEnter2D

    void timeMovesAgain()
    {
        john.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<SpriteRenderer>().material = originalMaterial;

    }
}
