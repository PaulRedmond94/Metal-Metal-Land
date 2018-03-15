using UnityEngine;
using System.Collections;

public class CellBehaviourScript : MonoBehaviour {

    SpriteRenderer mySpriteRenderer;
    string cellTerrainType;
    int test = 0;
    
	// Use this for initialization
	void Awake () {
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();
        mySpriteRenderer.color = new Color(255f, 0f, 0f, 1f); //sets color to red
        //Debug.Log("Color Changed");
        //Debug.Log("Cell created at " + this.transform.position);
        cellTerrainType = "ground";

    }

    void Start()
    {

    }//end start

    void Update()
    {
        /*if (test == 0)
            Debug.Log("boop");

        test++;
        */
    }//end update
    
    void OnMouseDown()
    {
        Debug.Log("Clicked on");
        Destroy(this.gameObject);

    }// end OnMouseDown

    //setters and getters
    public string getCellTerrainType()
    {
        return cellTerrainType;
    }
    public void setCellTerrainType(string cellTerrainType)
    {
        this.cellTerrainType = cellTerrainType;

    }
}
