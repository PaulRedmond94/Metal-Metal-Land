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
        if(players.Count <= 1)
        {
            players = GameObject.FindGameObjectsWithTag("Player").ToList();

        }//end while player count <= 1

        if (players.Count == 2)
            playerMeanPoint = calcMeanPoint();

        else
            playerMeanPoint = initialPos;

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


    }
}
