using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : UIQuest
{
    // Start is called before the first frame update
    void Start()
    {
        string questName="Zombie Attack";
        string description= "One of the scouts must of disturbed something, we now have Draugrs pouring out of the woodwork.  Stop the tide of decaying flesh!";
        int questID=11;
        int draugrA=13;
        int draugrB=14;
        int experienceReward=2000;
        int goldReward=700;
        List<string> itemRewards=new List<string>();
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        itemRewards.Add("mana_potion");
        questInfo=new Quest(questName,description,experienceReward,goldReward,questID,itemRewards);
        questInfo.startDialogue=startDialogue;
        questInfo.inProgressDialogue=inProgressDialogue;
        questInfo.questCompleteDialogue=completeDialogue;
        questInfo.goals.Add(new KillGoal(questInfo,draugrA,"Defeat 9 Draugr Warriors",false,0,9));
        questInfo.goals.Add(new KillGoal(questInfo,draugrB,"Defeat 9 Draugr Mages",false,0,9));
        questInfo.goals.ForEach(g=>g.Init());
    }
}
