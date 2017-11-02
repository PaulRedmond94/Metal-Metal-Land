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
                collision.gameObject.GetComponent<DebugTestScripts>().respawn();

            }//end if

        }
    }
    
}
