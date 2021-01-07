using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIUnequipButton : InventoryUIUseButton
{
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player").GetComponent<Player>();
    }

    public void UnequipButtonUsed(){
        if(theItem!=null && theItem.objectSlug!=""){
            UnequipTheItem((EquipmentInfo)theItem);
        }
    }

    public void UnequipTheItem(EquipmentInfo theItem){
        int area=0;
        if(theItem.location=="head"){
            area=0;
        }
        else if(theItem.location=="body"){
            area=1;
        }
        else if(theItem.location=="legs"){
            area=2;
        }
        else if(theItem.location=="weapon"){
            area=3;
        }
        else{
            return;
        }

        player.items.Add(theItem);
        playerInventory.currentCharacterSelection[area]=null;
        playerInventory.selectionSlots[area].GetComponent<InventoryUIButton>().SetIcon(null);

        CallingUpdateUI();
    }
}
