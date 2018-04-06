using UnityEngine;
using System.Collections;

public class RpgExplode : MonoBehaviour {

    CircleCollider2D cirCol;

    // Use this for initialization
    void Start () {
        cirCol = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol.radius);
        ExplosiveScript.detonateArray(colliders);

        Destroy(this.gameObject);
        
    }//end OnCollisionEnter2D
}
