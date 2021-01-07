using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBritain : MonoBehaviour
{
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Text textComponent = gameObject.GetComponent<Text>();
        textComponent.text = playerScript.gold.ToString();
    }
}
