using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScreen : MonoBehaviour
{
    ////TODO: Change to GenericItemInfo
    private GenericItem itemObject;
    [SerializeField]
    private GameObject itemScreenButton;

    public void SetObjectVariables(GenericItem itemObject, GameObject inventoryObject)
    {
        this.itemObject = itemObject;
        itemScreenButton.GetComponent<Button_Base>().SetGameObject(itemObject.objectSlug, inventoryObject);
        ChangeItemScreen();
    }

    private void ChangeItemScreen()
    {
        // Set Text values based on stored item values
        Text[] textComponents = gameObject.GetComponentsInChildren<Text>();

        // 0 is the exit ButtonText, 1 itemName, 2 description, 3 is Britain:, 4 is cost
        textComponents[1].text = itemObject.itemName;
        textComponents[2].text = itemObject.description;
        textComponents[4].text = itemObject.cost.ToString();


        // Add item sprite
        Image[] images = gameObject.GetComponentsInChildren<Image>();

        ////TODO: Change to GenericItemInfo
        //Sprite iconSprite = Resources.Load<Sprite>(newItemInfo.spritePath);

        images[2].sprite = itemObject.iconSprite;
    }
}
