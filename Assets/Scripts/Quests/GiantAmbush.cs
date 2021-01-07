using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAmbush : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="\"Giant\" Ambush";
        string description= "Now that you have been warned about the ambush, it is time to ambush the ambushers.";
        int questID=5;
        int giantA=6;
        int giantLeader=7;
        int experienceReward=1500;
        int goldReward=400;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("diamond_chestpiece");
        itemRewards.Add("diamond_chestpiece");
        itemRewards.Add("diamond_chestpiece");
        itemRewards.Add("diamond_chestpiece");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,giantA,"Defeat 10 \"Giants\"",false,0,10));
        questInfo.goals.Add(new KillGoal(questInfo,giantLeader,"Defeat Giant Leader",false,0,1));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
