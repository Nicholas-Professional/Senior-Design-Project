using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public string target;

    public bool isReached(){
        return (currentAmount >= requiredAmount);
    }
    public void EnemyKilled(GameObject enemy){
        //pass in the tag of enenmy killed as an argument 
        //and check if that enemy if the one required for this quest
        if(goalType== GoalType.Kill && enemy.tag==target){
            currentAmount++;
        }
    }
    public void ItemCollected(GameObject collectable){
        //pass in the tag of item collected as an argument 
        //and check if that item if the one required for this quest
        if(goalType== GoalType.Gathering && collectable.tag==target){
            currentAmount++;
        }
    }
    
}

//types of quests
public enum GoalType{
    Kill,
    Gathering,
    Protect,
    Clear
}
