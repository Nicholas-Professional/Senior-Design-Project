using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[System.Serializable]
public class Quest
{
    [SerializeField]
    public List<Goal> goals = new List<Goal>();
    [SerializeField]
    public string questName;
    [SerializeField]
    public string Description;
    [SerializeField]
    public int experienceReward;
    [SerializeField]
    public int goldReward;
    public List<string> ItemReward;
    public bool Completed;
    public bool accepted;

    public int QuestID;

    public string[] startDialogue;
    public string[] inProgressDialogue;
    public string[] questCompleteDialogue;

    public Quest(string questName, string Description, int experience, int gold,int ID, List<string> itemSlug){
        this.questName=questName;
        this.Description=Description;
        this.experienceReward=experience;
        this.goldReward=gold;
        this.QuestID = ID;
        this.ItemReward=itemSlug;
    }

    public void CheckGoals(){
        Debug.Log("Checking goals");
        Completed = goals.All(g => g.Completed);
    }

    public void GiveReward(){
        Debug.Log("Getting Rewards");
        //if(ItemReward != null){
            //TODO: implement give reward here
            //InvetoryController.instance.GiveItem(ItemReward);
       // }
    }
    public void Accept(){
        accepted=true;
    }
    public void Complete(){
        Completed=true;
    }

}
