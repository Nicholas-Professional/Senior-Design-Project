using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelector : MonoBehaviour
{
    public Quest MyQuest{
        get;
        set;
    }
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(){
        originalColor=GetComponent<Text>().color;
        GetComponent<Text>().color=Color.yellow;

        QuestLog.MyInstance.ShowDescription(this);
    }
    public void DeSelect(){
        GetComponent<Text>().color=originalColor;
    }
}
