using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSkeleton1 : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName = "Kill the Skeleton";
        string Description = "Kill a Skeleton.";
        int skeletonID=2;
        int experienceReward = 10;
        int goldReward =20;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_helmet");
        questInfo = new Quest(questName, Description, experienceReward,goldReward,0,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,skeletonID,Description, false,0,1));

        questInfo.goals.ForEach(g => g.Init());
    }
}
