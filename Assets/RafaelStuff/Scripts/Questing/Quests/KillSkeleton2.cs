using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSkeleton2 : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName = "Kill the Skeleton 2";
        string Description = "Kill two Skeletons in Level 1.";
        int skeletonID=2;
        int experienceReward = 30;
        int goldReward =40;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("iron_helmet");
        questInfo = new Quest(questName, Description, experienceReward,goldReward,1,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,skeletonID,Description, false,0,2));

        questInfo.goals.ForEach(g => g.Init());
    }
}
