using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour {

    public Button returnButton;

    // Use this for initialization
    void Start()
    {
        returnButton.GetComponent<Button>().onClick.AddListener(returnToMenu);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void returnToMenu()
    {
        StaticScript.nextSceneToLoad = "Scenes/MainMenu";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }
}
