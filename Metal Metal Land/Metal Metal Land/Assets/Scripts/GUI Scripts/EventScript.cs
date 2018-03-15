using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EventScript : MonoBehaviour {

    public Button btnPlay;
    public Button btnChars;
    public Button btnOptions;
    public Button btnExit;

	// Use this for initialization
	void Start () {
        //add on click listeners to each button
        btnPlay.GetComponent<Button>().onClick.AddListener(playFunction);
        btnChars.GetComponent<Button>().onClick.AddListener(charsFunction);
        btnOptions.GetComponent<Button>().onClick.AddListener(optionsFunction);
        btnExit.GetComponent<Button>().onClick.AddListener(exitFunction);


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void playFunction()
    {
        Debug.Log("Play Button Clicked");
        StaticScript.nextSceneToLoad = "Scenes/procGenScene";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end playFuncton

    void charsFunction()
    {
        StaticScript.nextSceneToLoad = ("Spaghetti");

    }//end charsFunction

    void optionsFunction() {
        Debug.Log(StaticScript.nextSceneToLoad);

    }//end optionsFunction

    void exitFunction()
    {
        //quit the game
        Debug.Log("Exit button Clicked");
        Application.Quit();

    }//end exitFunction
}
