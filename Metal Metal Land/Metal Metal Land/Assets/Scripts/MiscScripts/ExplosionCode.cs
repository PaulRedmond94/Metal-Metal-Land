using UnityEngine;
using System.Collections;

public class ExplosionCode : MonoBehaviour {

    Animator grenadeAnimator;
    public int scale;

	// Use this for initialization
	void Start () {
        grenadeAnimator = gameObject.GetComponent<Animator>();
        Destroy(gameObject, grenadeAnimator.GetCurrentAnimatorStateInfo(0).length);

        //grenadeAnimator.GetAnimatorTransitionInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(grenadeAnimator.GetCurrentAnimatorStateInfo(0).length);
        	
	}
}
