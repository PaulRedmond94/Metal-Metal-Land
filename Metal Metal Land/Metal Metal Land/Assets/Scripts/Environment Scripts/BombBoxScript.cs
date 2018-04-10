//script to control bomb box explosions, fuses, volatility etc.

using UnityEngine;
using System.Collections;

public class BombBoxScript : MonoBehaviour {

    public bool detonating;
    //TODO Bring in feature where if box falls, it will detonate when it hits ground
    Rigidbody2D bombBoxRigBox;
    CircleCollider2D cirCol;
    GameObject explosionEffect;

    // Use this for initialization
    void Start () {
        detonating = false;
        bombBoxRigBox = gameObject.GetComponent<Rigidbody2D>();
        explosionEffect = Resources.Load("Objects/ExplosionEffect") as GameObject;
        cirCol = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void detonateBomb()
    {
        detonating = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);
        ExplosiveScript.detonateArray(colliders);
        GameObject explosion = Instantiate(explosionEffect, gameObject.GetComponent<BoxCollider2D>().bounds.center, transform.rotation) as GameObject;
        explosion.transform.localScale = new Vector3(1.5f, 1.5f);


        Destroy(this.gameObject);

    }//end detonateBomb

}
