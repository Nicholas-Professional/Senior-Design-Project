using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuest : MonoBehaviour
{
    [SerializeField]
    public Quest questInfo;

    public string[] startDialogue;
    public string[] inProgressDialogue;
    public string[] completeDialogue;

    public void CheckGoals(){
        questInfo.CheckGoals();
    }

    public void GiveReward(){
        questInfo.GiveReward();
    }
    public void Accept(){
        questInfo.Accept();
    }
    public void Complete(){
        questInfo.Complete();
    }
    public string[] getStartDialogue(){
        return questInfo.startDialogue;
    }
    public string[] getInProgressDialogue(){
        return questInfo.inProgressDialogue;
    }
    public string[] getCompleteDialogue(){
        return questInfo.questCompleteDialogue;
    }
}
