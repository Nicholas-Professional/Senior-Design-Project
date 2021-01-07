using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstablishFoothold : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Establish Foothold";
        string description= "Clear the area around the tower of hostile bandits and thugs";
        int questID=1;
        int banditAID=1;
        int banditBID=2;
        int experienceReward=200;
        int goldReward=300;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_chestpiece");
        itemRewards.Add("iron_chestpiece");
        itemRewards.Add("iron_chestpiece");
        itemRewards.Add("iron_chestpiece");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,banditAID,"Defeat at least 3 Bandit Fighters",false,0,3));
        questInfo.goals.Add(new KillGoal(questInfo,banditBID,"Defeat at least 4 Bandit Rogues",false,0,4));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
