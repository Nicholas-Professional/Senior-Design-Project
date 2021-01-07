using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSlime1 : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName = "Kill the Slimes 1";
        string Description = "Kill a group of 5 slimes.";
        //TODO: reward them with item from item database
        //ItemReward = ItemDatabase.Instance.GetItem("potion_log");
        int experienceReward = 100;
        int goldReward =4224;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_chestplate");
        questInfo = new Quest(questName, Description, experienceReward,goldReward,0,itemRewards);
        //when we have save go here to update the currentAmount
        questInfo.goals.Add(new KillGoal(questInfo,0,Description, false,0,5));

        questInfo.goals.ForEach(g => g.Init());
    }

    
}
