using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingTextPulseEffect : MonoBehaviour {

    UnityEngine.UI.Text loadText;
    int count;
    bool loadingANewScene = false;
	// Use this for initialization
	void Start () {
        count = 0;
        loadText = GetComponentInChildren<UnityEngine.UI.Text>();

    }
	
	// Update is called once per frame
	void Update () {
        //don't attempt to load a scene if one hasn't been selected
        if((StaticScript.nextSceneToLoad!= null) && !loadingANewScene)
        {
            loadingANewScene = true;
            StartCoroutine(loadNewScene());

        }//end if

        if(Time.frameCount%60 == 0)
        {
            UpdateLoadText();

        }

	}

    IEnumerator loadNewScene()
    {
        InvokeRepeating("UpdateLoadText", 0, 1.0f);
        AsyncOperation levelLoad = SceneManager.LoadSceneAsync(StaticScript.nextSceneToLoad,LoadSceneMode.Single);

        while (!levelLoad.isDone)
        {
            yield return null;

        }//end while

    }//end loadNewScene

    void UpdateLoadText()
    {
        string textToLoad = "Loading";
        count++;
        textToLoad += new string('.', count);
        if (count == 3)
            count = 0;
        loadText.text = textToLoad;

    }//end void
}
