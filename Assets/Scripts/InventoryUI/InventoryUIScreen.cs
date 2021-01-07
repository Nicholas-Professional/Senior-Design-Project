using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScreen : MonoBehaviour
{
    [SerializeField]
    private GenericItemInfo itemObject;
    [SerializeField]
    private GameObject itemScreenButton;
    [SerializeField]
    private Button buttonText;

    public void SetObjectVariables(GenericItemInfo itemObject, GameObject inventoryObject){
        this.itemObject=itemObject;
        itemScreenButton.GetComponent<InventoryUIUseButton>().SetGameObject(itemObject);
        ChangeItemScreen();
    }

    private void ChangeItemScreen(){
        Text[] textComponents = gameObject.GetComponentsInChildren<Text>();
        textComponents[1].text=itemObject.itemName;
        textComponents[2].text=itemObject.description;
        textComponents[4].text=itemObject.cost.ToString();

        Image[] images=gameObject.GetComponentsInChildren<Image>();

        images[2].sprite=Resources.Load<Sprite>(itemObject.spritePath);
        if(itemObject.type=="equipment"){
            buttonText.interactable=true;
        }
        else{
            buttonText.interactable=false;
        }
    }
}
