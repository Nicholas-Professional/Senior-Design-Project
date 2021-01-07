using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTheRitual : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Stop the Ritual";
        string description= "The Great Odin is doing something not so great. Stop him before Midgard is destroyed.";
        int questID=13;
        int valkyrieA=16;
        int valkyrieB=17;
        int odin=18;
        int experienceReward=2400;
        int goldReward=10000;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("wooden_sword");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,valkyrieA,"Defeat 5 Valkyrie Warriors",false,0,5));
        questInfo.goals.Add(new KillGoal(questInfo,valkyrieB,"Defeat 5 Valkyrie Mages",false,0,5));
        questInfo.goals.Add(new KillGoal(questInfo,odin,"Defeat Odin",false,0,1));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
