using UnityEngine;
using System.Collections;

public class CellBehaviourScript : MonoBehaviour {

    SpriteRenderer mySpriteRenderer;
    string cellTerrainType;
    int cellHealth;
    Sprite undamaged;
    Sprite cracked;
    Sprite damaged;
    Sprite crumbling;
    Sprite destroyed;
    
	// Use this for initialization
	void Awake () {
        cellHealth = 5;
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();
        cellTerrainType = "ground";

    }

    void Start()
    {

    }//end start

    void Update()
    {
        if (cellHealth == 0)
        {
            explode();

        }//end if

    }//end update
    
    void OnMouseDown()
    {
        Debug.Log("Clicked on");
        decreaseCellHealth();
        

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

    public void setCellHealth(int cellHealth)
    {
        this.cellHealth = cellHealth;

    }//end setCellHealth

    public void decreaseCellHealth()
    {
        cellHealth--;
        if (cellHealth <= 0)
            Destroy(gameObject);

    }//end decreaseCellHealth

    public int getCellHealth()
    {
        return cellHealth;

    }

}
