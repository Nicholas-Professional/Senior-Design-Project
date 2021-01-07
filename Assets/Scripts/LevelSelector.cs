using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    [SerializeField]
    private Button level1;
    [SerializeField]
    private Button level2;
    [SerializeField]
    private Button level3;
    [SerializeField]
    private Button level4;
    [SerializeField]
    private Button level5;
    [SerializeField]
    private Button level6;
    [SerializeField]
    private Button final;
    [SerializeField]
    private GameObject selector;

    //TODO: Update with actual Quest names/ID
    public void OnOpenLevelSelect(){
        Player player =GameObject.Find("Player").GetComponent<Player>();
        //first disable all the buttons except for level 1 (it will always be available)
        level2.interactable=false;
        level3.interactable=false;
        level4.interactable=false;
        level5.interactable=false;
        level6.interactable=false;
        final.interactable=false;
        //scans active quests for level permission
        if(player.quests!=null){
            foreach(Quest x in player.quests){
                if(x.QuestID==3){
                    level2.interactable=true;
                }
                else if(x.QuestID==5){
                    level3.interactable=true;
                }
                else if(x.QuestID==7){
                    level4.interactable=true;
                }
                else if(x.QuestID==9){
                    level5.interactable=true;
                }
                else if(x.QuestID==11){
                    level6.interactable=true;
                }
                else if(x.QuestID==13){
                    final.interactable=true;
                }
            }
        }
        //checks completed quests for permission
        if(player.completed!=null){
            foreach(Quest x in player.completed){
                if(x.QuestID==3 || x.QuestID==4){
                    level2.interactable=true;
                }
                else if(x.QuestID==5 || x.QuestID==6){
                    level3.interactable=true;
                }
                else if(x.QuestID==7 || x.QuestID==8){
                    level4.interactable=true;
                }
                else if(x.QuestID==9 || x.QuestID==10){
                    level5.interactable=true;
                }
                else if(x.QuestID==12 || x.QuestID==11){
                    level6.interactable=true;
                }
                else if(x.QuestID==13){
                    final.interactable=true;
                }
            }
        }
        //makes the panel visible
        selector.SetActive(true);
    }

    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4(){
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5(){
        SceneManager.LoadScene("Level5");
    }
    public void LoadLevel6(){
        SceneManager.LoadScene("Level6");
    }
    public void LoadFinal(){
        SceneManager.LoadScene("Final");
    }
}
