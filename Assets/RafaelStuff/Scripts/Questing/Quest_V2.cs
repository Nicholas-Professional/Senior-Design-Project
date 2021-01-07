using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest_V2 : MonoBehaviour
{
    public List<Goal> goals {get;set;} = new List<Goal>();
    public string questName {get; set;}
    public string Description{get;set;}
    public int experienceReward{ get; set;}
    public int goldReward{get;set;}
    public Equipment ItemReward{get;set;}
    public bool Completed{get; set;}

    public void CheckGoals(){
        Completed = goals.All(g => g.Completed);
    }

    public void GiveReward(){
        if(ItemReward != null){
            //TODO: implement give reward here
            //InvetoryController.instance.GiveItem(ItemReward);
        }
    }

}
