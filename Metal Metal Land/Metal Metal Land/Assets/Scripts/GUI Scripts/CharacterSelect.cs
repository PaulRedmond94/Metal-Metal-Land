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

    // variable to hold what index to skip when moving through characters (Skip over character already selected by other player)
    int skipIndex;

    //boolean variables to determine if either player has picked their character 
    bool p1Confirm, p2Confirm;

    //boolean variables to determine if a player can move their character select icon
    //included to stop skipping over multiple characters easily
    bool p1CanChange, p2CanChange;


    //axis variables for controllers
    string p1move, p2move, p1ConfirmButton, p2ConfirmButton;

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
            p2IconXAxis.Add((charIconPos.GetComponent<RectTransform>().position.x) + 20);

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

        skipIndex = -1;

        p1CanChange = true;
        p2CanChange = true;

        p1move = "p1_move";
        p1ConfirmButton = "p1_jump";

        p2move = "p2_move";
        p2ConfirmButton = "p2_jump";

    }//end start

    // Update is called once per frame
    void Update() {
        bool p1Change = false;
        bool p2Change = false;

        // handle player 1's input
        if (!p1Confirm)
        {
            if (Input.GetAxis("p1_move")==1 && p1CanChange)
            {
                p1CanChange = false;
                p1CurrentCharacter++;
                if (p1CurrentCharacter > 7)
                    p1CurrentCharacter = 0;
                if (p1CurrentCharacter == skipIndex)
                    p1CurrentCharacter++;

                p1Change = true;
                StartCoroutine("allowPlayerToMove", 1);

            }//end if input is right

            else if (Input.GetAxis("p1_move")==-1 && p1CanChange)
            {
                p1CanChange = false;
                p1CurrentCharacter--;
                if (p1CurrentCharacter < 0)
                    p1CurrentCharacter = 7;

                if (p1CurrentCharacter == skipIndex)
                    p1CurrentCharacter--; 

                p1Change = true;
                StartCoroutine("allowPlayerToMove", 1);

            }//end p1 input
            else if (Input.GetAxis("p1_jump") == 1 && !p1Confirm)
            {
                p1Confirm = true;
                StaticScript.player1Character = characters[p1CurrentCharacter].getName();
                p1Icon.GetComponent<RectTransform>().position += new Vector3(0, -10.0f, 0);
                skipIndex = p1CurrentCharacter;
                if (!p2Confirm && p1CurrentCharacter == p2CurrentCharacter)
                {
                    lockIcon(1, ref p2CurrentCharacter);
                }

            }//end else if

        }//end if !p1Confirm 


        // handle player 2's input
        if (!p2Confirm && p2CanChange)
        {
            if (Input.GetAxis("p2_move")==1)
            {
                p2CanChange = false;
                p2CurrentCharacter++;
                if (p2CurrentCharacter > 7)
                    p2CurrentCharacter = 0;
                if (p2CurrentCharacter == skipIndex)
                    p2CurrentCharacter++;

                p2Change = true;
                StartCoroutine("allowPlayerToMove", 2);

            }//end if input is right

            else if (Input.GetAxis("p2_move")== -1)
            {
                p2CanChange = false;
                p2CurrentCharacter--;
                if (p2CurrentCharacter < 0)
                    p2CurrentCharacter = 7;
                if (p2CurrentCharacter == skipIndex)
                    p2CurrentCharacter--;

                p2Change = true;
                StartCoroutine("allowPlayerToMove", 2);

            }//end p2 input

            else if (Input.GetAxis("p2_jump") == 1 && !p2Confirm)
            {
                p2Confirm = true;
                StaticScript.player2Character = characters[p2CurrentCharacter].getName();
                p2Icon.GetComponent<RectTransform>().position += new Vector3(0, -10.0f, 0);
                if(!p1Confirm && p1CurrentCharacter == p2CurrentCharacter)
                    lockIcon(2, ref p1CurrentCharacter);

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
            StaticScript.player1Character = p1Image.GetComponent<Image>().sprite.name;
            StaticScript.player2Character = p2Image.GetComponent<Image>().sprite.name;
            
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
        string[] fileLines = text.Split('\n');
        //for each loop strips the data out of the lines and creates character objects for it
        foreach(string line in fileLines)
        {
            //seperate each line based on hash marks
            string[] attributes = line.Split('#');
            characters.Add(new Character(attributes[0], attributes[1], attributes[2]));            

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
                "\nNationality: " + characters[currChar].getNationality() +
                "\nGenre: " + characters[currChar].getGenre();

        }//end if

        else
        {
            pIcon.gameObject.transform.position = new Vector3(p2IconXAxis[currChar], p1Icon.gameObject.transform.position.y);
            pDesc.GetComponent<Text>().text =
                "Name: " + characters[currChar].getName() +
                "\nNationality: " + characters[currChar].getNationality() +
                "\nGenre: " + characters[currChar].getGenre();
        }//end else
        
    }//end updateProfileImages

    // function used to push other playericon off of a recently select profile
    // playerSubmitted is player who locked in
    // playerpos is the player who is to be moved
    void lockIcon(int playerSubmitted, ref int playerPos)
    {
        playerPos++;

        if (playerPos > 7)
        {
            playerPos = 0;

        }//end if

        if(playerSubmitted == 2)
        {
            updateProfiles(p1Image, p1Icon, p1DescText, p1CurrentCharacter);
        }
        else
        {
            updateProfiles(p2Image, p2Icon, p2DescText, p2CurrentCharacter);
        }

    }

    IEnumerator allowPlayerToMove(int player)
    {
        //wait for 1/10th of a second
        yield return new WaitForSeconds(0.1f);

        if(player == 1)
        {
            p1CanChange = true;

        }//end if player 1

        else
        {
            p2CanChange = true;

        }

        //return 
    }




}
