﻿/*
Script which uses a combination of Voronoi's algorithm and the Manhatten Distance algorithm to procedurally generate a world

*/
using UnityEngine;

public class ProceduralGenScript : MonoBehaviour
{
    //declare a 2d array of gameobjects to represent each cell 
    GameObject[,] terrainArray;

    //variables for determining the x and y dimensions of the world 
    public int terrXLength = 25;
    public int terrYLength = 10;

    //debug variable, delete later
    public int terrainType;

    //array of voronoi points
    VoronoiPoint[] vPoints;

    //variables for counting the amount of cells and for holding the max num of cells
    int maxVoronoiPoints;

    //load up default land cell and get the dimensions of it (normally 64*64)
    GameObject terrainCell;
    float dimX;
    float dimY;

    //get translation vectors for generating terrain
    Vector3 objDown;
    Vector3 objRight;
    Vector3 objUp;

    bool player1SpawnSet;
    bool player2SpawnSet;

    // Use this for initialization
    void Start()
    {
        terrainCell = Resources.Load("LandCell", typeof(GameObject)) as GameObject;
        dimX = terrainCell.GetComponent<SpriteRenderer>().bounds.size.x;
        dimY = terrainCell.GetComponent<SpriteRenderer>().bounds.size.y;
        objDown = new Vector3(0, dimY);
        objRight = new Vector3(dimX, 0); 

        maxVoronoiPoints = (int)((terrYLength * terrXLength) * 0.1);
        genTerrain();

    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject[] cells;
            cells = GameObject.FindGameObjectsWithTag("Environment");

            //destroy all current cells
            foreach(GameObject cell in cells)
            {
                Destroy(cell);

            }//end foreach

            //reset variables
            terrainArray = null;
            vPoints = null;

            genTerrain();

        }//end if
    }

    //core function to generate the terrain
    void genTerrain()
    {

        //procedural Generation algorithm goes here
        //declare an array of Voronoi Point objects 
        vPoints = new VoronoiPoint[maxVoronoiPoints];

        //execute function to generate values for the voronoi array
        generateVoronoiPoints();

        //create a terrain gameObject array which will hold each terrain cell
        terrainArray = new GameObject[terrXLength, terrYLength];

        //loops to go through each terrain cell and calculate its manhatten distance to its closest voronoi point
        for (int i = 0; i < terrXLength; i++)
        {
            for(int j =0; j < terrYLength; j++)
            {
                VoronoiPoint vPoint = null;
                int minManHatDist = 50;
                string terrainType = "";

                //loop to check for nearest voronoi point for current cell
                for(int z = 0; z < vPoints.Length; z++)
                {

                    if (vPoints[z].getManhattanDistance(i, j) == 0)
                    {
                        //item is a voronoi point, can't get closer than this so break out of loop and move to next cell
                        terrainType = vPoints[z].getCellType();
                        break;

                    }//end if

                    else if (vPoints[z].getManhattanDistance(i, j) < minManHatDist)
                    {
                        //item is not a voronoi point, continue examining closest points 
                        // NOTE::: Potentially include function to skip over voronoi points which are too far away to consider
                        minManHatDist = vPoints[z].getManhattanDistance(i, j);
                        vPoint = vPoints[z];

                    }//end else if

                }//end for

                //current cell was not a voronoi point. Therefore, collect its celltype
                if (terrainType == "")
                {
                    terrainType = vPoint.getCellType();

                }//end if

                // generate land cells based on terrainType
                if(terrainType == "land")
                {
                    Vector3 cellPos = this.transform.position - objDown * j;
                    cellPos = cellPos + objRight * i;

                    GameObject newCell = Instantiate(terrainCell, cellPos, this.transform.rotation) as GameObject;
                    terrainArray[i, j] = newCell;                    

                }//end if terrain of current cell is land

            }//end for

        }//end outer for loop

        updateTerrainArt();

        insertSpecialTerrain();
        
    }

    string distributedRandom(int x, int y)
    {
        //variables to hold specficiations of where the terrain type should consider the upper, middle and lower values of each axis
        int upperY;
        int midY;
        int lowerY;

        int upperX;
        int midX;
        int lowerX;

        int rand = Random.Range(1, 11);

        //if(StaticScript.terrainGenType.ToLower() == "standard")
        if (terrainType == 1) //temp variable, use later when basis for algorithm types are set
        {
            //set upper y to be at 20% height
            upperY = (int)(terrYLength * 0.2);
            lowerY = (int)(terrYLength *0.8);

            if (y <= upperY)
            {
                return "air";

            }//end if
            else if(y> upperY && y<= lowerY)
            {
                int subRand = Random.Range(1, 11);
                if (subRand <= 8)
                {
                    return "land";

                }//end if
                else if(subRand> 8)
                {
                    return "air";

                }

            }//end else if

            else if (y > lowerY)
            {
                return "land";

            }//end else if

        }//end if terrain gen is set to standard


        // default return type
        return "air";

    }//end distributedRandom

    void generateVoronoiPoints()
    {

        for(int i = 0; i < maxVoronoiPoints; i++)
        {
            int x = Random.Range(0, terrXLength + 1);
            int y = Random.Range(0, terrYLength + 1);
            vPoints[i] = new VoronoiPoint(x,y,distributedRandom(x,y));

        }//end for

    }//end generateVoronoiPoints

    void updateTerrainArt()
    {
        Sprite terrainTop =  Resources.Load("Images/EnvironmentArt/BlockSprites/TerrainATop", typeof(Sprite)) as Sprite;
        Sprite terrainBelow = Resources.Load("Images/EnvironmentArt/BlockSprites/TerrainAInterior", typeof(Sprite)) as Sprite;

        for(int i = 0; i < terrXLength; i++)
        {
            for(int j = 0; j < terrYLength; j++)
            {
                if(terrainArray[i,j] != null )
                {
                    try
                    {
                        if (terrainArray[i, j - 1] == null)
                        {
                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = terrainTop;
                            terrainArray[i, j].GetComponent<CellBehaviourScript>().setCellTerrainType("surface");

                        }

                        else
                        {
                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = terrainBelow;
                            terrainArray[i, j].GetComponent<CellBehaviourScript>().setCellTerrainType("underground");

                        }


                    }//end try
                    //used in the event that a cell is in the top row and is land
                    catch(System.IndexOutOfRangeException ioe)
                    {
                        terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = terrainTop;

                    }//end catch

                }//end if

            }//end for

        }//end for

    }//end updateTerrainArt

    //function which is used to insert special terrain
    /*
        Special terrain is prioritized as: (Most to least important)
            1. Player Spawns
            2. Weapon Spawns
            3. Spike Pits
            4. Elevators
    */
    void insertSpecialTerrain()
    {
        player1SpawnSet = false;
        player2SpawnSet = false;

        for(int i = 0; i < terrYLength; i++)
        {
            //loop for player 1
            for(int j = 0; j<terrXLength; j++)
            {
                if (terrainArray[j,i]!= null && player1SpawnSet == false)
                {
                    if (terrainArray[j, i].GetComponent<CellBehaviourScript>().getCellTerrainType() == "surface")
                    {
                        Debug.Log("Player 1 Spawn is at : " + j + " , " + (i - 1));
                        terrainArray[j, i].GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f); //sets color to red
                        player1SpawnSet = true;

                    }//end if

                }//end if

                if (player1SpawnSet)
                {
                    break;

                }//end if

            }//end for j



            //loop for player 2 spawn
            for(int j=terrXLength-1; j > 0; j--)
            {
                if (terrainArray[j, i] != null && player2SpawnSet == false)
                {
                    if (terrainArray[j, i].GetComponent<CellBehaviourScript>().getCellTerrainType() == "surface")
                    {
                        Debug.Log("Player 2 Spawn is at : " + j + " , " + (i - 1));
                        terrainArray[j, i].GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f, 1f); //sets color to red
                        player2SpawnSet = true;        

                    }//end if

                }//end if

                if (player2SpawnSet)
                {
                    break;

                }//end if player2SpawnSet
                
            }

        }//end for i
        

    }//end insertSpecialTerrain

}//end main class


//class to hold voronoi point object
class VoronoiPoint
{
    private int x;
    private int y;
    private string cellType;

    public VoronoiPoint(int x, int y, string cellType)
    {
        this.x = x;
        this.y = y;
        this.cellType = cellType;


    }//end constructor

    //setters and getters
    public void setX(int x) {
        this.x = x;

    }//end set X

    public int getX()
    {
        return x;

    }//end getX

    public void setY(int y)
    {
        this.y = y;

    }//end setY

    public int getY()
    {
        return y;

    }//end getY

    public void setCellType(string cellType)
    {
        this.cellType = cellType;

    }//end set CellType

    public string getCellType()
    {
        return cellType;

    }//end getCellType

    public string toString()
    {
        return "X CoOrd: " + x + "  yCoOrd: " + y + "  Terrain: " + cellType;

    }


    public int getManhattanDistance(int xVal, int yVal)
    {
        int absXValue = System.Math.Abs(xVal - x);
        int absYValue = System.Math.Abs(yVal - y);

        return absXValue + absYValue;

    }//end getManhattanDistance

}//end voronoiPoint class