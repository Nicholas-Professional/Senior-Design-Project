using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButton : MonoBehaviour
{

    [SerializeField]
    private Image myIcon;
    [SerializeField]
    private GameObject itemScreen;
    [SerializeField]
    private GameObject inventoryObject;
    [SerializeField]
    private GenericItemInfo itemObject;
    
    public void ShowItemScreen(){
        //send new item object to itemScreen
        if(itemObject!=null && itemObject.objectSlug!=""){
            Debug.Log("Show Screen");
            Debug.Log(itemScreen.name);
            itemScreen.SetActive(true);
            itemScreen.GetComponent<InventoryUIScreen>().SetObjectVariables(itemObject,inventoryObject);
        }
    }

    public void SetIcon(GenericItemInfo newItem){
        if(newItem!=null){
            myIcon.GetComponent<Image>().enabled=true;
            myIcon.sprite = Resources.Load<Sprite>(newItem.spritePath);
            itemObject=newItem;
        }
        else{
            myIcon.GetComponent<Image>().enabled=false;
            itemObject=null;
        }
    }
}
