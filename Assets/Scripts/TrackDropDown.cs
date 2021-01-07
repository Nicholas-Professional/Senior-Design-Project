using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackDropDown : MonoBehaviour
{
	Dropdown drop;
	public static string playerClass;
	int index;
	
	void Start()
	{
		drop = GetComponent<Dropdown>();
	}
	
    // Update is called once per frame
    void Update()
    {
		index = drop.value;
		playerClass = drop.options[index].text;
    }
}
