using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheWoods : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Clear the Woods";
        string description= "A half-giant's pet spiders escaped and is now attacking people in the elf woods.  Pacify them.";
        int questID=8;
        int spiderID=10;
        int experienceReward=1600;
        int goldReward=500;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("diamond_sword");
        itemRewards.Add("diamond_sword");
        itemRewards.Add("diamond_sword");
        itemRewards.Add("diamond_sword");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,spiderID,"Pacify 20 spiders",false,0,20));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
