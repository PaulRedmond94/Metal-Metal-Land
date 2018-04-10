using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class musicController : MonoBehaviour {

    List<AudioClip> songs = new List<AudioClip>();

    AudioSource objMusicController;

    //variable to determine if this is the first time the object has been created
    static bool hasBeenCreatedBefore = false;

	// Use this for initialization
	void Awake () {
        if (hasBeenCreatedBefore == false)
        {
            Debug.Log("first time");
            hasBeenCreatedBefore = true;
            DontDestroyOnLoad(transform.gameObject);

            objMusicController = gameObject.GetComponent<AudioSource>();

            //load in all songs
            Object[] songFiles = Resources.LoadAll("Music/BattleMusic");
            Debug.Log(songFiles.Length);
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
            Debug.Log("NEW SONG: " + song.name);
            objMusicController.clip = song;
            objMusicController.Play();
            objMusicController.volume = 100;
            Debug.Log("Now Playing!!!");

        }//end if

        if (objMusicController.isPlaying)
        {
            Debug.Log("Music is playing");

        }

        if (Input.GetKeyDown("m"))
        {
            Debug.Log("Loading Song");
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
