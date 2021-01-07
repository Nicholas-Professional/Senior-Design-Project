using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestItem : GenericItem
{
    public int QuestID;

    public QuestItem(QuestItemInfo x): base((GenericItemInfo) x){
        this.QuestID=x.QuestID;
    }
}

[System.Serializable]
public class QuestItemInfo : GenericItemInfo{
    public int QuestID;
}