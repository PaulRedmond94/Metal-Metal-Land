﻿using UnityEngine;
using System.Collections;

public class TerrainGenScript : MonoBehaviour {

    int terrXLength = 30;
    int terrYLength = 10;
    int cellCount = 0;
    int test = 4;
    //declare a 2d array of gameobjects to represent each cell 
    GameObject[,] terrainArray;
    GameObject terrainCell; 
    
    
    // Use this for initialization
	void Start () {
        terrainArray = new GameObject[terrXLength, terrYLength];
        terrainCell = Resources.Load("LandCell", typeof(GameObject)) as GameObject;

        GameObject prevObject;
        float dimX = terrainCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float dimY = terrainCell.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 objDown = new Vector3(0, dimY);
        Vector3 objRight = new Vector3(dimX, 0);

        //for loops to generate terrain on the y axis
        for (int i = 0; i < terrYLength; i++)
        {
            
        
            prevObject = Instantiate(terrainCell, this.transform.position - (objDown * i), this.transform.rotation) as GameObject;
            cellCount++;
            string textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + 0 + " " + i;
            prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
            terrainArray[0, i] = prevObject;

            for (int j = 1; j< terrXLength; j++)
            {
                prevObject = Instantiate(terrainCell, prevObject.transform.position + objRight, this.transform.rotation) as GameObject;
                cellCount++;
                textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + j + " " + i;
                prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
                terrainArray[j, i] = prevObject;

            }//end for

            test = 0;

        }//end for
        
        genTerrain(terrainArray);

	}//end start
	
	// Update is called once per frame
	void Update () {
	    
	}

    void genTerrain(GameObject[,] terrainArray)
    {
        Debug.Log("function entered");
        //procedural Generation algorithm goes here
        if (test == 0)
            Debug.Log("for loop finished");

        //update sprites for the cells based on their terrain type
        GameObject currentCell;    

        for (int i = 0; i < terrYLength; i++)
        {
            for(int j = 0; j< terrXLength; j++)
            {
                currentCell = terrainArray[j, i];
                Debug.Log("Cell text" + currentCell.GetComponentInChildren<TextMesh>().text);
                //Debug.Log("new cell loaded");
                Debug.Log("Current Cell Type: " + currentCell.GetComponent<CellBehaviourScript>().getCellTerrainType());
                Debug.Log("Compare to: Ground");
                Debug.Log("" + currentCell.GetComponent<SpriteRenderer>().color);
                //determine terrain type
                if (currentCell.GetComponent<CellBehaviourScript>().getCellTerrainType().ToLower() == "ground")
                {
                    Debug.Log("we got a match");
                    currentCell.GetComponent<SpriteRenderer>().color = Color.grey;

                }//end if

            }

        }//end for int i
        

    }//end setSquareColour

}
