/*
Script which uses a combination of Voronoi's algorithm and the Manhatten Distance algorithm to procedurally generate a world

*/
using UnityEngine;
using System.Collections;

public class ProceduralGenScript : MonoBehaviour
{
    //variables for determining the x and y dimensions of the world 
    public int terrXLength = 30;
    public int terrYLength = 10;


    //debug variable, delete later
    public bool staticTerrainDispersal;

    //array of voronoi points
    VoronoiPoint[] vPoints;

    //variables for counting the amount of cells and for holding the max num of cells
    int cellCount = 0;
    public int maxCellCount = 0;

    int landCellCount;
    int airCellCount;

    int maxVoronoiPoints;
    int currVPoints = 0;

    //declare a 2d array of gameobjects to represent each cell 
    GameObject[,] terrainArray;
    GameObject landCell;
    //GameObject airCell;


    // Use this for initialization
    void Start()
    {
        landCellCount = 0;
        airCellCount = 0;
        maxVoronoiPoints = (int)((terrYLength * terrXLength) * 0.1);

        cellCount = landCellCount + airCellCount;

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
        
        //Pseudo Code steps defined by PC:: at start of comment, [./] defines that the task is complete, [X] defines it is not currently complete


        //PC:: create reference to terrain cell object [./]
        //landCell = Resources.Load("LandCell", typeof(GameObject)) as GameObject;
        //airCell = Resources.Load("AirCell", typeof(GameObject)) as GameObject;

        //PC:: create empty 2d GameObject array to hold each terrainCell [./]
        //PC:: get public array dimensions and create the new array based on these dimensions[./]
        terrainArray = new GameObject[terrXLength, terrYLength];

        generateVoronoiPoints();

        GameObject prevObject = null;

        //PC:: move through gameobject array and instantiate objects based on what their closest voronoi point is.         
        //PC:: use manhatten distance algorithm to determine the nearest point
        //PC:: (Depending on terrain gen type specified by user, choose whether to prioritize air or land)
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
                    cellCount++;
                    terrainArray[i, j] = newCell;


                    /*if(j == 0)
                    {
                        prevObject = Instantiate(terrainCell, this.transform.position - (objDown * i), this.transform.rotation) as GameObject;
                        cellCount++;
                        string textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + 0 + " " + i;
                        prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
                        terrainArray[0, i] = prevObject;
                        

                    }
                    else
                    {
                        prevObject = Instantiate(terrainCell, prevObject.transform.position + objRight, this.transform.rotation) as GameObject;
                        cellCount++;
                        string textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + j + " " + i;
                        prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
                        terrainArray[j, i] = prevObject;

                    }*/
                    

                }//end if terrain of current cell is land

                else if(terrainType == "air")
                {


                }//end else if

                else
                {

                }//end else


            }//end for

        }//end outer for loop


        //PC:: end for loop

        //PC:: For loop

        //PC:: Instantiate special objects (Item boxes, traps, platforms etc. based on generated terrain)

        //PC:: End For loop

        //PC:: For loop

        //PC:: Instantiate player spawns

        //PC:: End For loop









        //old terrain gen below this
        /*
        float dimX = terrainCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float dimY = terrainCell.GetComponent<SpriteRenderer>().bounds.size.y;

        Vector3 objDown = new Vector3(0, dimY);
        Vector3 objRight = new Vector3(dimX, 0);

        GameObject prevObject;
        //for loops to generate terrain on the y axis
        for (int i = 0; i < terrYLength; i++)
        {
            prevObject = Instantiate(terrainCell, this.transform.position - (objDown * i), this.transform.rotation) as GameObject;
            cellCount++;
            string textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + 0 + " " + i;
            prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
            terrainArray[0, i] = prevObject;

            for (int j = 1; j < terrXLength; j++)
            {
                prevObject = Instantiate(terrainCell, prevObject.transform.position + objRight, this.transform.rotation) as GameObject;
                cellCount++;
                textMeshVal = "Cell Num: \n" + cellCount + "\nPos: " + j + " " + i;
                prevObject.GetComponentInChildren<TextMesh>().text = textMeshVal;
                terrainArray[j, i] = prevObject;

            }//end for

        }//end for


        //update sprites for the cells based on their terrain type
        GameObject currentCell;

        for (int i = 0; i < terrYLength; i++)
        {
            for (int j = 0; j < terrXLength; j++)
            {
                currentCell = terrainArray[j, i];
                //determine terrain type
                if (currentCell.GetComponent<CellBehaviourScript>().getCellTerrainType().ToLower() == "ground")
                {
                    currentCell.GetComponent<SpriteRenderer>().color = Color.grey;

                }//end if

            }

        }//end for int i


    }//end setSquareColour

    */

    }

    string distributedRandom()
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
        if(1==1)
        {
            //set upper y to be at 20% height
            upperY = (int)(terrYLength * 0.2);
            lowerY = (int)(terrYLength *0.8);

            //Debug.Log("Upper y: " + upperY);
            //Debug.Log("Mid y: " + midY);
            //Debug.Log("Lower y: " + lowerY);

            if (rand < upperY)
            {
                airCellCount++;
                return "air";

            }//end if
            else if(rand>= upperY && rand<= lowerY)
            {
                int subRand = Random.Range(1, 11);
                if (subRand <= 8)
                {
                    landCellCount++;
                    return "land";
                }//end if
                else if(subRand> 8)
                {
                    airCellCount++;
                    return "air";
                }

            }//end else if

            else if (rand > lowerY)
            {
                landCellCount++;
                return "land";

            }//end else if

        }//end if

        return "air";

    }//end distributedRandom


    void generateVoronoiPoints()
    {
        if (staticTerrainDispersal)
        {
            /*int loopVal = 0;
            //do while loop to generate positions of voronoi points
            while (currVPoints < maxVoronoiPoints)
            {
                if(loopVal%maxVoronoiPoints == 0)
                {

                    currVPoints++;
                }
                

            }//end while*/

        }//end if
        else
        {
            for(int i = 0; i < maxVoronoiPoints; i++)
            {
                int x = Random.Range(0, terrXLength + 1);
                int y = Random.Range(0, terrYLength + 1);
                vPoints[i] = new VoronoiPoint(x,y,distributedRandom());

            }//end for

        }//end else

    }//end generateVoronoiPoints

}


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