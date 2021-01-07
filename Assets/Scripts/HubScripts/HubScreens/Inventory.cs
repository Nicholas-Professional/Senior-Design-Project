using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{ 
    [SerializeField]
    private bool isShop;
    [SerializeField]
    private GameObject buttonTemplate;

    // Unique to this script
    private List<string> itemSlugs;
    private string shopObjectName;
    private List<GenericItem> inventoryList;

    ////TODO: change from GenericItem to GenericItemInfo for all scripts
    private List<GenericItemInfo> inventoryInfoList;

    void Start()
    {
        inventoryInfoList = GameObject.Find("Player").GetComponent<Player>().items;

        // If Player, populate Inventory on Start
        if (!isShop)
        {
            CopyInformation(null);
        }
    }

    // Add itemSlugs to list based on if Shop or PlayerInventory
    public void CopyInformation(string newShopObjectName)
    {
        itemSlugs = new List<string>();

        if(isShop)
        {
            shopObjectName = newShopObjectName;

            GameObject shopObject = GameObject.Find(shopObjectName);
            itemSlugs = shopObject.GetComponent<OpenShop>().itemSlugs;
        }
        else
        {
            shopObjectName = null;
            foreach (GenericItemInfo itemInfo in inventoryInfoList)
            {
                itemSlugs.Add(itemInfo.objectSlug);
            }
        }

        GenerateInventoryFromItemSlugs();
    }

    void GenerateInventoryFromItemSlugs()
    {
        inventoryList = new List<GenericItem>();

        foreach (string objectSlug in itemSlugs)
        {
            GenericItemInfo newItemInfo = ItemDatabase.Instance.FindItem(objectSlug);

            Sprite iconSprite = Resources.Load<Sprite>(newItemInfo.spritePath);

            if (newItemInfo.type == "consumable")
            {
                Consumable newItem = gameObject.AddComponent<Consumable>();

                newItem.objectSlug = newItemInfo.objectSlug;
                newItem.type = newItemInfo.type;
                newItem.itemName = newItemInfo.itemName;
                newItem.description = newItemInfo.description;
                newItem.cost = newItemInfo.cost;
                newItem.iconSprite = iconSprite;
                newItem.effect = ((ConsumableInfo)newItemInfo).effect;
                newItem.amount = ((ConsumableInfo)newItemInfo).amount;
                
                inventoryList.Add(newItem);
            }
            else if (newItemInfo.type == "equipment")
            {
                Equipment newItem = gameObject.AddComponent<Equipment>();

                newItem.objectSlug = newItemInfo.objectSlug;
                newItem.type = newItemInfo.type;
                newItem.itemName = newItemInfo.itemName;
                newItem.description = newItemInfo.description;
                newItem.cost = newItemInfo.cost;
                newItem.iconSprite = iconSprite;
                newItem.location = ((EquipmentInfo)newItemInfo).location;
                newItem.effect = ((EquipmentInfo)newItemInfo).effect;
                newItem.stats = ((EquipmentInfo)newItemInfo).stats;
                
                inventoryList.Add(newItem);
            }
            else    // Quest item
            {
                Debug.Log("Quest item " + objectSlug + " in some inventory list");
            }
        }
        
        CreateButtonsFromInventoryList();
    }


    ////TODO: update shop list as quests are completed
    ////TODO: Change foreach to for loop and load x number of items based on quest completion
    public void CreateButtonsFromInventoryList()
    {
        if (inventoryList == null)
            return;
        
        // Create Buttons in the shop screen with Sprite images
        foreach (GenericItem newItem in inventoryList)
        {
            // Create a new button
            GameObject itemButton = Instantiate(buttonTemplate) as GameObject;
            itemButton.SetActive(true);

            // Set itemButton sprite and parent
            itemButton.GetComponent<InventoryButton>().SetIcon(newItem);
            itemButton.transform.SetParent(buttonTemplate.transform.parent, false);
        }
    }

    public void RemoveItemFromShopList(string oldObjectSlug)
    {
        // Edit shopObject List then reload
        GameObject shopObject = GameObject.Find(shopObjectName);
        List<string> shopObjectSlugs = shopObject.GetComponent<OpenShop>().itemSlugs;
        shopObjectSlugs.Remove(oldObjectSlug);
    }

    public void AddItemToShopList(string oldObjectSlug)
    {
        // Edit shopObject List then reload
        GameObject shopObject = GameObject.Find(shopObjectName);
        List<string> shopObjectSlugs = shopObject.GetComponent<OpenShop>().itemSlugs;
        shopObjectSlugs.Add(oldObjectSlug);
    }

    public void DeleteOldButtons()
    {
        if (transform == null)
            return;

        foreach (Transform child in transform)
            if (child.name != "Button")
                Destroy(child.gameObject);
    }

    public void DeleteThenRemakeButtons()
    {
        DeleteOldButtons();
        CopyInformation(shopObjectName);
    }    
}
