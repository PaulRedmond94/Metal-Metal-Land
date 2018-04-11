using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomCharacterImage : MonoBehaviour {

    Object[] characterImages;
    Sprite charSprite;

    float originalHeight;

    // Use this for initialization
    void Start () {
        characterImages = Resources.LoadAll("Images/CharacterArt/CharacterDefaultImage",typeof(Sprite));
        charSprite = (Sprite)characterImages[Random.Range(0, 7)];

        gameObject.GetComponent<Image>().sprite = charSprite;

        originalHeight = transform.position.y;
        

    }
	
	// Update is called once per frame
	void Update () {
        //sourced from here: https://gamedev.stackexchange.com/questions/96878/how-to-animate-objects-with-bobbing-up-and-down-motion-in-unity
        this.transform.position = new Vector3(this.transform.position.x, originalHeight + ((float)Mathf.Sin(Time.time)) * 10f);

        //sourced code ends here

    }
}
