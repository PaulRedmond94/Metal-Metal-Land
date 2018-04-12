using UnityEngine;
using System.Collections;

public class CharacterFloatScript : MonoBehaviour {

    float originalHeight;

    // Use this for initialization
    void Start()
    {
        originalHeight = this.transform.position.y;

    }//end start

    // Update is called once per frame
    void Update()
    {
        //sourced from here: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity
        this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)Mathf.Sin(Time.time)) * 30f);

    }//end update
}
