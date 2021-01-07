using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Sell : Button_Base
{
    // Inheritted from Button_Base:
    // GameObject playerContentPanel;
    // GameObject shopContentPanel;    
    // GameObject inventoryObject;
    // GameObject inventoryObject;
    // string objectSlug;
    // GameObject playerObject
    // Player playerScript;

    private GenericItemInfo newItemInfo;

    public void SellButtonPressed()
    {
        // Grab item data
        newItemInfo = ItemDatabase.Instance.FindItem(objectSlug);

        AddBritainAndMoveItemFromPlayerToShop();

        DeleteThenRemakeButtonsForInventoryPanels();

        HideParentPanel();
    }

    void AddBritainAndMoveItemFromPlayerToShop()
    {
        playerScript.gold += newItemInfo.cost;

        MoveItemFromPlayerToShop();
    }

    void MoveItemFromPlayerToShop()
    {
        playerScript.items.Remove(newItemInfo);

        shopContentPanel.GetComponent<Inventory>().AddItemToShopList(objectSlug);
    }

}
