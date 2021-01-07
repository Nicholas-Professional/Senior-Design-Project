using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheFirstFloor : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Clear the First Floor";
        string description= "Clear out the remaining bandits";
        int questID=2;
        int banditAID=1;
        int banditBID=2;
        int experienceReward=500;
        int goldReward=400;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_sword");
        itemRewards.Add("iron_sword");
        itemRewards.Add("iron_sword");
        itemRewards.Add("iron_sword");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,banditAID,"Defeat 5 Bandit Fighters",false,0,5));
        questInfo.goals.Add(new KillGoal(questInfo,banditBID,"Defeat 6 Bandit Rogues",false,0,6));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
