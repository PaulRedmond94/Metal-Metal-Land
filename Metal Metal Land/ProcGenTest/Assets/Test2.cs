using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {

    int screenHeight, screenWidth, cellsX, cellsY;
    int cellSize = 40;


    // Use this for initialization
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        cellsX = screenWidth / 40;
        cellsY = screenHeight / 40;

        Debug.Log("\nScreen Height: " + screenHeight);
        Debug.Log("Screen Width: " + screenWidth);
        Debug.Log("\nX Axis Cells: " + cellsX);
        Debug.Log("\nY Axis Cells: " + cellsY);

        createGrid();       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //function to createGrid
    void createGrid()
    {
        for(int i = 0; i<cellsX; i++)
        {
            for(int j = 0; j<cellsY; j++)
            {
                GameObject newCell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                newCell.transform.position = new Vector2((i * 40), (j * 40));


            }//end for on the y axis

        }//end for on the x axis

    }//end createGrid
}
