using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfQuestActive : MonoBehaviour
{
    public Player mc;
    // Update is called once per frame
    public GameObject questObject;
    void Update()
    {
        if(mc.quests.Count>0 && mc.quests[0].accepted){
            questObject.SetActive(true);
        }
        else if(mc.quests.Count==0){
            questObject.SetActive(false);
        }
        
    }
}
