using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenericItem : MonoBehaviour
{
    // equipment or consumable
    public string type;
    public string objectSlug;
    public string itemName;
    public string description;
    //TODO: public bool stackable;

    // How much the item will sell for/ cost to buy
    public int cost;

    // Store the sprite for the item
    public Sprite iconSprite;

    public GenericItem(GenericItemInfo x){
        this.objectSlug=x.objectSlug;
        this.type=x.type;
        this.itemName=x.itemName;
        this.description =x.description;
        this.cost =x.cost;
        this.iconSprite =Resources.Load<Sprite>(x.spritePath);
    }

}

[System.Serializable]
public class GenericItemInfo{
    public string objectSlug;
    public string type;
    public string itemName;
    public string description;
    public int cost;
    public string spritePath;
}
