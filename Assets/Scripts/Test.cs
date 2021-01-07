using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	
	public Player mainCharacter;

	void Start()
	{
		mainCharacter = GameObject.Find("Player").GetComponent<Player>();
		mainCharacter.setName("Blank");
		//mainCharacter.setBackground("Blank");
	}

	public void createCharacter(string playerName)
    {
		if (playerName == "")
			playerName = "Blank";

		mainCharacter.setName(playerName);
    }
	
	public Character getCharacter()
	{
		return mainCharacter;
	}
	
	public void giveClass(string playerClass)
	{
		
		Classes player = null;
		if (playerClass == "Warrior") {
			player = new Warrior(mainCharacter); 
		}
		else if(playerClass == "Scout") {
			player = new Scout(mainCharacter);
		}
		else if(playerClass == "Mage") {
			player = new Mage(mainCharacter, null);
		}
		
		
		mainCharacter.setClass(player);
	}
	
	public void giveBoon(string boon) {
		Mage temp = (Mage)(mainCharacter.playerClass);
		temp.setBoon(boon);
	}
	
	public void giveName(string playerName)
	{
		mainCharacter.setName(playerName);
	}
}


