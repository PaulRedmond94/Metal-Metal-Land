using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

    //List to hold character objects so their data can be displayed
    List<Character> characters = new List<Character>();

    //public to directly load in images
    public List<Sprite> characterProfileImages;

    //List to hold character game object positions
    List<GameObject> charIcons;

    //variables which hold the current position of the users cursor
    int p1CurrentCharacter;
    int p2CurrentCharacter;

    //lists to hold the x axis position of where the players icon should move to
    List<float> p1IconXAxis;
    List<float> p2IconXAxis;

    //variables to access and manipulate each players character profile game object and descriptions
    GameObject p1Image, p1Icon, p1DescText;
    GameObject p2Image, p2Icon, p2DescText;

    //boolean variables to determine if either player has picked their character 
    bool p1Confirm;
    bool p2Confirm;

	// Use this for initialization
	void Start () {
        p1IconXAxis = new List<float>();
        p2IconXAxis = new List<float>();

        charIcons = GameObject.FindGameObjectsWithTag("CharacterIcon").ToList();
        charIcons.Sort(delegate (GameObject originalObject, GameObject comparingObject)
        {
            return (originalObject.transform.position.x).CompareTo(comparingObject.transform.position.x);

        });//end sort

        //for loop to populate the P1Icon and P2Icon XAxis Lists

        foreach (GameObject charIconPos in charIcons)
        {
            p1IconXAxis.Add((charIconPos.GetComponent<RectTransform>().position.x) - 20);
            p2IconXAxis.Add((charIconPos.GetComponent<RectTransform>().position.x) + 30);

        }//end foreach 

        //assign variables
        p1CurrentCharacter = 0;
        p2CurrentCharacter = 7;
        p1Image = GameObject.Find("Player1Image");
        p2Image = GameObject.Find("Player2Image");
        p1Icon = GameObject.Find("Player1Icon");
        p2Icon = GameObject.Find("Player2Icon");
        p1DescText = GameObject.Find("p1DescText");
        p2DescText = GameObject.Find("p2DescText");
        p1Confirm = false;
        p2Confirm = false;

        //call function to read in character details from text file 
        readInCharacterData();



        //load in initial values
        p1DescText.GetComponent<Text>().text =
            "Name: " + characters[p1CurrentCharacter].getName() +
            //"\nBio: " + characters[p1CurrentCharacter].getBio() +
            "\nNationality: " + characters[p1CurrentCharacter].getNationality() +
            "\nGenre: " + characters[p1CurrentCharacter].getGenre();

        p2DescText.GetComponent<Text>().text =
            "Name: " + characters[p2CurrentCharacter].getName() +
            //"\nBio: " + characters[p2CurrentCharacter].getBio() +
            "\nNationality: " + characters[p2CurrentCharacter].getNationality() +
            "\nGenre: " + characters[p2CurrentCharacter].getGenre();


    }//end start

    // Update is called once per frame
    void Update() {
        bool p1Change = false;
        bool p2Change = false;

        // handle player 1's input
        if (!p1Confirm)
        {
            if (Input.GetKeyDown("right"))
            {
                p1CurrentCharacter++;
                if (p1CurrentCharacter > 7)
                    p1CurrentCharacter = 0;

                p1Change = true;

            }//end if input is right

            else if (Input.GetKeyDown("left"))
            {
                p1CurrentCharacter--;
                if (p1CurrentCharacter < 0)
                    p1CurrentCharacter = 7;

                p1Change = true;

            }//end p1 input
            else if (Input.GetKeyDown("space") && p1Confirm != true)
            {
                p1Confirm = true;
                p1Icon.GetComponent<RectTransform>().position += new Vector3(0, -10.0f, 0);

            }//end else if

        }//end if !p1Confirm 


        // handle player 2's input
        if (!p2Confirm)
        {
            if (Input.GetKeyDown("up"))
            {
                p2CurrentCharacter++;
                if (p2CurrentCharacter > 7)
                    p2CurrentCharacter = 0;

                p2Change = true;

            }//end if input is right

            else if (Input.GetKeyDown("down"))
            {
                p2CurrentCharacter--;
                if (p2CurrentCharacter < 0)
                    p2CurrentCharacter = 7;

                p2Change = true;

            }//end p2 input

            else if (Input.GetKeyDown("return") && p2Confirm != true)
            {
                p2Confirm = true;
                p2Icon.GetComponent<RectTransform>().position += new Vector3(0, -10.0f, 0);


            }//end else if

        }//end if !p2Confirm

        if (p1Change)
        {
            updateProfiles(p1Image, p1Icon, p1DescText, p1CurrentCharacter);

        }//end if p1Change

        if (p2Change)
        {
            updateProfiles(p2Image, p2Icon, p2DescText, p2CurrentCharacter);

        }//end if p2Change

        //if both players characters are confirmed
        if (p1Confirm && p2Confirm)
        {
            Debug.Log("Both players ready to go, load scene here");
            StaticScript.nextSceneToLoad = "Scenes/MatchOptions";
            SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

        }//end if

        
	}//end update

    void readInCharacterData()
    {
        //Stream reader for reading character details
        StreamReader streamReader = new StreamReader("Assets/Resources/TextFiles/characterDetails.txt");

        string text = streamReader.ReadToEnd();
        //split text into lines
        string[] fileLines = text.Split('\r');
        //for each loop strips the data out of the lines and creates character objects for it
        foreach(string line in fileLines)
        {
            //seperate each line based on hash marks
            string[] attributes = line.Split('#');
            characters.Add(new Character(attributes[0], attributes[2], attributes[3]));            

        }//end foreach

    }//end readInCharacterData

    void updateProfiles(GameObject img, GameObject pIcon, GameObject pDesc, int currChar)
    {
        img.GetComponent<Image>().sprite = characterProfileImages[currChar];
        if (pIcon.name == "Player1Icon")
        {
            pIcon.gameObject.transform.position = new Vector3(p1IconXAxis[currChar], p1Icon.gameObject.transform.position.y);
            pDesc.GetComponent<Text>().text =
                "Name: " + characters[currChar].getName() +
                //"\nBio: " + characters[currChar].getBio() +
                "\nNationality: " + characters[currChar].getNationality() +
                "\nGenre: " + characters[currChar].getGenre();

        }//end if

        else
        {
            pIcon.gameObject.transform.position = new Vector3(p2IconXAxis[currChar], p1Icon.gameObject.transform.position.y);
            pDesc.GetComponent<Text>().text =
                "Name: " + characters[currChar].getName() +
                //"\nBio: " + characters[currChar].getBio() +
                "\nNationality: " + characters[currChar].getNationality() +
                "\nGenre: " + characters[currChar].getGenre();
        }//end else
        Debug.Log(pIcon.GetComponent<RectTransform>().position);


    }//end updateProfileImages

}
