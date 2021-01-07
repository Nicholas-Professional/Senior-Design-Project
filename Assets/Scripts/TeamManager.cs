using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public static TeamManager Instance{get;set;}
    public int teamSize;

    public bool[] isAlive;
    public bool[] Available;
    
    public GameObject[] teamCharacters;

    public void Awake(){
        DontDestroyOnLoad(this);
    }

    private void Start(){
        if(Instance!=null && Instance !=this){
            Destroy(gameObject);
        }   
        else{
            Instance=this;
       }
       GameEvents.PlayerDied+=CheckWhichPlayerDied;
       isAlive=new bool[teamSize];
       Available=new bool[teamSize];
       teamCharacters = new GameObject[teamSize];
       //add all the player characters to the list
       teamCharacters[0]=transform.Find("Player").gameObject;
       teamCharacters[1]=transform.Find("TeamMember1").gameObject;
       teamCharacters[2]=transform.Find("TeamMember2").gameObject;
       teamCharacters[3]=transform.Find("TeamMember3").gameObject;
       for(int i = 0; i < teamCharacters.Length;i++){
           if(teamCharacters[i] != null){
               isAlive[i]=true;
               Available[i]=true;
           }
       }
       for(int i = 1; i <teamCharacters.Length; i++){
           if((teamCharacters[i].GetComponent<TeamMember>()).isLoaded==false){
               isAlive[i]=false;
               Available[i]=false;
           }
       }
    }

    public void CheckWhichPlayerDied(GameObject player){
        for(int i =0; i < teamCharacters.Length; i++){
            //checks if the player that died is in the list and if they are set them to dead
            if(GameObject.ReferenceEquals(player, teamCharacters[i])){
                isAlive[i]=false;
                break;
            }
        }
        int countOfAlive=0;
        //checks if all players are dead
        foreach(bool x in isAlive){
            if(x){
                countOfAlive++;
            }
        }
        if(countOfAlive==0){
            TeamWiped();
        }

    }

    public void TeamWiped(){
        GameObject deathScreen=GameObject.Find("CanvasWithDeathMenu");
        GameObject x = deathScreen.transform.Find("DeathScreen").gameObject;
        x.SetActive(true);
    }

    public GameObject[] getAvailableAndAliveTeamMembers(){
        List<GameObject> members=new List<GameObject>();
        for(int i =0; i<Available.Length;i++){
            if(isAlive[i] && Available[i]){
                members.Add(teamCharacters[i]);
            }
        }
        return members.ToArray();
    }
    public GameObject[] getAliveTeamMembers(){
        List<GameObject> aliveMembers=new List<GameObject>();
        for(int i =0; i<Available.Length;i++){
            if(isAlive[i]){
                aliveMembers.Add(teamCharacters[i]);
            }
        }
        return aliveMembers.ToArray();
    }

    public bool checkIfAlive(GameObject member){
        for(int i = 0; i < teamCharacters.Length; i++){
            if(GameObject.ReferenceEquals(member, teamCharacters[i])){
                return isAlive[i];
            }
        }
        return false;
    }
    public void checkIfTeamMemberActive(){
        for(int i = 1; i <teamCharacters.Length; i++){
            if(teamCharacters[i].GetComponent<TeamMember>()!=null && teamCharacters[i].GetComponent<TeamMember>().characterName=="No Name" ){
                isAlive[i]=false;
                Available[i]=false;
            }
            else if(teamCharacters[i].GetComponent<TeamMember>()==null){
                //do nothing if its null
            }
            else{
                isAlive[i]=true;
                Available[i]=true;
            }
        }
    }

    public void ReviveAll(){
        for(int i=0; i<isAlive.Length;i++){
            isAlive[i]=true;
        }
    }


}
