using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenShop : MonoBehaviour
{
    public List<string> itemSlugs;
    [SerializeField]
    private Texture2D cursor;
    [SerializeField]
    private GameObject playerInventoryPanel;
    [SerializeField]
    private GameObject shopPanel;
    
    // Unique to related object
    private string shopObjectName;

    void Start()
    {
        shopObjectName = gameObject.name;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DisplayShopAndInventory();
        }

    }

    void DisplayShopAndInventory()
    {
        PassNameAndItemsToShopPanel();

        // Display ShopPanel and PlayerInventoryPanel if
        playerInventoryPanel.SetActive(true);
        shopPanel.SetActive(true);
    }

    void PassNameAndItemsToShopPanel()
    {
        // Grab Content object using direct path
        Transform contentObject = shopPanel.transform.Find("Scroll View");
        contentObject = contentObject.transform.Find("Viewport_BS");
        contentObject = contentObject.transform.Find("Content_BS");

        contentObject.GetComponent<Inventory>().CopyInformation(shopObjectName);
    }

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
