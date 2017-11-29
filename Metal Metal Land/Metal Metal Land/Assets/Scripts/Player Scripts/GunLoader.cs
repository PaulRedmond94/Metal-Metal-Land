using UnityEngine;
using System.Collections;

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
