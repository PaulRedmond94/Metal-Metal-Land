using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectMainMenu : MonoBehaviour {

    public Button mainMenuButton;

	// Use this for initialization
	void Start () {
        mainMenuButton.GetComponent<Button>().onClick.AddListener(goToMainMenu);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void goToMainMenu()
    {
        StaticScript.nextSceneToLoad = "Scenes/MainMenu";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }
}
