using UnityEngine;
using System.Collections;

public class PlatformScript : MonoBehaviour {
    GameObject parentPlatformBracket,platform;
    float startXCoOrd, endXCoOrd, startYCoOrd, endYCoOrd, speed, objXLength, objYLength;
    bool coOrdDecrease;
    public string orientation;

	// Use this for initialization
	void Start () {
        platform = this.gameObject;
        parentPlatformBracket = this.gameObject.transform.parent.gameObject;
        coOrdDecrease = false;
        //startXCoOrd = parentPlatformBracket.transform.position.x;
        speed = 5.0f;
        /*
        if(orientation.ToUpper()!="VERTICAL" || orientation.ToUpper() != "HORIZONTAL")
        {
            Debug.Log("Error, platform does not have valid direction");
            Debug.Log(orientation);
            Debug.Log(orientation.ToUpper() + "VERTICAL");
            Debug.Log(orientation.ToUpper() + "HORIZONTAL");
            Application.Quit(); 

        }//end if
        */
        if (orientation.ToUpper() == "VERTICAL")
        {
            Debug.Log("Upper platform found");
            objYLength = parentPlatformBracket.GetComponent<SpriteRenderer>().bounds.size.y;
            Debug.Log(objYLength);
            startYCoOrd = (parentPlatformBracket.transform.position.y - (objYLength / 2));
            Debug.Log(startYCoOrd);
            endYCoOrd = (startYCoOrd + objYLength);
            Debug.Log(endYCoOrd);

        }//end else if
        else if(orientation.ToUpper() == "HORIZONTAL")
        {
            Debug.Log("Sideways platform found");
            objXLength = parentPlatformBracket.GetComponent<SpriteRenderer>().bounds.size.x;
            startXCoOrd = (parentPlatformBracket.transform.position.x - (objXLength / 2));
            endXCoOrd = (startXCoOrd + objXLength);
            Debug.Log("BING BONG");
        }//end else if

	}
	
	// Update is called once per frame
	void Update () {
        if(orientation.ToUpper() == "HORIZONTAL")
        {
            //Debug.Log("Start: " + startXCoOrd + "\tEnd: " + endXCoOrd);
            if (this.transform.position.x <= startXCoOrd)
            {
                coOrdDecrease = false;

                //playerCharacter.transform.position += (moveDir * speed * Time.deltaTime);

            }//end if
            else if (this.transform.position.x > endXCoOrd)
            {
                coOrdDecrease = true;

            }//end else
            Vector3 Movedir;
            if (coOrdDecrease)
            {
                Movedir = new Vector3(-1, 0, 0);
                platform.gameObject.transform.position += (Movedir * speed * Time.deltaTime);

            }
            else if (!coOrdDecrease)
            {
                Movedir = new Vector3(1, 0, 0);
                platform.gameObject.transform.position += (Movedir * speed * Time.deltaTime);

            }//end else if

        }//end if orientation is horizontal
        else if(orientation.ToUpper() == "VERTICAL")
        {
            //Debug.Log("Start: " + startXCoOrd + "\tEnd: " + endXCoOrd);
            if (this.transform.position.y <= startYCoOrd)
            {
                coOrdDecrease = false;

                //playerCharacter.transform.position += (moveDir * speed * Time.deltaTime);

            }//end if
            else if (this.transform.position.y > endYCoOrd)
            {
                coOrdDecrease = true;

            }//end else
            Vector3 Movedir;
            if (coOrdDecrease)
            {
                Movedir = new Vector3(0, -1, 0);
                platform.gameObject.transform.position += (Movedir * speed * Time.deltaTime);

            }
            else if (!coOrdDecrease)
            {
                Movedir = new Vector3(0, 1, 0);
                platform.gameObject.transform.position += (Movedir * speed * Time.deltaTime);

            }//end else if

        }//end else if
        


    }//end update

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log(this.gameObject.name + " player hit platform ");
            collision.transform.parent = this.transform;

        }//end if

    }// end onCollisionEnter2D
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.parent = null;

        }//end if
    }
}
