using UnityEngine;
using System.Collections;

//simple script for loading the new sprite for guns after picking them up
// MAY BE USELESS

public class GunLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	    

	}

    public void updateSprite(Sprite newSprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;

    }//end updateSprite
}
