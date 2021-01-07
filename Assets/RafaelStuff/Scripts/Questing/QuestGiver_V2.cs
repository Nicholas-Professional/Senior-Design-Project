using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver_V2 : NPC
{
    public bool assignedQuest{get; set;}
    public bool helped{get;set;}
    [SerializeField]
    private GameObject quests;
    [SerializeField]
    private string questType;
    private UIQuest quest{get; set;}
    public override void Interact()
    {
        if(!assignedQuest && !helped){
            //TODO: assign quest
            base.Interact();
            AssignQuest();
        }
        else if(assignedQuest && !helped){
            //TODO: check complete
            CheckQuest();
        }
        else{
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "Thanks for the help!" });
        }
    }
    void AssignQuest(){
        assignedQuest  =true;
        quest =(UIQuest)quests.AddComponent(System.Type.GetType(questType));
    }
    void CheckQuest(){
        if(quest.questInfo.Completed){
            quest.GiveReward();
            helped =true;
            assignedQuest=false;
            //TODO:quest finish dialogue
            //DialogueSystem.Instance.AddNewDialogue(new string[] { " Thanks for that! Here's your reward." });
        }
        else{
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "You have active quests come back when its complete" });
        }
    }
}
