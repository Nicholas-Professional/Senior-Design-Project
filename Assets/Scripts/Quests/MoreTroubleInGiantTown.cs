using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreTroubleInGiantTown : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="More Trouble with \"Giants\"";
        string description= "Now leaderless the giants are attacking everyone in sight; end them.";
        int questID=6;
        int giantA=6;
        int experienceReward=1400;
        int goldReward=450;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("diamond_helmet");
        itemRewards.Add("diamond_helmet");
        itemRewards.Add("diamond_helmet");
        itemRewards.Add("diamond_helmet");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,giantA,"Defeat 15 \"Giants\"",false,0,15));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
