using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TeamMember : Character
{
    public GameObject leader;

    public List<EquipmentInfo> equipped;

    public bool isLoaded;

    public string classSelection;
    public string elementalBoon;

    public void Start(){
        if(characterName=="No Name"){
            isLoaded=false;
        }
        else{
            isLoaded=true;
        }
        if(classSelection.Equals("Mage")){
            playerClass=new Mage(this,elementalBoon);
            Debug.Log(characterName+" is a "+ playerClass.className);
        }
        else if(classSelection.Equals("Scout")){
            playerClass=new Scout(this);
            Debug.Log(characterName+" is a "+ playerClass.className);
        }
        else{
            playerClass=new Warrior(this);
            Debug.Log(characterName+" is a "+ playerClass.className);
        }
    }

    //this code will have the team member follow the leader
    public void followLeader(){
        Player activeLeader =leader.GetComponent<Player>();

    }
    //this code will be used to locate and attack enemies
    public void attack(){

    }
    //try to heal
    public void heal(){
        
    }

    //TODO: Incorporate this with save/load to load the team member
    //used to get data from the save file / teammate database and then transfer it to the team member
    public void LoadingTeamMemberInfo(TeamMemberData data){
        this.characterName=data.characterName;
        this.stats=data.stats;
        //this.background=data.background;
        this.Level=data.level;
        this.exp=data.exp;
        if(data.items!=null){
            foreach(string slugs in data.items){
                equipped.Add((EquipmentInfo)ItemDatabase.Instance.FindItem(slugs));
            }
        }
        isLoaded=true;

        if(data.className.Equals("Mage")){
            playerClass=new Mage(this,data.elementalBoon);
            classSelection=data.className;
        }
        else if(data.className.Equals("Scout")){
            playerClass=new Scout(this);
            classSelection=data.className;
        }
        else{
            playerClass=new Warrior(this);
            classSelection=data.className;
        }

        for(int i=0; i<4;i++){
            this.equiped[i]=(EquipmentInfo)ItemDatabase.Instance.FindItem(data.equipped[i]);
        }

        this.currentHealth=data.currentHP;
        this.maxHealth=data.maxHP;
        this.maxMana=data.maxMP;
        this.currentMana=data.currentMP;
        this.elementalBoon=data.elementalBoon;
    }

    public TeamMemberData GetTeamMemberInformation(){
        return MakeTeamMemberData();
    }

    public TeamMemberData MakeTeamMemberData(){
        TeamMemberData y=new TeamMemberData();
        y.characterName=this.characterName;
        y.level=this.Level;
       // this.background=member.background;
        y.exp=this.exp;
        y.stats=this.stats;
        if(this.equipped!=null){
            foreach(EquipmentInfo x in this.equipped){
                y.items.Add(x.objectSlug);
            }
        }

        y.currentHP=this.currentHealth;
        y.maxHP=this.maxHealth;
        y.currentMP=this.currentMana;
        y.maxMP=this.maxMana;

        for(int i=0; i < this.equiped.Length;i++){
            if(this.equiped[i]!=null){
                y.equipped[i]=this.equiped[i].objectSlug;
            }
        }
        y.className=this.classSelection;
        if(this.Equals("Mage")){
            y.elementalBoon=((Mage)this.playerClass).elementalBoon;
        }
        else{
            y.elementalBoon="none";
        }

        return y;
    }

}

