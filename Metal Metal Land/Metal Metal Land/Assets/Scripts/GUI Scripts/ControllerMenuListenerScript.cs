using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerMenuListenerScript : MonoBehaviour {

    public Button returnButton;
    public Button gameObjectiveButton;

	// Use this for initialization
	void Start () {
        returnButton.GetComponent<Button>().onClick.AddListener(returnToMenu);
        gameObjectiveButton.GetComponent<Button>().onClick.AddListener(goToGameObjective);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void returnToMenu(){
        StaticScript.nextSceneToLoad = "Scenes/MainMenu";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }

    void goToGameObjective()
    {
        StaticScript.nextSceneToLoad = "Scenes/GameObjective";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }
}

