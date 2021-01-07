using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Goal{    
    //public Quest Quest;
    public string Description;
    public bool Completed;
    public int CurrentAmount;
    public int RequiredAmount;
    public int typeID;

    public virtual void Init(){
        //default init stuff
    }
    public void Evaluate(){
        if(CurrentAmount >= RequiredAmount){
            Complete();
        }
    }
    public void Complete(){
        
        //Quest.CheckGoals();
        Completed = true;
        GameEvents.OnGoalCompleted(this);
    }
    
}
