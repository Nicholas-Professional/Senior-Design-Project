using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateTheMagma : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Navigate the Magma";
        string description= "We know nothing about the next floor, go see what is there.";
        int questID=9;
        int magmaGob=11;
        int magmaSlime=12;
        int experienceReward=1800;
        int goldReward=600;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("archmage_staff");
        itemRewards.Add("archmage_staff");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,magmaGob,"Defeat 7 Lava Goblin",false,0,7));
        questInfo.goals.Add(new KillGoal(questInfo,magmaSlime,"Defeat 8 Lava Slime",false,0,8));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
