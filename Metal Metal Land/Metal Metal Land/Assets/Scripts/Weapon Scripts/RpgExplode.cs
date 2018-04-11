using UnityEngine;
using System.Collections;

public class RpgExplode : MonoBehaviour {

    CircleCollider2D cirCol;
    GameObject explosionEffect;

    // Use this for initialization
    void Start () {
        cirCol = gameObject.GetComponent<CircleCollider2D>();
        explosionEffect = Resources.Load("Objects/ExplosionEffects/RPGExplosionEffect") as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);
        ExplosiveScript.detonateArray(colliders);
        Instantiate(explosionEffect, gameObject.GetComponent<BoxCollider2D>().bounds.center, transform.rotation);

        Destroy(this.gameObject);
        
    }//end OnCollisionEnter2D
}
