using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

	public GameObject input1;
	public GameObject input2;
	public GameObject button1;
	public GameObject button2;
	public GameObject input;
	public Test testCanvas;
	public GameObject stats;

	// Continue to Stat Allocation
	public void Confirm()
	{
		Debug.Log(testCanvas.mainCharacter.playerClass.className);
		Debug.Log((testCanvas.mainCharacter.playerClass as Mage)?.elementalBoon);
		//testCanvas.giveClass(TrackDropDown.playerClass);
		button1.SetActive(false);
		button2.SetActive(false);
		stats.SetActive(true);
		input.SetActive(false);
	}

	// Reset character creation
	public void TryAgain()
	{
		input1.SetActive(false);
		input2.SetActive(true);
		button1.SetActive(false);
		button2.SetActive(false);
		
	}

}
