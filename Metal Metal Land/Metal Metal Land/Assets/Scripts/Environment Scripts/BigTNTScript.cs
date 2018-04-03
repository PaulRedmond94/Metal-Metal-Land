using UnityEngine;
using System.Collections;
using System;

public class BigTNTScript : MonoBehaviour {

    public int bombCountDown;
    CircleCollider2D cirCol;
    Single val;
    Type type;
	// Use this for initialization
	void Start () {
        bombCountDown = 3;
        cirCol = this.gameObject.GetComponent<CircleCollider2D>();
        /*type = cirCol.radius.GetType();
        val = 2.32f;
        Debug.Log("Radius type = " + type + ", " + cirCol.radius);
        cirCol.radius = val;
        Debug.Log("Radius type = " + type + ", " + cirCol.radius);
        */
        StartCoroutine(beginFuse());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator beginFuse()
    {
        Debug.Log("fuse is lit");
        yield return new WaitForSeconds(bombCountDown);
        Debug.Log("fuse is done, detonation imminent");
        detonate();

    }
    void detonate()
    {
        int count = 0;
        //create array of items that are currently within the objects radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);

        Debug.Log("In detonation function");

        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Environment")
            {
                Debug.Log("Count incremented");
                count++;

                collider.GetComponent<CellBehaviourScript>().explode();

            }//end if

        }

        Debug.Log("Amount exploded: " + count);

        Destroy(this.gameObject);

    }//end detonate


}
