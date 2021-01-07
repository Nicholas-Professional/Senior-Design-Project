using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class KillGoal : Goal
{
    //id of enemy to be killed
    [SerializeField]
    public int enemyID;

    public KillGoal(Quest quest,int enemyId, string description, bool completed, int currentAmount,int requiredAmount){
        //this.Quest=quest;
        this.enemyID=enemyId;
        this.Description=description;
        this.Completed=completed;
        this.CurrentAmount=currentAmount;
        this.RequiredAmount=requiredAmount;
        this.typeID=0;
    }
    public override void Init(){
        base.Init();
        //TODO: add Combat Event System and attach here
        CombatEvents.OnEnemyDeath+=EnemyDied;
    }

    void EnemyDied(EnemyTemplate enemy){
        Debug.Log("Enemy Id"+enemy.Id);
        Debug.Log("Target Id:"+this.enemyID);
        if(enemy.Id == this.enemyID){
            this.CurrentAmount++;
            Evaluate();
        }
    }
}
