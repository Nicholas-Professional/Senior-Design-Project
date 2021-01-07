using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField]
    GameObject mainPanel;

    [SerializeField]
    GameObject stancePanel;
    [SerializeField]
    GameObject warriorPanel;
    [SerializeField]
    GameObject roguePanel;
    [SerializeField]
    GameObject magePanel;
    [SerializeField]
    GameObject itemPanel;
    [SerializeField]
    GameObject sorryPanel;
    [SerializeField]
    GameObject enemyPanel;
    [SerializeField]

    Button stanceButton;

	[SerializeField]
	GameObject movementPanel;
    Character currentlyActive;
    CombatManager manager;
	public static bool canMoveAgain = true;

    void Start(){
        manager = GameObject.Find("Manager").GetComponent<CombatManager>();
    }

    void Update(){
		
		if(AbilityManager.targeting) {
			closeAllPanels();
		}
		
        if(manager.getState()==State.actions || (manager.getTurnEntity() != null && manager.getTurnEntity().tag=="Enemy")){
            OnTurnStart();
        }
		else if(manager.getState()==State.movement && GridManager.finished && !canMoveAgain) {
			CheckMovement();
		}
        else{
            OnTurnEnd();
        }
    }

    public void OnTurnStart(){
		movementPanel.SetActive(false);
        mainPanel.SetActive(true);
        currentlyActive=manager.getTurnEntity().GetComponent<Character>();
        if(currentlyActive==null){
            //set the mainPanel to false and have the enemy turn thing show up
            enemyPanel.SetActive(true);
        }
        else{
            mainPanel.SetActive(true);
            if(currentlyActive.getClass().className!="Warrior"){
                stanceButton.interactable=(false);
            }
            else{
                stanceButton.interactable=true;
            }
        }
    }
    public void OnTurnEnd(){
        enemyPanel.SetActive(false);
        mainPanel.SetActive(false);
        closeAllPanels();
    }
	
	public void CheckMovement() {
		movementPanel.SetActive(true);
	}

    public void OnStanceSelected(){
        closeAllPanels();
        stancePanel.SetActive(true);
    }

    public void OnOffenseSelected(){
        //send stance change to the combat manager

        closeAllPanels();
    }

    public void OnDefenseSelected(){
        //send stance change to the combat manager
        
        closeAllPanels();
    }

    public void OnAbilityPanelSelected(){
        //get who is the active character and then call up their class
        closeAllPanels();
        Classes activeClass=currentlyActive.getClass();
        if(activeClass.className=="Warrior"){
            warriorPanel.SetActive(true);
        }
        else if(activeClass.className=="Mage"){
            magePanel.SetActive(true);
        }
        else if(activeClass.className=="Scout"){
            roguePanel.SetActive(true);
        }
        else{
            return;
        }
    }
	
	public void ConfirmMovement() {
		MovementConfirmation.movementConfirmed = true;
		canMoveAgain = true;
		closeAllPanels();
	}
	
	public void TryAgain() {
		manager.getTurnEntity().transform.position = MovementConfirmation.playerPosition;
		canMoveAgain = true;
		closeAllPanels();
	}

    public void OnAbilitySelected(int selection){
        //based on the number input, send the respective ability to the combat manager
        if(currentlyActive.getClass().className=="Warrior"){
            if(selection == 0){
                //attempt strike
            }
            else if(selection == 1){
                //attempt shove
            }
            else if(selection == 2){
                //attempt ArtOfWar
            }
        }
        else if(currentlyActive.getClass().className=="Scout"){
            if(selection == 0){
                //attempt strike
            }
            else if(selection==1){
                //attempt bleeding strike
            }
        }
        else if(currentlyActive.getClass().className=="Mage"){
            if(selection == 0){
                //attempt strike
            }
            else if(selection == 1){
                //attempt Ice Spike
            }
            else if(selection == 2){
                //attempt Rock Blast
            }
            else if(selection == 3){
                //attempt Siphoning Strike
            }
            else if(selection == 4){
                //attempt Fire Ball
            }
        }

        closeAllPanels();
    }

    public void OnItemSelect(){
        closeAllPanels();
        itemPanel.SetActive(true);
    }

    public void UseHealthItem(){
        if(currentlyActive.currentHealth==currentlyActive.maxHealth){
            //Do nothing
            return;
        }
        Player player=GameObject.Find("Player").GetComponent<Player>();

        bool itemUsed=false;
        foreach(GenericItemInfo x in player.items){
            if(x.type=="consumable"){
                ConsumableInfo y = ((ConsumableInfo)x);
                if(y.effect=="heal"){
                    currentlyActive.Heal(y.amount);
                    player.items.Remove(x);
                    //TODO:tell combat manager to move to next state;
                    AbilityManager.completed=true;
                    return;
                }
            }
        }
        if(!itemUsed){
            closeAllPanels();
            sorryPanel.SetActive(true);
        }
    }

    public void UseManaItem(){
        if(currentlyActive.currentMana==currentlyActive.maxMana){
            //Do nothing
            return;
        }
        Player player=GameObject.Find("Player").GetComponent<Player>();
        bool itemUsed=false;
        foreach(GenericItemInfo x in player.items){
            if(x.type=="consumable"){
                ConsumableInfo y = ((ConsumableInfo)x);
                if(y.effect=="restore"){
                    currentlyActive.GainMana(y.amount);
                    player.items.Remove(x);
                    //TODO:tell combat manager to move to next state;
                    AbilityManager.completed=true;
                    return;
                }
            }
        }
        if(!itemUsed){
            closeAllPanels();
            sorryPanel.SetActive(true);
        }
    }


    public void closeAllPanels(){
        stancePanel.SetActive(false);
        warriorPanel.SetActive(false);
        roguePanel.SetActive(false);
        magePanel.SetActive(false);
        itemPanel.SetActive(false);
        sorryPanel.SetActive(false);
		movementPanel.SetActive(false);
    }





}
