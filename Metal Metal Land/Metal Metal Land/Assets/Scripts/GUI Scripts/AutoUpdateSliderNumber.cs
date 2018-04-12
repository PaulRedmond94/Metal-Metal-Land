using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoUpdateSliderNumber : MonoBehaviour {

    //integer to hold converted slider value
    int sliderVal;
    
    //add gui elements
    Text sliderTxtVal;
    GameObject sliderObj;
    
	// Use this for initialization
	void Start () {
        sliderTxtVal = this.gameObject.GetComponent<Text>();
        sliderObj = GameObject.Find("RoundSlider");

	}//end start
	
	// Update is called once per frame
	void Update () {
        //sliderVal = Mathf.RoundToInt(sliderObj.GetComponent<Slider>().value);
        sliderVal = (int)sliderObj.GetComponent<Slider>().value;

        //if round is <=50 then show the number, otherwise, the rounds will be endless
        if (sliderVal<= 50)
        {
            sliderTxtVal.text = "" + sliderVal;

        }//end if
        else
        {
            sliderTxtVal.text = "ENDLESS!";

        }
        
	
	}//end update
}
