/*
This is a script which makes a gun object float up and down while on an altar

*/

using UnityEngine;
using System.Collections;

public class GunFloatScript : MonoBehaviour {

    float originalHeight;

	// Use this for initialization
	void Start () {
        originalHeight = this.transform.position.y;

	}//end start
	
	// Update is called once per frame
	void Update () {
        //old version, works for ascending, breaks when it gets to peak
        //this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)(Time.time%10) * 0.15f));
        // new version, 
        //sourced from here: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity
        this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)Mathf.Sin(Time.time)) * 0.15f);

	}//end update

    
}//end class
