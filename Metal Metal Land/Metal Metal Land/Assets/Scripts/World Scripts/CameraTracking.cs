﻿using UnityEngine;
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
        float camX = Mathf.Clamp(playerMeanPoint.x, leftBounds, rightBounds);
        float camY = Mathf.Clamp(playerMeanPoint.y, downBounds, upBounds);
        transform.position = (new Vector3(camX, camY, zoomBounds));


        // save this code for later on to use it for zooming in and out as needed for players. 
        // Upper clamp is ~6.5f
        // lower clamp is ~2.0f
        cam.orthographicSize = ((scaleOrtographicZoom(8.0f, 20.0f, 4.0f,6.5f,Vector2.Distance(players[0].transform.position, players[1].transform.position))));
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

    public float scaleOrtographicZoom(float playerDistanceMin, float playerDistanceMax, float orthoZoomMin, float orthoZoomMax,  float playerDistanceValue)
    {
        //if and else if to ensure that there is a cap to the orthographic zoom
        //if players are currently below the minimum mandatory distance for the orthograph zoom to take place, return the min zoom
        if (playerDistanceValue <= playerDistanceMin)
        {
            return orthoZoomMin;
        }
        //else if players are above the max mandatory distance, return max zoom
        else if (playerDistanceValue >= playerDistanceMax)
        {
            return orthoZoomMax;
        }
        //otherwise scale the camera orthographic zoom as necessary
        else {
            //calculate the player distance mapped to the minimum and maximum possible distances, return the difference between these and map it to the orthographic scale
            float mappedPlayerDist = (playerDistanceValue - playerDistanceMin) / (playerDistanceMax - playerDistanceMin);
            float orthoDifference = Mathf.Abs(orthoZoomMax - orthoZoomMin);
            return orthoDifference * (mappedPlayerDist) + orthoZoomMin;
        }
    }

}
