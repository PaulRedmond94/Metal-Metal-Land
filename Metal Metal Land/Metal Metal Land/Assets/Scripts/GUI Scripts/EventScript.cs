using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EventScript : MonoBehaviour {

    public Button btnPlay;
    public Button btnObjective;
    public Button btnControls;
    public Button btnOptions;
    public Button btnExit;

	// Use this for initialization
	void Start () {
        //add on click listeners to each button
        btnPlay.GetComponent<Button>().onClick.AddListener(playFunction);
        btnObjective.GetComponent<Button>().onClick.AddListener(objectiveFunction);
        btnControls.GetComponent<Button>().onClick.AddListener(controlsFunction);
        btnOptions.GetComponent<Button>().onClick.AddListener(optionsFunction);
        btnExit.GetComponent<Button>().onClick.AddListener(exitFunction);


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void playFunction()
    {
        StaticScript.nextSceneToLoad = "Scenes/CharacterSelect";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end playFuncton

    void objectiveFunction()
    {
        StaticScript.nextSceneToLoad = "Scenes/GameObjective";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end ObjectiveFunction

    void controlsFunction()
    {
        StaticScript.nextSceneToLoad = ("Scenes/Controls");
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end charsFunction

    void optionsFunction() {
        StaticScript.nextSceneToLoad = ("Scenes/GameOptions");
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end optionsFunction

    void exitFunction()
    {
        //quit the game
        Debug.Log("Exit button Clicked");
        Application.Quit();

    }//end exitFunction
}
