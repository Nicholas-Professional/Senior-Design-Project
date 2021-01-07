using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
	private GameObject text;
	public string question; 
    // Start is called before the first frame update
    void Start()
    {
       text = GameObject.Find("Question");
	   text.GetComponent<Text>().text = question; 
    }

    // Update is called once per frame
    void Update()
    {
       text = GameObject.Find("Question");
	   text.GetComponent<Text>().text = question; 
    }
}
