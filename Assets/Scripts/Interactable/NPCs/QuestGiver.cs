using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestGiver : NPC
{
    public UIQuest questObject;

    public List<UIQuest> questList;

    public Player player;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    public bool assignedQuest;
    public bool helped;
    [SerializeField]
    private GameObject quests;
    [SerializeField]
    private string questType;

    public string[] questInProgressDialogue;

    public string[] questTurnIn;
    public string[] helpedDialogue;
    void Start(){
        player=GameObject.Find("Player").GetComponent<Player>();
        //TODO: Write code that will set which quest we will try to give
        //check the player for their active quest then set it to first quest in the list that is not
        //first check the player's completed list
        if(player.completed!=null){
            for(int i=0; i < player.completed.Count; i++){
                for(int j =0; j < questList.Count; j++){
                    //checks the quests in the player's completed list with the ones in the quest list
                    //and if they match sets the current quest to be the next one in the list unless
                    //it is the final quest when it sets the quest giver to helped
                    if(player.completed[i].QuestID==questList[j].questInfo.QuestID){
                        if(j==questList.Count-1){
                            helped=true;
                            return;
                        }
                        else{
                            questObject=questList[j+1];
                        }
                    }
                }
            }
        }
        else{
            questObject=questList[0];
        }
        if(player.quests!=null){
            foreach(Quest quest in player.quests){
                Debug.Log(quest.questName+" " +questObject.questInfo.questName);
                if(quest.QuestID==questObject.questInfo.QuestID){
                    assignedQuest=true;
                    break;
                }
            }
        }
        if(player.completed!=null){
            foreach(Quest quest in player.completed){
                if(quest.QuestID == questObject.questInfo.QuestID){
                    assignedQuest=false;
                    helped=true;
                    break;
                }
            }
        }
        dialogue=questObject.getStartDialogue();
        questInProgressDialogue=questObject.getInProgressDialogue();
        questTurnIn=questObject.getCompleteDialogue();
    }

    public void OpenQuestWindow(){
        questWindow.SetActive(true);
        titleText.text = questObject.questInfo.questName;
        descriptionText.text = questObject.questInfo.Description;
        experienceText.text = questObject.questInfo.experienceReward.ToString();
        goldText.text = questObject.questInfo.goldReward.ToString();
    }

    public void AcceptQuest(){
        questWindow.SetActive(false);
        assignedQuest=true;
        //accept quest
        questObject.questInfo.Accept();
        //give quest to player
        player.quests.Add(questObject.questInfo);
        //give quest to the Quest Log
        QuestLog.MyInstance.OnQuestAccepted(questObject.questInfo);
    }
     public override void Interact()
    {   
        if(!assignedQuest && !helped){
            bool isGiveRewardDialogue=false;
            //TODO: assign quest
            DialogueSystem.Instance.AddNewDialogueQuestGiver(dialogue,NPCname,this,isGiveRewardDialogue);
            //OpenQuestWindow();
        }
        else if(assignedQuest && !helped){
            //TODO: check complete
            Debug.Log("Checking Quest");
            CheckQuest();
        }
        else{
            DialogueSystem.Instance.AddNewDialogue(helpedDialogue,NPCname,this);
        }
    }
    void AssignQuest(){
        assignedQuest  =true;
        OpenQuestWindow();
        //questObject =(UIQuest)quests.AddComponent(System.Type.GetType(questType));
    }
    void CheckQuest(){
        //TODO: Update so that it looks at the player's quest not the quest object
        bool isComplete=false;
        foreach(Quest quest in player.quests){
            if(quest.QuestID == questObject.questInfo.QuestID){
                isComplete=quest.Completed;
                break;
            }
        }
        if(isComplete){
            assignedQuest=false;
            bool giveRewardDialogue=true;
            DialogueSystem.Instance.AddNewDialogueQuestGiver(questTurnIn,NPCname,this,giveRewardDialogue);
            //trying to move the quest to the completed list
            for(int i =0; i < player.quests.Count; i++){
                if(player.quests[i].QuestID==questObject.questInfo.QuestID){
                    //add it to the completed list
                    player.completed.Add(player.quests[i]);
                    //remove it from the active list
                    player.quests.Remove(player.quests[i]);
                    break;
                }
            }
        }
        else{
            DialogueSystem.Instance.AddNewDialogue(questInProgressDialogue,NPCname,this);
        }
    }
    //gets called by dialogue system so that it only gets triggered after dialogue is finished.
    public void GiveReward(){
        GameObject[] x= GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject y in x){
            if(y.GetComponent<Player>() != null){
                Player currentPlayer=y.GetComponent<Player>();
                currentPlayer.addExp(questObject.questInfo.experienceReward);
                currentPlayer.gold+=questObject.questInfo.goldReward;
                if(questObject.questInfo.ItemReward != null){
                    foreach(string z in questObject.questInfo.ItemReward){
                        currentPlayer.items.Add(ItemDatabase.Instance.FindItem(z));
                    }
                }
            }
            else if(y.GetComponent<TeamMember>() != null){
                TeamMember currentMember = y.GetComponent<TeamMember>();
                currentMember.addExp(questObject.questInfo.experienceReward);
            }
        }
        //set the next quest if there is one
        setNextQuest();
    }

    public void setNextQuest(){
        int currentIndex = questList.IndexOf(questObject);
        //if its not the last quest in the chain
        if(currentIndex!=questList.Count-1){
            questObject=questList[currentIndex+1];
            //resets the dialogue
            dialogue=questObject.getStartDialogue();
            questInProgressDialogue=questObject.getInProgressDialogue();
            questTurnIn=questObject.getCompleteDialogue();
        }
        else{
            helped=true;
        }
    }
}
