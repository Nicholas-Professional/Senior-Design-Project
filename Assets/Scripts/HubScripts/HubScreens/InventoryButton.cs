using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField]
    private Image myIcon;
    [SerializeField]
    private GameObject itemScreen;
    [SerializeField]
    private GameObject inventoryObject;
    
    ////TODO: Change to GenericItemInfo
    private GenericItem itemObject;

    public void ShowItemScreen()
    {
        // Send new item object to itemScreen
        itemScreen.GetComponent<ItemScreen>().SetObjectVariables(itemObject, inventoryObject);
        itemScreen.SetActive(true);
    }

    public void SetIcon(GenericItem newItem)
    {
        myIcon.sprite = newItem.iconSprite;
        itemObject = newItem;
    }
}
