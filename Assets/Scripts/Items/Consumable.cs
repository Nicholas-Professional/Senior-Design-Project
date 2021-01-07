using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Consumable : GenericItem
{
    // Heal health, restore mana, deal damage, etc.
    public string effect;
    
    // How much health, mana, damage, etc.
    public int amount;

    public Consumable(ConsumableInfo x): base((GenericItemInfo) x){
        this.effect=x.effect;
        this.amount=x.amount;
    }
}

[System.Serializable]
public class ConsumableInfo : GenericItemInfo{
    public string effect;
    public int amount;
}