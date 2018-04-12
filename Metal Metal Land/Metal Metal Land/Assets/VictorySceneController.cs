using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictorySceneController : MonoBehaviour {

    public GameObject winningPlayer;
    public GameObject losingPlayer;
    public GameObject winText;

	// Use this for initialization
	void Start () {
        //if player1 is the winner
        if (StaticScript.player1Score == StaticScript.roundCount)
        {
            winningPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterDefaultImage/" + StaticScript.player1Character);
            losingPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterDefaultImage/" + StaticScript.player2Character);
            winText.GetComponent<Text>().text = "Player 1 is the winner!";

        }
        //player 2 is the winner
        else
        {
            winningPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterDefaultImage/" + StaticScript.player2Character);
            losingPlayer.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CharacterArt/CharacterDefaultImage/" + StaticScript.player1Character);
            winText.GetComponent<Text>().text = "Player 2 is the winner!";

        }//end else

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
