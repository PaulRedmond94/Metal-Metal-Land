using UnityEngine;
using System.Collections;

public class GrenadeDetonation : MonoBehaviour {

    CircleCollider2D cirCol2D;

	// Use this for initialization
	void Start () {
        Invoke("detonateGrenade", 2.5f);
        cirCol2D = gameObject.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void detonateGrenade()
    {
        Debug.Log("Grenade go boom");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cirCol2D.radius);
        ExplosiveScript.detonateArray(colliders);

        Destroy(gameObject);

    }
}
