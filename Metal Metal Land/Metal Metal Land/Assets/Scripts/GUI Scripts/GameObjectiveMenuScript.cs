using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameObjectiveMenuScript : MonoBehaviour {

    public Button controllerButton;
    public Button mainMenuButton;

	// Use this for initialization
	void Start () {
        controllerButton.GetComponent<Button>().onClick.AddListener(goToController);
        mainMenuButton.GetComponent<Button>().onClick.AddListener(goToMainMenu);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void goToController()
    {
        StaticScript.nextSceneToLoad = "Scenes/Controls";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end goToController

    void goToMainMenu()
    {
        StaticScript.nextSceneToLoad = "Scenes/MainMenu";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end goToMainMenu
}
