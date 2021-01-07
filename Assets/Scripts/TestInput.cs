using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInput : MonoBehaviour
{
	public GameObject input1;
	public GameObject input2;
	public Test testCanvas;
    // Update is called once per frame
    void Update()
    {
		
		if(Input.GetKeyDown(KeyCode.Return))
		{
			input1.SetActive(false);
			input2.SetActive(true);
			Debug.Log(TrackDropDown.playerClass);
			testCanvas.giveClass(TrackDropDown.playerClass);
			if(TrackDropDown.playerClass == "Mage") {
				Debug.Log(CheckForBoon.Boon);
					testCanvas.giveBoon(CheckForBoon.Boon);
			}
        }
	}
}
