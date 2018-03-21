/*
Script which uses a combination of Voronoi's algorithm and the Manhatten Distance algorithm to procedurally generate a world

*/
using UnityEngine;
using System.Collections;

public class ProceduralGenScript : MonoBehaviour
{
    //declare a 2d array of gameobjects to represent each cell 
    GameObject[,] terrainArray;
    GameObject landCell;

    //variables for determining the x and y dimensions of the world 
    public int terrXLength = 25;
    public int terrYLength = 10;


    //debug variable, delete later
    public int terrainType;

    //array of voronoi points
    VoronoiPoint[] vPoints;

    //variables for counting the amount of cells and for holding the max num of cells

    int maxVoronoiPoints;
    int currVPoints = 0;

    // Use this for initialization
    void Start()
    {
        maxVoronoiPoints = (int)((terrYLength * terrXLength) * 0.1);
        genTerrain();



    }//end start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space pressed");
            GameObject[] cells;
            cells = GameObject.FindGameObjectsWithTag("Environment");

            foreach(GameObject cell in cells)
            {
                Destroy(cell);

            }//end foreach
            terrainArray = null;
            vPoints = null;
            genTerrain();

        }//end if
    }



    void genTerrain()
    {
        GameObject terrainCell = Resources.Load("LandCell", typeof(GameObject)) as GameObject;
        float dimX = terrainCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float dimY = terrainCell.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 objDown = new Vector3(0, dimY);
        Vector3 objRight = new Vector3(dimX, 0);

        //procedural Generation algorithm goes here
        vPoints = new VoronoiPoint[maxVoronoiPoints];
        generateVoronoiPoints();
        terrainArray = new GameObject[terrXLength, terrYLength];


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
                        //item is a voronoi point, can't get closer than this
                        terrainType = vPoints[z].getCellType();
                        break;

                    }//end if

                    else if (vPoints[z].getManhattanDistance(i, j) < minManHatDist)
                    {
                        minManHatDist = vPoints[z].getManhattanDistance(i, j);
                        vPoint = vPoints[z];

                    }//end else if

                }//end for

                if(terrainType == "")
                {
                    terrainType = vPoint.getCellType();

                }//end if

                // generate cell based on terrainType
                if(terrainType == "land")
                {
                    Vector3 cellPos = this.transform.position - objDown * j;
                    cellPos = cellPos + objRight * i;

                    GameObject newCell = Instantiate(terrainCell, cellPos, this.transform.rotation) as GameObject;
                    terrainArray[i, j] = newCell;                    

                }//end if terrain of current cell is land

                else
                {

                }//end else


            }//end for

        }//end outer for loop

        updateTerrainArt();
        
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
                if(terrainArray[i,j] != null)
                {
                    try
                    {
                        if (terrainArray[i, j - 1] == null)
                        {
                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = terrainTop;

                        }

                        else
                        {
                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = terrainBelow;
                        }


                    }
                    catch(System.IndexOutOfRangeException ioe)
                    {
                        Debug.Log("boop");
                    }



                }//end if

            }//end for

        }//end for


    }//end updateTerrainArt

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