using UnityEngine;
using System.Collections;

public class HitDetectionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Env. Hit");
        if(this.gameObject.tag == "DangerousEnvironment")
        {
            Debug.Log("D. Env. Hit");
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<SpawnerScript>().respawn();

            }//end if

        }//end if
        else if(this.gameObject.tag == "BouncePad")
        {
            if(collision.gameObject.tag == "Player")
            {
                try
                {
                    Rigidbody2D playerRb2d = collision.gameObject.GetComponent<Rigidbody2D>();
                    playerRb2d.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
                }
                catch(MissingComponentException mce)
                {
                    Debug.Log("Error!!!!!!!!! Players rigidbody doesn't exist!!!!");

                }
                

            }//end if collision is a player

        }//end else if
    }
    
}
