using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class musicController : MonoBehaviour {

    List<AudioClip> songs = new List<AudioClip>();

    //variable to determine if this is the first time the object has been created
    static bool hasBeenCreatedBefore = false;

	// Use this for initialization
	void Awake () {
        if (!hasBeenCreatedBefore)
        {

            DontDestroyOnLoad(gameObject);

            Object[] songFiles = Resources.LoadAll("Music/BattleMusic");
            foreach(Object song in songFiles)
            {
                songs.Add((AudioClip)song);

            }//end for each

            

        }// end if     
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Songs loaded" + songs.Count);
	}
}
