using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour {

    //Stream reader for reading character details
    StreamReader streamReader = new StreamReader("Assets/Resources/TextFiles/characterDetails.txt");

    //List to hold character objects
    List<Character> characters = new List<Character>();

    //array to hold character game object positions
    Transform[] guiCharacters;
    int curCharacter;

	// Use this for initialization
	void Start () {
        guiCharacters = this.gameObject.GetComponentsInChildren<Transform>();
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
        string text = streamReader.ReadToEnd();
        //split text into lines
        string[] fileLines = text.Split('\r');
        //for each loop strips the data out of the lines and creates character objects for it
        foreach(string line in fileLines)
        {
            //seperate each line based on hash marks
            string[] attributes = line.Split('#');
            characters.Add(new Character(attributes[0], attributes[1], attributes[2], attributes[3]));            



        }//end foreach

        //debug to determine if all data came through okay
        foreach(Character character in characters)
        {
            Debug.Log(character.toString());
        }

    }//end readInCharacterData
}
