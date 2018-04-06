using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGameScript : MonoBehaviour {

    Button beginButton;
    GameObject roundCounter;

	// Use this for initialization
	void Start () {
        beginButton = this.gameObject.GetComponent<Button>();
        beginButton.onClick.AddListener(beginGame);
        roundCounter = GameObject.Find("RoundValText");

    }//end start
	
	// Update is called once per frame
	void Update () {
	

	}

    void beginGame()
    {
        StaticScript.roundCount = System.Int32.Parse(roundCounter.GetComponent<Text>().text);
        StaticScript.player1Score = 0;
        StaticScript.player2Score = 0;
        StaticScript.nextSceneToLoad = "Scenes/procGenScene";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end beginGame
}
