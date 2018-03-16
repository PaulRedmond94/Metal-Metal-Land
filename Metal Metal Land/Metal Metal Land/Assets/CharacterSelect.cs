using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

    Transform[] characters;
    int curCharacter;

	// Use this for initialization
	void Start () {
        characters = this.gameObject.GetComponentsInChildren<Transform>();
        curCharacter = 0;
        readInCharacterData();

	}//end start
	
	// Update is called once per frame
	void Update () {
        if (curCharacter < 0)
            curCharacter = 7;
        else if (curCharacter > 7)
            curCharacter = 0;

        if (Input.GetKey("right"))
            curCharacter++;
        else if (Input.GetKey("left"))
            curCharacter--;

	}//end update

    void readInCharacterData()
    {


    }//end readInCharacterData
}
