using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIUseButton : MonoBehaviour
{
    [SerializeField]
    protected InventoryUIManager playerInventory;
    [SerializeField]
    protected GenericItemInfo theItem;
    protected Player player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player").GetComponent<Player>();
    }

    public void SetGameObject(GenericItemInfo item1){
        this.theItem=item1;
    }

    //update the UI
    public void CallingUpdateUI(){
        playerInventory.UpdateUI();
    }

    public void UseButtonPressed(){
        if(theItem.type=="equipment"){
            EquipmentInfo equip=(EquipmentInfo)theItem;
            EquipTheItem(equip);
        }
        else if(theItem.type=="consumable"){
            ConsumableInfo consumable=(ConsumableInfo)theItem;
            ConsumeTheItem(consumable);
        }
    }

    public void EquipTheItem(EquipmentInfo toBeEquiped){
        int area=0;
        if(toBeEquiped.location=="head"){
            area=0;
        }
        else if(toBeEquiped.location=="body"){
            area=1;
        }
        else if(toBeEquiped.location=="legs"){
            area=2;
        }
        else if (toBeEquiped.location=="weapon"){
            area=3;
        }
        else{
            return;
        }
        Debug.Log("Area to be equipped to:" + area);
        
        if(playerInventory.currentCharacterSelection[area]!=null && !string.IsNullOrEmpty(playerInventory.currentCharacterSelection[area].objectSlug)){
            Debug.Log("Item was returned to inventory");
            //return item in the slot to inventory
            player.items.Add(playerInventory.currentCharacterSelection[area]);
        }
        //remove the item from inventory and then add it to the equiped slot
        player.items.Remove(toBeEquiped);
        playerInventory.currentCharacterSelection[area]=toBeEquiped;
        //removed from player inventory
        
        Debug.Log("Items left in the inventory:"+player.items.Count);
        //should update the UI
        CallingUpdateUI();
    }

    public void ConsumeTheItem(ConsumableInfo toBeConsumed){
        //affect the active character selection somehow
    }

}
