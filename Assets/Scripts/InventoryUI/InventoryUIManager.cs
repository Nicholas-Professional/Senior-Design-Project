using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUIManager : MonoBehaviour
{
    
    [SerializeField]
    public EquipmentInfo[] currentCharacterSelection;
    public GameObject[] selectionSlots;
    public Text characterName;
    public GameObject inventorySlotPrefab;
    public GameObject inventoryParentObject;

    public List<GenericItemInfo> inventory;
    private bool InventoryIsOpen=false;
    void Start()
    {
        Player character=GameObject.Find("Player").GetComponent<Player>();
        currentCharacterSelection=character.equiped;
        inventory=character.items;
        characterName.text=character.characterName;
        
    }

    public void Selection1(){
        Character character=GameObject.Find("Player").GetComponent<Player>();
        changeActiveCharacterEquipment(character);
    }
    public void Selection2(){
        Character character=GameObject.Find("TeamMember1").GetComponent<TeamMember>();
        changeActiveCharacterEquipment(character);
    }
    public void Selection3(){
        Character character=GameObject.Find("TeamMember2").GetComponent<TeamMember>();
        changeActiveCharacterEquipment(character);
    }
    public void Selection4(){
        Character character=GameObject.Find("TeamMember3").GetComponent<TeamMember>();
        changeActiveCharacterEquipment(character);
    }

    public void changeActiveCharacterEquipment(Character character){
        currentCharacterSelection=character.equiped;
        characterName.text=character.characterName;
        UpdateUI();

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(InventoryIsOpen){
                OnInventoryClose();
            }
            else{
                OnInventoryOpen();
            }
        }
    }

    public void OnInventoryClose(){
        InventoryIsOpen=false;
        DeleteOldButtons();
        foreach(Transform child in transform){
            child.gameObject.SetActive(false);
        }
    }
    public void OnInventoryOpen(){
        InventoryIsOpen=true;
        GameObject holder=transform.Find("PanelHolder").gameObject;
        holder.SetActive(true);
        CreateButtonsFromInventory();
        UpdateSelectionScreen();
    }

    public void UpdateUI(){
        inventory=GameObject.Find("Player").GetComponent<Player>().items;
        DeleteThenRemakeButtons();
        UpdateSelectionScreen();
    }

    public void CreateButtonsFromInventory(){
        Debug.Log("Making Inventory buttons");
        if(inventory==null){
            Debug.Log("Inventory is null");
            return;
        }
        Debug.Log("Trying to make button");
        Debug.Log(inventory.Count);
        foreach(GenericItemInfo x in inventory){
            //create new Button
            GameObject itemButton=Instantiate(inventorySlotPrefab) as GameObject;
            Debug.Log("Button Instianted");
            itemButton.SetActive(true);
            
            //set the sprite and the parent of the button
            itemButton.GetComponent<InventoryUIButton>().SetIcon(x);
            itemButton.transform.SetParent(inventoryParentObject.transform,false);
            Debug.Log("Made a button in inventory");
        }
    }

    public void RemoveItemFromInventory(GenericItemInfo itemToRemove){
        List<GenericItemInfo> temp=GameObject.Find("Player").GetComponent<Player>().items;
        foreach(GenericItemInfo x in temp){
            if(x.Equals(itemToRemove)){
                temp.Remove(x);
                return;
            }
        }
        UpdateUI();
    }

    public void AddItemToInventory(GenericItemInfo itemToAdd){
        List<GenericItemInfo> temp=GameObject.Find("Player").GetComponent<Player>().items;
        temp.Add(itemToAdd);
        UpdateUI();
    }

    public void DeleteOldButtons(){
        if(transform==null){
            return;
        }
        //kill all the children
        foreach(Transform child in inventoryParentObject.transform){
            Destroy(child.gameObject);
        }
    }

    public void DeleteThenRemakeButtons(){
        DeleteOldButtons();
        CreateButtonsFromInventory();
    }

    public void UpdateSelectionScreen(){
        
        for(int i =0; i < selectionSlots.Length; i++){
            if(currentCharacterSelection[i]!=null && currentCharacterSelection[i].objectSlug!=""){
                selectionSlots[i].GetComponent<InventoryUIButton>().SetIcon(currentCharacterSelection[i]);
            }
            else{
                selectionSlots[i].GetComponent<InventoryUIButton>().SetIcon(null);
            }
        }
    }
}
