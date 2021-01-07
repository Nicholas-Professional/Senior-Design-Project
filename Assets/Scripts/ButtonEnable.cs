using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEnable : MonoBehaviour
{
	
	public GameObject tryAgainButton; 
	public GameObject text;
	public GameObject question;
	public GameObject canvas;
    
	

    // Change Text to display Player's info
    void Update()
    {
		question.GetComponent<Text>().text = "Does your name and class look correct to you?";
		
		text.GetComponent<Text>().text = "Name: " + canvas.GetComponent<Test>().mainCharacter.characterName +
			"\nClass: " + canvas.GetComponent<Test>().mainCharacter.playerClass.className;

        tryAgainButton.SetActive(true);
    }
}
