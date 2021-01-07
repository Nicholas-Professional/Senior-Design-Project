using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Base : MonoBehaviour
{
    [SerializeField]
    protected GameObject playerContentPanel;
    [SerializeField]
    protected GameObject shopContentPanel;
    
    protected Inventory playerInventory;
    protected GameObject inventoryObject;
    protected string objectSlug;
    protected Player playerScript;

    void Start()
    {
        // Grab Player and Inventories at start
        playerScript = GameObject.Find("Player").GetComponent<Player>();

        playerInventory = playerContentPanel.GetComponent<Inventory>();
    }
    
    public void SetGameObject(string objectSlug, GameObject inventoryObject)
    {
        this.objectSlug = objectSlug;
        this.inventoryObject = inventoryObject;
    }

    protected void DeleteThenRemakeButtonsForInventoryPanels()
    {
        playerContentPanel.GetComponent<Inventory>().DeleteThenRemakeButtons();
        shopContentPanel.GetComponent<Inventory>().DeleteThenRemakeButtons();
    }

    protected void HideParentPanel()
    {
        GameObject parentPanel = gameObject.transform.parent.gameObject;
        parentPanel.SetActive(false);
    }
}
