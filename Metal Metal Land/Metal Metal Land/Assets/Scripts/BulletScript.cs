using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    float lifetime = 1.0f;
    bool faceLeft = true;
    int dir = 0;
	// Use this for initialization
	void Start () {
        Rigidbody2D rb;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * (100*dir));
        Debug.Log("Bullet Created");

    }//end start

    void Awake()
    {
        Destroy(this.gameObject, lifetime);

    }//end awake
	
	// Update is called once per frame
	void Update () {
	

	}//end update

    public void setDir(int dir)
    {
        this.dir = dir;

    }//end sayHello
    
    public int getDir()
    {
        return this.dir;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet Hit");
        Destroy(this.gameObject);

    }//end onCollisionEnter

}
