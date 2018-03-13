using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    int screenHeight, screenWidth, cellsX, cellsY;
    int cellSize = 40;


    // Use this for initialization
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        cellsX = screenWidth / 40;
        cellsY = screenHeight / 40;

        Debug.Log("\nScreen Height: " + screenHeight +
            "\nScreen Width: " + screenWidth +
            "\nX Axis Cells: " + cellsX +
            "\nY Axis Cells: " + cellsY);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
