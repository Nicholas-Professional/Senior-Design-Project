using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheSwamp : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Clear the Swamp";
        string description= "The tide was stopped but someone needs to finish off the stragglers and stop the living fauna trying to reclaim their territory.";
        int questID=12;
        int draugrA=13;
        int draugrB=14;
        int swampy=15;
        int experienceReward=2200;
        int goldReward=750;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        itemRewards.Add("giga_health_potion");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,draugrA,"Defeat 6 Draugr Warriors",false,0,6));
        questInfo.goals.Add(new KillGoal(questInfo,draugrB,"Defeat 7 Draugr Mages",false,0,7));
        questInfo.goals.Add(new KillGoal(questInfo,swampy,"Defeat 4 Swamp Monsters",false,0,4));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
