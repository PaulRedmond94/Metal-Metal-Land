using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class musicController : MonoBehaviour {

    static List<AudioClip> songs = new List<AudioClip>();

    static AudioSource objMusicController;

    //variable to determine if this is the first time the object has been created
    static bool hasBeenCreatedBefore = false;

	// Use this for initialization
	void Awake () {
        if (hasBeenCreatedBefore == false)
        {

            hasBeenCreatedBefore = true;
            DontDestroyOnLoad(transform.gameObject);

            objMusicController = gameObject.GetComponent<AudioSource>();

            //load in all songs
            Object[] songFiles = Resources.LoadAll("Music/BattleMusic");
            foreach(Object song in songFiles)
            {
                songs.Add((AudioClip)song);

            }//end for each
        
        }// end if     
	
	}//end awake
	
	// Update is called once per frame
	void Update () {
        if (!objMusicController.isPlaying)
        {
            AudioClip song = getRandomSong();
            objMusicController.clip = song;
            objMusicController.Play();
            objMusicController.volume = 100;

        }//end if

        if (Input.GetKeyDown("m"))
        {
            objMusicController.Stop();
            AudioClip song = getRandomSong();
            objMusicController.clip = song;
            objMusicController.Play();

            Debug.Log("Now playing: " + song.name);

        }//end if
        
	}

    AudioClip getRandomSong()
    {
        int songIndex = Random.Range(1, songs.Count + 1);

        return songs[songIndex];

    }//end getRandomSong
}
