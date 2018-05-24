/*
    This is a script which determines bullet behaviour such as velocity and time to live

*/

using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    float timeToLive;
    bool faceLeft = true;
    int dir = 1;
    // Use this for initialization
    void Start()
    {
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * (100 * dir));

    }//end start

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, timeToLive);

    }//end update

    public void setDir(int dir)
    {
        this.dir = dir;

    }//end sayHello

    public int getDir()
    {
        return this.dir;

    }

    public void setTimeToLive(string weapon)
    {
        if (weapon.ToLower() == "pistol")
        {
            this.timeToLive = 1.0f;

        }//end if
        else if (weapon.ToLower() == "shotgun")
        {
            this.timeToLive = 0.25f;

        }//end if
        else if (weapon.ToLower() == "m16")
        {
            this.timeToLive = 0.75f;

        }
        else if(weapon.ToLower() == "minigun")
        {
            this.timeToLive = 0.75f;
            
        }//end else if
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ammunition")
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());

        }//end if object is ammunition

        else if(collision.gameObject.tag == "BombBox")
        {


        }


        else
        {
            Debug.Log("Bullet Hit");
            //Destroy(this.gameObject);

        }//end else
    }//end onCollisionEnter

}//end setTimeToLive

    /*public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Ammunition")
        {
            //Destroy(this.gameObject);
        }//end if
        Debug.Log("Hit: " + collision.tag);
    }*/




















    /*float timeToLive;
    bool faceLeft = true;
    Vector3 dir = new Vector3(0,0 , 0);
	// Use this for initialization
	void Start () {
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(Vector2.right + " " + this.gameObject.transform.position);
        rb.AddForce( (this.transform.position * 10.0f));
        Debug.Log("Bullet Created");

    }//end start
	
	// Update is called once per frame
	void Update () {
        

	}//end update

    public void setDir(Vector3 dir)
    {
        this.dir = dir;

    }//end setDir

    
    public Vector3 getDir()
    {
        return this.dir;

    }
    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ammunition")
        {
            Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());

        }//end if object is ammunition

        else
        {
            Debug.Log("Bullet Hit");
            //Destroy(this.gameObject);

        }//end else
    }//end onCollisionEnter

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Ammunition")
        {
            //Destroy(this.gameObject);
        }//end if
    }

    */




