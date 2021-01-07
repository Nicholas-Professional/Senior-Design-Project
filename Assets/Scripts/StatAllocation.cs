using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StatAllocation : MonoBehaviour
{
	
	public static float StatPointAlloc = 10;
	public Text textStatPoints;
	public GameObject[] buttons; 
	public Test testCanvas;
	public Text STR;
	public Text DEX;
	public Text INT;
	public Text WIS;
	public Text VIT;
	public Text SPD;
	public Text LCK;
	public Text Points;  
	private float[] StatMod = new float[8]; 
	private Stats[] statCheck = new Stats[7];

	void Start()
	{
		for(int i = 0; i < statCheck.Length; i++) {
			Stats statRef = new Stats(10f);
			statCheck[i] = statRef;
			StatMod[i] = 0; 
		}
	}
	
	// Update shown stats every time a Button is pressed
	void Update() {
		
		if(GetButtonName.pressed)
		{
			GetButtonName.pressed = false; 
			string stat = GetButtonName.button;
			Allocate(stat);
			if(StatPointAlloc == 0)
				buttons[buttons.Length-1].SetActive(true); 
			else
				buttons[buttons.Length-1].SetActive(false);
		}
				
		if(testCanvas.mainCharacter != null)
		{	
			STR.text = (testCanvas.mainCharacter.stats["STR"].Value + StatMod[0]).ToString();
			DEX.text = (testCanvas.mainCharacter.stats["DEX"].Value + StatMod[1]).ToString();
			VIT.text = (testCanvas.mainCharacter.stats["VIT"].Value + StatMod[2]).ToString();
			INT.text = (testCanvas.mainCharacter.stats["INT"].Value + StatMod[3]).ToString();
			WIS.text = (testCanvas.mainCharacter.stats["WIS"].Value + StatMod[4]).ToString();
			SPD.text = (testCanvas.mainCharacter.stats["SPD"].Value + StatMod[5]).ToString();
			LCK.text = (testCanvas.mainCharacter.stats["LCK"].Value + StatMod[6]).ToString();

			DisplayStatPointsRemaining();
		}
		
	}
	
	// Limit Min and Max values for stats and change values when user clicks a Button
	public void Allocate(string stat)
	{
		switch (stat)
		{
			case string a when a.Contains("STR"):
				PerformStatMod(a, 0);
				break;
			case string a when a.Contains("DEX"):
				PerformStatMod(a, 1);
				break;
			case string a when a.Contains("VIT"):
				PerformStatMod(a, 2);
				break;
			case string a when a.Contains("INT"):
				PerformStatMod(a, 3);
				break;
			case string a when a.Contains("WIS"):
				PerformStatMod(a, 4);
				break;
			case string a when a.Contains("SPD"):
				PerformStatMod(a, 5);
				break;
			case string a when a.Contains("LCK"):
				PerformStatMod(a, 6);
				break;
		}
	}

	private void PerformStatMod(string stat, int index)
	{
		int mod = 1;
		if(stat.Contains("+")) {
			StatMod[index] += mod;
			StatPointAlloc -= mod;
			if(StatMod[index] > 4) {
			buttons[index*2].SetActive(false); 
			}
			else if(StatMod[index] > -2) {
			buttons[index*2+1].SetActive(true); 
			}			 
		}
		else {
			StatMod[index] -= mod;
			StatPointAlloc += mod;
			if(StatMod[index] == -2) {
				buttons[index*2+1].SetActive(false);
			}
			else if(StatMod[index] < 5) {
				buttons[index*2].SetActive(true); 
			}			 
		}
	}


	public void DisplayStatPointsRemaining()
	{
		textStatPoints.text = StatPointAlloc.ToString();
	}
	
	public void ConfirmModifiers() {

		// Save character stats with user's selection
		int i = 0;
		foreach(KeyValuePair<string, Stats> entity in testCanvas.mainCharacter.stats)
		{
			testCanvas.mainCharacter.stats[entity.Key].AddModifier(new StatMods(StatMod[i]));
			Debug.Log(entity.Key + ": " + testCanvas.mainCharacter.stats[entity.Key].Value);	// Test
			i++;
		}

		// Change scene to ShopHub
		SceneManager.LoadScene("ShopHubScene");
	}

}
