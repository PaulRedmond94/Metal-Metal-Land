using UnityEngine;
using System.Collections;

public class CellBehaviourScript : MonoBehaviour {

    SpriteRenderer mySpriteRenderer;
    string cellTerrainType;
    int test = 0;
    
    
	// Use this for initialization
	void Awake () {
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();
        //mySpriteRenderer.color = new Color(255f, 0f, 0f, 1f); //sets color to red
        Debug.Log("Sprite dims: " + mySpriteRenderer.bounds.size);
        
        //Debug.Log("Color Changed");
        //Debug.Log("Cell created at " + this.transform.position);
        cellTerrainType = "ground";

    }

    void Start()
    {

    }//end start

    void Update()
    {

    }//end update
    
    void OnMouseDown()
    {
        Debug.Log("Clicked on");
        explode();
        

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
    public void explode()
    {
        Debug.Log("Kaboom!");
        Destroy(this.gameObject);
        
    }//end detonate
}
