using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public static bool QuestLogIsOpen=false;
    Player playerComponent;

    private QuestSelector selectedQuest;

    [SerializeField]
    private Text questDescription;

    private static QuestLog Instance;

    public GameObject questSelectorPrefab;
    public Transform questSelectorParent;

    public static QuestLog MyInstance{
        get{
            if(Instance==null){
                Instance = FindObjectOfType<QuestLog>();
            }
            return Instance;
        }
    }
    void Start()
    {
        playerComponent=GameObject.Find("Player").GetComponent<Player>();
        foreach(Quest x in playerComponent.quests){
            GameObject temp = Instantiate(questSelectorPrefab,questSelectorParent);
            temp.GetComponent<Text>().text =x.questName;

            temp.GetComponent<QuestSelector>().MyQuest=x;
        }
        foreach(Quest x in playerComponent.completed){
            GameObject temp = Instantiate(questSelectorPrefab,questSelectorParent);
            temp.GetComponent<Text>().text =x.questName;
            temp.GetComponent<QuestSelector>().MyQuest=x;
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            if(QuestLogIsOpen){
                OnQuestLogClose();
            }
            else{
                OnQuestLogOpen();
            }
        }
    }

    public void OnQuestLogOpen(){
        
        //sets the Panel with the quest log stuff to active.
        transform.Find("QuestLogPanel").gameObject.SetActive(true);
        QuestLogIsOpen=true;
    }

    public void OnQuestLogClose(){
        transform.Find("QuestLogPanel").gameObject.SetActive(false);
        QuestLogIsOpen=false;
    }
    public void OnQuestAccepted(Quest quest){
        GameObject temp = Instantiate(questSelectorPrefab,questSelectorParent);
        temp.GetComponent<Text>().text =quest.questName;

        temp.GetComponent<QuestSelector>().MyQuest=quest;
    }
    public void ShowDescription(QuestSelector quest){
        if(selectedQuest!=null){
            selectedQuest.DeSelect();
        }
        selectedQuest=quest;

        string description=quest.MyQuest.Description;
        string questName=quest.MyQuest.questName;
        string output=questName+"\n\nDetails:\n"+description+"\n\nGoal(s):\n";
        foreach(Goal x in quest.MyQuest.goals){
            output=output+x.Description+"\n";
        }
        output=output+"\nBritain Reward:\n" + quest.MyQuest.goldReward+"\n";
        output=output+"\nExperience Reward:\n" + quest.MyQuest.experienceReward+"\n";
        foreach(string z in quest.MyQuest.ItemReward){
            GenericItemInfo a =ItemDatabase.Instance.FindItem(z);
            if(a!=null){
                output=output+"\nItem Reward:\n"+a.itemName;
            }
        }
        
        questDescription.text=string.Format(output);
    }
}
