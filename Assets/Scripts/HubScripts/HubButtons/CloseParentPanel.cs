using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseParentPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject shopContentPanel;
    [SerializeField]
    private List<GameObject> siblingPanels;

    public void Continue()
    {
        // Hide parent panel (***Button must be placed directly below parent panel in hierarchy)
        GameObject parentPanel = gameObject.transform.parent.gameObject;
        parentPanel.SetActive(false);

        // On close of either InventoryPanel, delete old buttons in ShopPanel
        if (shopContentPanel != null)
            shopContentPanel.GetComponent<Inventory>().DeleteOldButtons();

        // Hide any sibling panels of the parent panel (PlayerInventory or other shops)
        foreach (GameObject sibling in siblingPanels)
        {
            if (sibling.activeSelf)
                sibling.SetActive(false);
        }
    }
}
