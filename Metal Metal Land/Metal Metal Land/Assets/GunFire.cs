using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(this.transform.parent!= null)
        {
            if (Input.GetKey("n") && this.transform.parent.tag == "Player")
            {
                Debug.Log("Bang");
                //Debug.DrawRay(, Vector2.right, Color.green, 1.0f, false);
                Debug.DrawRay(this.gameObject.transform.GetChild(0).position, Vector2.right * 10, Color.green, 1.0f);


            }//end if

        }//end if
        

	}//end update
}
