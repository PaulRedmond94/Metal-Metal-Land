using UnityEngine;
using System.Collections;

public class CellBehaviourScript : MonoBehaviour {

    SpriteRenderer mySpriteRenderer;
    string cellTerrainType;
    int cellHealth;
    Sprite cracked;
    Sprite damaged;
    Sprite crumbling;
    Sprite destroyed;
    
	// Use this for initialization
	void Awake () {
        cellHealth = 5;
        mySpriteRenderer = this.GetComponent<SpriteRenderer>();
        cellTerrainType = "ground";

        //load in terrainDamage sprites 
        cracked = Resources.Load<Sprite>("Images/EnvironmentArt/BlockSprites/TerrainDegradation/Level1");
        damaged = Resources.Load<Sprite>("Images/EnvironmentArt/BlockSprites/TerrainDegradation/Level2");
        crumbling = Resources.Load<Sprite>("Images/EnvironmentArt/BlockSprites/TerrainDegradation/Level3");
        destroyed = Resources.Load<Sprite>("Images/EnvironmentArt/BlockSprites/TerrainDegradation/Level4");

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

    //overload that allows you to increase stength
    public void decreaseCellHealth(int damageVal)
    {
        cellHealth -= damageVal; 
        if (cellHealth <= 0)
            Destroy(gameObject);

        //if the current block type is a standard terrain block, make it look suitably damaged
        if(gameObject.tag.ToLower() == "environment" && gameObject.transform.GetChild(0).gameObject != null)
        {
            GameObject cellDamage = gameObject.transform.GetChild(0).gameObject;

            if (cellHealth == 4)
            {
                cellDamage.GetComponent<SpriteRenderer>().sprite = cracked;

            }

            else if(cellHealth == 3)
            {
                cellDamage.GetComponent<SpriteRenderer>().sprite = damaged;

            }//end else if

            else if (cellHealth == 2)
            {
                cellDamage.GetComponent<SpriteRenderer>().sprite = crumbling;

            }//end else if

            else if(cellHealth ==1)
            {
                cellDamage.GetComponent<SpriteRenderer>().sprite = destroyed;

            }//end else if

        }//end if regular environment block

        

    }//end decreaseCellHealth

    public int getCellHealth()
    {
        return cellHealth;

    }

}
