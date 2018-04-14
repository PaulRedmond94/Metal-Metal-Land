/*
Script which uses a combination of Voronoi's algorithm and the Manhatten Distance algorithm to procedurally generate a world

*/

using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenScript : MonoBehaviour
{
    //declare a 2d array of gameobjects to represent each cell 
    GameObject[,] terrainArray;

    //variables for determining the x and y dimensions of the world 
    int terrXLength = 30;
    int terrYLength = 10;

    public Sprite[] terrainTop;
    public Sprite[] terrainBelow;

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
    Vector2 player1Spawn;

    bool player2SpawnSet;
    Vector2 player2Spawn;

    void Awake()
    {
        //debug mode variables
        if(StaticScript.player1Character == "")
        {
            StaticScript.player1Character = "Ailbhe";
            StaticScript.player2Character = "Stuart Butler";
            StaticScript.roundCount = 10;

        }

    }//end Awake

    // Use this for initialization
    void Start()
    {
        terrainCell = Resources.Load("Objects/LandCell", typeof(GameObject)) as GameObject;
        dimX = terrainCell.GetComponent<SpriteRenderer>().bounds.size.x;
        dimY = terrainCell.GetComponent<SpriteRenderer>().bounds.size.y;
        objDown = new Vector3(0, dimY);
        objRight = new Vector3(dimX, 0); 

        maxVoronoiPoints = (int)((terrYLength * terrXLength) * 0.1);
        genTerrain();

        foreach (GameObject gObject in terrainArray)
        {
            if(gObject != null)
            {
                gObject.layer = 8;

            }//end if
            

        }//end foreach

    }//end start

    // Update is called once per frame
    void Update()
    {

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

    //function which returns whether the land should be air or land cells
    string distributedRandom(int x, int y)
    {
        //variables to hold specficiations of where the terrain type should consider the upper, middle and lower values of each axis
        int upperY;
        int lowerY;

        int upperX;

        int lowerX;

        int rand = Random.Range(1, 11);

        //if(StaticScript.terrainGenType.ToLower() == "standard")
        if (2 == 1) //temp variable, use later when basis for algorithm types are set
        {
            //set upper y to be at 20% height
            upperY = (int)(terrYLength * 0.2);
            lowerY = (int)(terrYLength * 0.8);

            if (y <= upperY)
            {
                return "air";

            }//end if
            else if (y > upperY && y <= lowerY)
            {
                int subRand = Random.Range(1, 11);
                if (subRand <= 8)
                {
                    return "land";

                }//end if
                else if (subRand > 8)
                {
                    return "air";

                }

            }//end else if

            else if (y > lowerY)
            {
                return "land";

            }//end else if

        }//end if terrain gen is set to standard
        else if (2 == 3)
        {
            upperY = (int)(terrYLength * 0.2);
            lowerY = (int)(terrYLength * 0.8);

            upperX = (int)(terrXLength * 0.2);
            lowerX = (int)(terrXLength * 0.8);

            if ((x <= upperX || x >= lowerX))
            {
                return "land";

            }

            else if (y <= upperY)
            {
                return "air";

            }//end if

            else if (y > lowerY)
            {
                return "land";

            }//end else if

            else if((x>upperX && x<lowerX)&&(y>upperY&& y < lowerY))
            {
                if(Random.Range(1,4) % 2 == 0)
                {
                    return "land";

                }
                else
                {
                    return "air";
                }

            }

            else
            {
                return "land";
            }

        }//end else if 1==1

        else if (1 == 1)
        {
            if(Random.Range(1, 3)==1){
                return "land";

            }//end if

            else
            {
                return "air";

            }

        }



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
        //Sprite terrainTop =  Resources.Load("Images/EnvironmentArt/BlockSprites/TerrainATop", typeof(Sprite)) as Sprite;
        //Sprite terrainBelowMoss = Resources.Load("Images/EnvironmentArt/BlockSprites/TerrainAInterior", typeof(Sprite)) as Sprite;
        for(int i = 0; i < terrXLength; i++)
        {
            for(int j = 0; j < terrYLength; j++)
            {
                if(terrainArray[i,j] != null )
                {
                    try
                    {
                        int randomNum = Random.Range(1, 10);

                        Sprite cellSprite;

                        if (j - 1 < 0)
                        {
                            if (randomNum < 8)
                            {
                                cellSprite = terrainTop[0];

                            }//end if

                            else {
                                cellSprite = terrainTop[1];

                            }//end else

                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = cellSprite;
                            terrainArray[i, j].GetComponent<CellBehaviourScript>().setCellTerrainType("surface");
                        }
                        else if (terrainArray[i, j - 1] == null)
                        {
                            if (randomNum < 8)
                            {
                                cellSprite = terrainTop[0];

                            }//end if

                            else {
                                cellSprite = terrainTop[1];

                            }//end else

                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = cellSprite;
                            terrainArray[i, j].GetComponent<CellBehaviourScript>().setCellTerrainType("surface");

                        }
                        else
                        {
                            if (randomNum < 7)
                            {
                                cellSprite = terrainBelow[0];

                            }//end if

                            else
                            {
                                cellSprite = terrainBelow[1];

                            }//end else if

                            terrainArray[i, j].GetComponent<SpriteRenderer>().sprite = cellSprite;
                            terrainArray[i, j].GetComponent<CellBehaviourScript>().setCellTerrainType("underground");

                        }


                    }//end try
                    //used in the event that a cell is in the top row and is land
                    catch(System.IndexOutOfRangeException ioe)
                    {
                        

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

        //loop through to find surface cells and their positions
        List<Vector2> surfaceCells = new List<Vector2>();

        for(int i = 0; i < terrXLength; i++)
        {
            for(int j = 0; j< terrYLength; j++)
            {
                if(terrainArray[i, j] != null &&
                        terrainArray[i, j].GetComponent<CellBehaviourScript>().getCellTerrainType() == "surface")
                {
                    surfaceCells.Add(new Vector2(i, j));

                }//end if

            }//end for j

        }//end for i


        //pass a reference to the surface cells list to a function which will set up player spawns
        while (!player1SpawnSet)
        {
            player1SpawnSet = generatePlayerSpawn(ref surfaceCells, 1);

        }//end while player 1 spawn is not set
        while (!player2SpawnSet)
        {
            player2SpawnSet = generatePlayerSpawn(ref surfaceCells, 2);

        }//end while player 2 spawn is not set

        //determine amount of weapon altars to insert 
        int weaponAltarAmount = surfaceCells.Count / 5;
        int weaponAltarCount = 0;

        GameObject weaponAltarInert = Resources.Load("Objects/waltar_inert") as GameObject;

        while (weaponAltarCount < weaponAltarAmount)
        {
            
            Vector2 weaponAltarSpawnLocation = getSurfaceCells(ref surfaceCells);

            if (!(weaponAltarSpawnLocation.x <= -1 || weaponAltarSpawnLocation.y <= -1))
            {
                terrainArray[(int)weaponAltarSpawnLocation.x, (int)weaponAltarSpawnLocation.y].GetComponent<CellBehaviourScript>().setCellHealth(int.MaxValue);
                Instantiate(weaponAltarInert, terrainArray[(int)weaponAltarSpawnLocation.x, (int)weaponAltarSpawnLocation.y].transform.position + new Vector3(0,0.64f), this.transform.rotation);
                surfaceCells.Remove(weaponAltarSpawnLocation);
                weaponAltarCount++;

            }//end if

        }//end while for generating weapons

        //generate spike pits or explosive barrels or nothing (If 3 is rolled, no dangerous items are generated
        int spikeOrBombBarrelChance = Random.Range(1, 4);

        //generate spikes
        //if (spikeOrBombBarrelChance == 1)
        if(Random.Range(1,3) == 1)
        {
            GameObject spikes = Resources.Load("Objects/Spikes") as GameObject;

            bool spikesGenerated = false;
            while (!spikesGenerated)
            {
                Vector2 spikePitLocation = new Vector2(-1,-1);

                try
                {
                    do
                    {
                        spikePitLocation = getSurfaceCells(ref surfaceCells);
                    } while (terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + 1] == null ||
                            terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + 2] == null);
                    
                    foreach (Vector2 vec in surfaceCells)
                    {
                        if (vec.x == spikePitLocation.x)
                        {
                            //ensure that potential spike pit cells have 3 height
                            if (terrainArray[(int)vec.x, (int)vec.y + 1] != null &&
                                terrainArray[(int)vec.x, (int)vec.y + 2] != null)
                            {
                                spikePitLocation = vec;
                                break;
                            }//end if

                        }//end if

                    }//end for each


                }//end try
                catch(System.IndexOutOfRangeException ioore)
                {


                }//end catch
                                    
                if (!(spikePitLocation.x == -1 && spikePitLocation.y == -1))
                {
                    int terrainDepth = 0;
                    
                    // an error happens here, game doesn't crash or anything and works perfectly fine despite it, I just want to get rid of the warning
                    try
                    {
                        while (terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + terrainDepth + 2] != null)
                        {
                            //this block should be destroyed
                            Destroy(terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + terrainDepth]);

                            //this should be replaced with spikes
                            Vector3 spikesPos = terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + terrainDepth + 1].transform.position;
                            Destroy(terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + terrainDepth + 1]);
                            GameObject spikesSpawned = Instantiate(spikes, spikesPos, this.transform.rotation) as GameObject;
                            spikesSpawned.GetComponent<CellBehaviourScript>().setCellHealth(int.MaxValue);
                            terrainArray[(int)spikePitLocation.x, (int)spikePitLocation.y + terrainDepth + 1] = spikesSpawned;
                            spikesGenerated = true;
                            terrainDepth++;

                        }//end while

                    }//end try
                    catch(System.IndexOutOfRangeException ioore)
                    {
                        Debug.Log("here's the pointless error and look at it do nothing at all");

                    }//end catch

                }//end if

            }//end while            
            
        }//end if spikes are to be used

        else
        {
            GameObject bombBox = Resources.Load("Objects/BombBox") as GameObject;

            int explosiveBarrelsCount = 0;
            int maxExplosiveBarrelsCount = terrXLength / 10;
            while (explosiveBarrelsCount< maxExplosiveBarrelsCount)
            {

                Vector2 bombBoxPos = getSurfaceCells(ref surfaceCells);

                if (!(bombBoxPos.x <= -1 || bombBoxPos.y <= -1))
                {
                    GameObject bombBoxSpawned = Instantiate(bombBox, terrainArray[(int)bombBoxPos.x, (int)bombBoxPos.y].transform.position + new Vector3(0, 0.64f), this.transform.rotation) as GameObject;
                    surfaceCells.Remove(bombBoxPos);
                    explosiveBarrelsCount++;

                }//end if

            }//end while for generating weapons

        }//generate explosive boxes
        
        
    }//end insertSpecialTerrain

    bool generatePlayerSpawn(ref List<Vector2> cellVectors, int player)
    {
        bool playerSpawnSet = false;
        while (!playerSpawnSet)
        {
            Vector2 playerSpawn = getSurfaceCells(ref cellVectors);

            while (playerSpawn.x <= -1 || playerSpawn.y <= -1) {
                playerSpawn = getSurfaceCells(ref cellVectors);

            }//end while

            //ensure user does not spawn very low down or in a cave etc.
            if (playerSpawn.y < terrYLength / 2)
            {

                playerSpawnSet = true;
                cellVectors.Remove(playerSpawn);
                if (player == 1)
                {
                    GameObject player1Prefab = Resources.Load("Characters/" + StaticScript.player1Character) as GameObject;
                    GameObject player1Char = Instantiate(player1Prefab, terrainArray[(int)playerSpawn.x, (int)playerSpawn.y].transform.position + new Vector3(0, 0.64f), transform.rotation) as GameObject;
                    player1Char.GetComponent<PlayerGameController>().setPlayerNumber(1);
                    player1Spawn = playerSpawn;
                    return true;

                }

                else if (player == 2)
                {
                    GameObject player2Prefab = Resources.Load("Characters/" + StaticScript.player2Character) as GameObject;
                    GameObject player2Char = Instantiate(player2Prefab, terrainArray[(int)playerSpawn.x, (int)playerSpawn.y].transform.position + new Vector3(0, 0.64f), transform.rotation) as GameObject;
                    player2Char.GetComponent<PlayerGameController>().setPlayerNumber(2);
                    player2Spawn = playerSpawn;

                    return true;

                }//end else if

            }//end if

        }//end while playerSpawn is not set

        return false;

    }//end generate player spawn

    Vector2 getSurfaceCells (ref List<Vector2> cellVectors)
    {
        bool spawnSet = false;
        Vector2 objectSpawn = new Vector2(-1, -1);

        while (!spawnSet)
        {
            int xCoOrd = Random.Range(0, cellVectors.Count);

            foreach (Vector2 vec in cellVectors)
            {
                if (vec.x == xCoOrd)
                {
                    objectSpawn = vec;
                    break;

                }//end if


            }//end for each

            //handle the rare event where there may not be a valid square at the point, roll again and grab the next square
            if (!(objectSpawn.x <= -1) || !(objectSpawn.y <= -1))
            {
                spawnSet = true;

            }//end if

            return objectSpawn;

        }//end while

        //vector shouldn't be returned
        return new Vector2(-1, -1);
        
    }//end getSurfaceCells

    //setters and getters
    public int getTerrXLength()
    {
        return terrXLength;

    }

    public void setTerrXLength(int terrXLength)
    {
        this.terrXLength = terrXLength;

    }

    public int getTerrYLength()
    {
        return terrYLength;

    }//end getTerrYLength

    public void setTerrYLength(int terrYLength)
    {
        this.terrYLength = terrYLength;

    }

    //function which returns the current status of all cells
    public GameObject[,] getLandCellArray()
    {
        return terrainArray;

    }

}//end main class