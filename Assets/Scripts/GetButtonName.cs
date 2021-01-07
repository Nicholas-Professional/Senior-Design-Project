using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetButtonName : MonoBehaviour
{
		
	public static string button;
	public static bool pressed;
	
	public void OnClicked(Button butt)
	{
		button = butt.name;
		pressed = true; 
	}

}
