using UnityEngine;
using System.Collections;

public class InertWeaponAltarScript : MonoBehaviour {

    GameObject liveWeaponAltar = Resources.Load("Objects/weapon_altar_live") as GameObject;

	// Use this for initialization
	void Start () {
        Invoke("activateAltar", Random.Range(1.0f, 10.0f));
        	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void activateAltar()
    {
        Instantiate(liveWeaponAltar, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
        Debug.Log("Altar activated");
        

    }//end void
}
