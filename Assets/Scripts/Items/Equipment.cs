using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Equipment : GenericItem
{
    // one-handed, two-handed, head, chest, arms, legs, feet
    public string location;

    // Increased armor, stats, reflect damage, etc.
    public string effect;

    // How much armor, stats, etc.
    public Stats[] stats;

    public Equipment(EquipmentInfo x): base((GenericItemInfo) x){
        this.effect=x.effect;
        this.location=x.location;
        this.stats=x.stats;
    }
}

[System.Serializable]
public class EquipmentInfo : GenericItemInfo{
    public string location;
    public string effect;
    public Stats[] stats;
}