using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Player : Character
{
    public static Player Instance{get;set;}
    public int gold;
    [SerializeField]
    public List<Quest> quests;
     [SerializeField]
    public List<Quest> completed;

    //public List<Equipment> equipped;
    // List as done above OR some dictionary so  dict[item.location] if != null
    [SerializeField]
    private List<string> startItemSlugs;
    public List<GenericItemInfo> items;
//variable for enemy/interactable/other for quest related stuff.

    public string classSelection;
    public string elementalBoon;
	

    public void completeQuest(){
        //get rewards from quest
        gold+=quests[0].goldReward;
        exp+=quests[0].experienceReward;
        //move the quest to completed list
        completed.Add(quests[0]);
        //mark complete
        completed[0].Complete();
        //remove the quest form quest list
        quests.Remove(quests[0]);
    }

    public void Awake(){
        DontDestroyOnLoad(this);
    }
    private void Start(){
       
        if(Instance!=null && Instance !=this){
            Destroy(gameObject);
        }   
        else{
            Instance=this;
            CombatEvents.OnEnemyDeath+=EnemyToExperience;
            GameEvents.GoalCompleted+=CheckQuestsForCompletedGoals;
       }

        foreach (string objectSlug in startItemSlugs)
        {
            GenericItemInfo startItemInfo = ItemDatabase.Instance.FindItem(objectSlug);
            items.Add(startItemInfo);
        }

        if(classSelection=="Mage"){
            playerClass=new Mage(this,elementalBoon);
        }
        else if(classSelection=="Scout"){
            playerClass=new Scout(this);
        }
        else{
            playerClass=new Warrior(this);
        }

        Debug.Log(characterName+" is a "+ playerClass.className);
    }
    public PlayerInfo GetPlayerInformation(){
        return makePlayerData();
    }

    public void LoadPlayerInformation(PlayerInfo loadedInformation){
        quests.Clear();
        completed.Clear();
        items.Clear();
        this.characterName=loadedInformation.playerName;
        this.classSelection=loadedInformation.className;
        this.gold=loadedInformation.gold;
        this.Level=loadedInformation.level;
       // this.background=loadedInformation.background;
        this.exp=loadedInformation.exp;
        
        this.stats=loadedInformation.stats;
        foreach(Quest x in loadedInformation.activeQuests){
            x.goals.ForEach(g => g.Init());
            quests.Add(x);

        }
        foreach(Quest x in loadedInformation.completedQuests){
            completed.Add(x);
        }
        if(loadedInformation.equipment != null){
            foreach(string objectSlug in loadedInformation.equipment){
                items.Add(ItemDatabase.Instance.FindItem(objectSlug));
                
            }
        }

        if(loadedInformation.className.Equals("Mage")){
            playerClass=new Mage(this,loadedInformation.elementalBoon);
            classSelection=loadedInformation.className;
        }
        else if(loadedInformation.className.Equals("Scout")){
            playerClass=new Scout(this);
            classSelection=loadedInformation.className;
        }
        else{
            playerClass=new Warrior(this);
            classSelection=loadedInformation.className;
        }

        for(int i=0; i<4;i++){
            this.equiped[i]=(EquipmentInfo)ItemDatabase.Instance.FindItem(loadedInformation.equipped[i]);
        }
        this.currentHealth=loadedInformation.currentHP;
        this.maxHealth=loadedInformation.maxHP;
        this.maxMana=loadedInformation.maxMP;
        this.currentMana=loadedInformation.currentMP;
    }
    public void CheckQuestsForCompletedGoals(Goal goal){
        foreach(Quest quest in quests){
            quest.CheckGoals();
        }
    }

    public PlayerInfo makePlayerData(){
        PlayerInfo x=new PlayerInfo();
        x.playerName = this.characterName;
        x.gold = this.gold;
        x.level = this.Level;
       // this.background = player.background;
        x.exp=this.exp;
        x.stats=this.stats;
        x.activeQuests = this.quests;
        x.completedQuests = this.completed;
        x.currentHP=this.currentHealth;
        x.maxHP=this.maxHealth;
        x.currentMP=this.currentMana;
        x.maxMP=this.maxMana;
        foreach(GenericItemInfo y in this.items){
            x.equipment.Add(y.objectSlug);
        }
        for(int i=0; i < this.equiped.Length;i++){
            if(this.equiped[i]!=null){
                x.equipped[i]=this.equiped[i].objectSlug;
            }
        }
        x.className=this.playerClass.className;
        if(x.className.Equals("Mage")){
            x.elementalBoon=((Mage)this.playerClass).elementalBoon;
        }
        else{
            x.elementalBoon="none";
        }

        return x;
    }
}
