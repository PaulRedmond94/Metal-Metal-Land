using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOptionsVolumeController : MonoBehaviour {

    public Slider sfxSlider;
    public Slider musicSlider;
    public Text sfxText;
    public Text musicText;

    public Button applyButton;

	// Use this for initialization
	void Start () {
        sfxSlider.value = StaticScript.sfxLevel;
        musicSlider.value = StaticScript.musicLevel;
        applyButton.onClick.AddListener(applyOptions);

	}
	
	// Update is called once per frame
	void Update () {
        sfxText.text = "" + sfxSlider.value;
        musicText.text = "" + musicSlider.value;

    }

    void applyOptions()
    {
        StaticScript.sfxLevel = (int)sfxSlider.value;
        StaticScript.musicLevel = (int)musicSlider.value;
        StaticScript.nextSceneToLoad = "Scenes/MainMenu";
        SceneManager.LoadScene("Scenes/LoadingManager", LoadSceneMode.Single);

    }//end applyOptions

}
