//script to control bomb box explosions, fuses, volatility and so on

using UnityEngine;
using System.Collections;

public class BombBoxScript : MonoBehaviour {

    //variable to determine if the bomb box is detonating, used to prevent stack overflow glitches when an explosion hits a bomb box
    public bool detonating;
    //used to determine if the box is falling. If boxes are falling, they will explode when they hit the ground
    bool falling;

    //physics parts of the game object
    Rigidbody2D bombBoxRigBox;
    CircleCollider2D cirCol;
    GameObject explosionEffect;

    // Use this for initialization
    void Start () {
        detonating = false;
        bombBoxRigBox = gameObject.GetComponent<Rigidbody2D>();
        explosionEffect = Resources.Load("Objects/ExplosionEffects/BombBoxExplosionEffect") as GameObject;
        cirCol = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //determine if box is falling
        if (bombBoxRigBox.velocity.y > 0)
        {
            falling = true;

        }//end if to determine falling
	
	}//end update

    public void detonateBomb()
    {
        detonating = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);
        ExplosiveScript.detonateArray(colliders);
        GameObject explosion = Instantiate(explosionEffect, gameObject.GetComponent<BoxCollider2D>().bounds.center, transform.rotation) as GameObject;
        explosion.transform.localScale = new Vector3(1.5f, 1.5f);

        Destroy(this.gameObject);

    }//end detonateBomb

    //collision function to blow up bomb box if block below it is destroyed
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (falling)
        {
            detonateBomb();

        }//end if

    }

    public void setFalling(bool falling)
    {
        this.falling = falling;

    }//end setFalling

    public bool getFalling()
    {
        return falling;

    }//end getFalling

}
