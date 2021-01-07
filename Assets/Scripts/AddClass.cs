using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddClass : MonoBehaviour
{

	public GameObject input; 
	public Test testCanvas;
    // Update is called once per frame
    void Update()
    {
		if(TrackDropDown.playerClass == "Mage") {
			input.SetActive(true);
		}
		else {
			input.SetActive(false);
		}
		
	}
}
