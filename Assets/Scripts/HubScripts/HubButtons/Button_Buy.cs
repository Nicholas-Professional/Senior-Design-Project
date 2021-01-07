using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Buy : Button_Base
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
    [SerializeField]
    private GameObject notEnoughBritainPanel;

    public void BuyButtonPressed()
    {
        // Grab item data
        newItemInfo = ItemDatabase.Instance.FindItem(objectSlug);
        
        CheckPlayerBritain();

        DeleteThenRemakeButtonsForInventoryPanels();

        HideParentPanel();
    }
    
    // If enough Britain, move item from Shop to Player
    void CheckPlayerBritain()
    {
        if (playerScript.gold >= newItemInfo.cost)
            ReduceBritainAndMoveItemFromShopToPlayer();
        else
            NotEnoughBritain();

    }

    void ReduceBritainAndMoveItemFromShopToPlayer()
    {
        playerScript.gold -= newItemInfo.cost;

        MoveItemFromShopToPlayer();
    }

    void MoveItemFromShopToPlayer()
    {
        playerScript.items.Add(newItemInfo);

        shopContentPanel.GetComponent<Inventory>().RemoveItemFromShopList(objectSlug);
    }

    void NotEnoughBritain()
    {
        notEnoughBritainPanel.SetActive(true);
    }
}
