using UnityEngine;
using System.Collections;

// Script which is used to control grenade explosions, its radius and fuse

public class GrenadeDetonation : MonoBehaviour {

    CircleCollider2D cirCol2D;
    GameObject explosionEffect;

	// Use this for initialization
	void Start () {
        Invoke("detonateGrenade", 2.5f);
        explosionEffect = Resources.Load("Objects/ExplosionEffects/GrenadeExplosionEffect") as GameObject;
        cirCol2D = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //function which controls grenade detonation
    void detonateGrenade()
    {
        //get all objects which fall inside explosion radius and pass all the objects to the explosion script and have it choose what to do with each object based on its type
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol2D.radius);
        ExplosiveScript.detonateArray(colliders);
        SpriteRenderer grenadeSprite = gameObject.GetComponent<SpriteRenderer>();

        //remove grenade sprite and replace it with the explosion animation
        grenadeSprite.sprite = null;
        Instantiate(explosionEffect, gameObject.GetComponent<PolygonCollider2D>().bounds.center, transform.rotation);
        Destroy(gameObject);

    }
}
