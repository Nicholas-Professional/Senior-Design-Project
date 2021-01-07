using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance{get;set;}
    [SerializeField]
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();

    Button continueButton;
    Text dialogueText,nameText;
    public int dialogueIndex;
    public bool isQuestGiver;
    public bool questGiverRewarding;
    public Interactable interactableObject;
    // Start is called before the first frame update
    void Awake(){
        continueButton=dialoguePanel.transform.Find("ContinueButton").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
        nameText= dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();
        continueButton.onClick.AddListener(delegate{ ContinueDialogue(); });
        dialoguePanel.SetActive(false);
        if(Instance!=null && Instance !=this){
            Destroy(gameObject);
        }
        else{
            Instance=this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName,Interactable interactableObject){
        dialogueIndex=0;
        dialogueLines=new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName=npcName;
        this.interactableObject=interactableObject;
        isQuestGiver=false;
        CreateDialogue();
    }

    public void CreateDialogue(){
        dialogueText.text=dialogueLines[dialogueIndex++];
        nameText.text=npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue(){
        if(dialogueIndex < dialogueLines.Count){
            dialogueText.text=dialogueLines[dialogueIndex];
            dialogueIndex++;
        }
        else{
            dialoguePanel.SetActive(false);
            if(isQuestGiver){
                
                
                if(questGiverRewarding){
                    Debug.Log("Trying to Give Rewards");
                    ((QuestGiver)interactableObject).GiveReward();
                }
                else{
                    Debug.Log("Trying to Open Quest Window");
                    ((QuestGiver)interactableObject).OpenQuestWindow();
                }
            }
        }
    }

    public void AddNewDialogueQuestGiver(string[] lines, string npcName, QuestGiver giver, bool QuestCompleted){
        AddNewDialogue(lines,npcName,giver);
        isQuestGiver=true;
        questGiverRewarding=QuestCompleted;
    }
}
