using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfBlockade : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Elf Blockade";
        string description= "The Elfs are a mistrusting lot, see if you can \"convince\" them to let you through";
        int questID=7;
        int lightElf=8;
        int darkElf=9;
        int experienceReward=1700;
        int goldReward=500;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("diamond_leggings");
        itemRewards.Add("diamond_leggings");
        itemRewards.Add("diamond_leggings");
        itemRewards.Add("diamond_leggings");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,lightElf,"Defeat 7 light elves",false,0,7));
        questInfo.goals.Add(new KillGoal(questInfo,darkElf,"Defeat 7 dark elves",false,0,7));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
