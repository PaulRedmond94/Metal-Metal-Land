using UnityEngine;
using System.Collections;

public class BouncePadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            try
            {
                Rigidbody2D playerRb2d = collision.gameObject.GetComponent<Rigidbody2D>();
                playerRb2d.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            }
            catch (MissingComponentException mce)
            {
                Debug.Log("Error!!!!!!!!! Players rigidbody doesn't exist!!!!");

            }//end catch


        }//end if collision is a player

       

    }
}
