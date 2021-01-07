using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCultRemnants : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Clear Cult Remnants";
        string description= "Stop the cultists from regrouping and open the way to the next floor";
        int questID=4;
        int cultistA=3;
        int cultistB=4;
        int experienceReward=1500;
        int goldReward=300;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("wizard_staff");
        itemRewards.Add("wizard_staff");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,cultistA,"Defeat 6 Cult Mages",false,0,6));
        questInfo.goals.Add(new KillGoal(questInfo,cultistB,"Defeat 7 Cult Guards",false,0,7));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
