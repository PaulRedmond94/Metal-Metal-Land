using UnityEngine;
using System.Collections;

public class ControllerMainMenuScript : MonoBehaviour {
    
    private bool moveUp;

	// Use this for initialization
	void Start () {
        moveUp = true;

	}//end start 
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("p1_ui_vert") >= 0.8)
        {
            //Debug.Log("GOING UP");
            moveUp = true;
            moveIcon();

        }//end if 
        else if (Input.GetAxisRaw("p1_ui_vert") <= -0.8)
        {
            moveUp = false;
            //Debug.Log("GOING down");
            moveIcon();
        }//end else if 

        else if (Input.GetAxisRaw("p2_jump")>0)
        {
            Debug.Log("game broke");

        }

        if(Time.frameCount%60 == 0)
        {
            Debug.Log(Input.GetAxisRaw("p1_ui_vert"));

        }

	}

    public IEnumerator moveIcon() {
        Debug.Log("In move icon");

        if (moveUp)
        {
            Debug.Log("moving up");

        }//end if
        else
        {
            Debug.Log("going down");

        }

        yield return new WaitForSeconds(0.25f);

    }//end moveIcon
}
