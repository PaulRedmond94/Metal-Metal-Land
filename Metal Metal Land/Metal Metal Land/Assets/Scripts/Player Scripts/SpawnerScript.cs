using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    Vector3 initPos;
    // Use this for initialization
    void Start()
    {
        initPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void respawn()
    {
        this.transform.position = initPos;


    }//end respawn
}
