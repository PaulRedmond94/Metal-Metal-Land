using UnityEngine;
using System.Collections;

public class GunFloat : MonoBehaviour {

    float originalHeight;
    SpriteRenderer gunSprite;

    // Use this for initialization
    void Start()
    {
        originalHeight = this.transform.position.y;
        gunSprite = this.GetComponent<SpriteRenderer>();
        //GameObject gunToLoad = transform.parent.gameObject.GetComponent<LiveWeaponAltarScript>().getAltarWeapon();
        //

    }//end start

    // Update is called once per frame
    void Update()
    {
        //old version, works for ascending, breaks when it gets to peak
        //this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)(Time.time%10) * 0.15f));
        //sourced from here: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity
        this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)Mathf.Sin(Time.time)) * 0.075f);

    }//end update
}
