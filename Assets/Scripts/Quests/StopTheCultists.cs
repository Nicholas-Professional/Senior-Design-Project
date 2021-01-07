using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTheCultists : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Stop the Cultists";
        string description= "Defeat the cultists and stop the mysterious ritual.";
        int questID=3;
        int cultistA=3;
        int cultistB=4;
        int cultistLeaderID=5;
        int experienceReward=1000;
        int goldReward=600;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_helmet");
        itemRewards.Add("iron_helmet");
        itemRewards.Add("iron_helmet");
        itemRewards.Add("iron_helmet");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,cultistA,"Defeat 4 Cult Mages",false,0,4));
        questInfo.goals.Add(new KillGoal(questInfo,cultistB,"Defeat 5 Cult Guards",false,0,5));
        questInfo.goals.Add(new KillGoal(questInfo,cultistLeaderID,"Defeat the Cultist Leader",false,0,1));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
