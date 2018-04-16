using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CameraTracking : MonoBehaviour {
    Camera cam;
    List<GameObject> players;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    float leftBounds, rightBounds, upBounds, downBounds, zoomBounds;
    public GameObject nwPoint;
    public GameObject sePoint;

    Vector2 initialPos;
    Vector2 playerMeanPoint;

    bool loadCheck = false;

    // Use this for initialization
    void Start () {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        cam = this.GetComponent<Camera>();
        zoomBounds = -10.0f;
        initialPos = this.transform.position;

        leftBounds =  (nwPoint.transform.position.x + initialPos.x)/2;
        upBounds = (nwPoint.transform.position.y + initialPos.y)/2;
        downBounds = (sePoint.transform.position.y + initialPos.y)/2;
        rightBounds = (sePoint.transform.position.x + initialPos.x) /2;
        playerMeanPoint = new Vector2();

        players = new List<GameObject>();

        //Debug.Log("Max X,Y : " + leftBounds + "," + upBounds);
        //Debug.Log("Min X,Y : " + rightBounds + "," + downBounds);


    }//end start
	
	// Update is called once per frame
	void Update () {

        if (players.Count <= 1 && !loadCheck)
        {
            players = GameObject.FindGameObjectsWithTag("Player").ToList();

        }//end while player count <= 1

        if (players.Count == 2)
        {
            playerMeanPoint = calcMeanPoint();

            //Debug.Log(Vector2.Distance(players[0].transform.position, players[1].transform.position));

           

        }//end if players.count == 1

        else if(players.Count == 1)
        {
            //cam.transform.position = players..transform.position;


        }

        else
        {
            playerMeanPoint = initialPos;
        }
            

        

        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        //Vector2 screenPos = cam.WorldToScreenPoint(player.transform.position);
        //Debug.Log("Player is " + screenPos.x + " , " + screenPos.y);
        float camX = Mathf.Clamp(playerMeanPoint.x, leftBounds, rightBounds);
        float camY = Mathf.Clamp(playerMeanPoint.y, downBounds, upBounds);
        transform.position = (new Vector3(camX, camY, zoomBounds));
        //sDebug.Log("player y: " + player.gameObject.transform.position.y + " , upBounds);
        //cam.orthographicSize = Mathf.Clamp((playerMeanPoint.x/(Screen.width/2))*6.5f, 2.0f, 6.5f);

        // save this code for later on to use it for zooming in and out as needed for players. 
        // Upper clamp is ~6.5f
        // lower clamp is ~2.0f
        cam.orthographicSize = ((Map(4.0f,6.5f,8.0f,20.0f,Vector2.Distance(players[0].transform.position, players[1].transform.position))));
        if (Input.GetKeyDown("h"))
        {
            cam.orthographicSize += 0.5f;


        }
        else if (Input.GetKeyDown("k"))
        {
            cam.orthographicSize -= 0.5f;

        }

    }

    Vector3 calcMeanPoint()
    {
        Vector3 returnVal;

        returnVal= new Vector3(0, 0);

        //get meanpoint between to player Objects
        foreach (GameObject player in players)
        {
            returnVal += player.transform.position;

        }//end foreach

        return returnVal/2;


    }//end update
     /*
     float findOrthographicScale(float value, float distanceMin, float distanceMax, float orthographicMin, float orthographicMax)
     {
         float distancePoint = (distanceMax - distanceMin);
         float ortographicPoint = (orthographicMax - orthographicMin);
         float returnVal = ((((value - distanceMin)* ortographicPoint)/distancePoint) + distanceMin)/2;

         return returnVal;


     }//end findOrt
     */

    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }

}
