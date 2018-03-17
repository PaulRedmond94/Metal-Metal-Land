using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGameScript : MonoBehaviour {

    Button beginButton;

	// Use this for initialization
	void Start () {
        beginButton = this.gameObject.GetComponent<Button>();
        beginButton.onClick.AddListener(beginGame);
    }//end start
	
	// Update is called once per frame
	void Update () {
	

	}

    void beginGame()
    {
        Debug.Log("Play Button Clicked");
        StaticScript.nextSceneToLoad = "Scenes/procGenTest";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end beginGame
}
