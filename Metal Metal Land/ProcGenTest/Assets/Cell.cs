using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    int colourIntensity;
    int size;
    GameObject cellBody;
    //begin constructor
	public Cell(int size)
    {
        colourIntensity = Random.Range(0, 256);
        cellBody = new GameObject();
        this.size = size;
        cellBody.transform.localScale = (new Vector2(size,size));
        

    }//end constructor
}
