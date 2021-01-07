using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForBoon : MonoBehaviour
{
	//public Text question;;
	public GameObject input; 
	Dropdown drop;
	public static string Boon;
	int index;

		
		void Start()
	{
		drop = GetComponent<Dropdown>();
	}
    // Update is called once per frame
    void Update()
    {
		index = drop.value;
		Boon = drop.options[index].text;
		
		if(Input.GetKeyDown(KeyCode.Return) ) {
			input.SetActive(false);
		}
        
    }
}
