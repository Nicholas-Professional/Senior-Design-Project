using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTheLavaPath : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Clear the Lava Path";
        string description= "Now that we know that to expect, stop the local fauna from stopping the convoy";
        int questID=10;
        int magmaGob=11;
        int magmaSlime=12;
        int experienceReward=1800;
        int goldReward=600;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("super_health_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,magmaGob,"Defeat 8 Lava Goblin",false,0,8));
        questInfo.goals.Add(new KillGoal(questInfo,magmaSlime,"Defeat 8 Lava Slime",false,0,8));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
