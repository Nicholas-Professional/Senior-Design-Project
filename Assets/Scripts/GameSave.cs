using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    
    // Start is called before the first frame update
    GameObject playerObject;
    void Start()
    {
        GameEvents.SaveInitiated+=Save;
        GameEvents.LoadInitiated+=Load;
        //playerObject=GameObject.Find("Player");
        
    }
    void OnDisable(){
        GameEvents.SaveInitiated-=Save;
        GameEvents.LoadInitiated-=Load;
    }
    void Save(){
        //create the SaveContents class that will store the data to be saved
        SaveContents toBeSaved = new SaveContents();
        playerObject=GameObject.Find("Player");
        //saving the player object data
        Player playerComponent=playerObject.GetComponent<Player>();
        
        toBeSaved.player=(playerComponent.GetPlayerInformation());
        

        //saving active team members
        List<TeamMemberData> activeTeam = new List<TeamMemberData>();
        for(int i =1; i<4; i++){
            activeTeam.Add(GameObject.Find("TeamMember"+i).GetComponent<TeamMember>().GetTeamMemberInformation());
        }
        toBeSaved.team=(activeTeam);
        //TODO: add other objects to be saved
        //save the contents
        SaveLoad.Save(toBeSaved,"GameInfo");
    }
    void Load(){
        if(SaveLoad.SaveExists("GameInfo")){
            SaveContents temp=(SaveLoad.Load<SaveContents>("GameInfo"));
            //this will update the players information
            playerObject=GameObject.Find("Player");
            Player playerComponent = playerObject.GetComponent<Player>();
            playerComponent.LoadPlayerInformation(temp.player);

            //try to load the active team members
            int activeTeamIndex =1;
            foreach(TeamMemberData x in temp.team){
                //if the data only has the default name it will be considered an empty team member and will not be added to the active team
                if(x.characterName!="No Name"){
                    GameObject.Find("TeamMember"+activeTeamIndex++).GetComponent<TeamMember>().LoadingTeamMemberInfo(x);
                }
                
            }
            //resets the values of alive and available to what it should be
            TeamManager.Instance.checkIfTeamMemberActive();
            //TODO: load other things to be updated
        }
    }
}
