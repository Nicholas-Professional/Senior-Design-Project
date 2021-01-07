using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance{get;set;}
    public TextAsset EquipmentJSONFile;
    public TextAsset ConsumableJSONFile;
    public TextAsset QuestJSONFile;
    private List<EquipmentInfo> EquipmentList;
    private List<ConsumableInfo> ConsumablesList;
    private List<QuestItemInfo> QuestList;
    // Start is called before the first frame update

    void Awake(){
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        if(Instance!=null && Instance!=this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
            EquipmentJSONFile=Resources.Load<TextAsset>("JSONFiles/EquipmentList");
            ConsumableJSONFile=Resources.Load<TextAsset>("JSONFiles/ConsumableItemList");
            QuestJSONFile = Resources.Load<TextAsset>("JSONFiles/QuestItemList");
            EquipmentList =new List<EquipmentInfo>();
            ConsumablesList = new List<ConsumableInfo>();
            QuestList = new List<QuestItemInfo>();

            BuildDatabase();
        }
    }
    //take JSON info to build the objects
    private void BuildDatabase()
    {
        ListHelper temp = JsonUtility.FromJson<ListHelper>(EquipmentJSONFile.text);
        
        foreach(EquipmentInfo x in temp.items){
            EquipmentList.Add(x);
        }
        ConsumbaleHelper temp1 = JsonUtility.FromJson<ConsumbaleHelper>(ConsumableJSONFile.text);
        foreach(ConsumableInfo x in temp1.items){
            ConsumablesList.Add((ConsumableInfo)x);
        }
        QuestListHelper temp2= JsonUtility.FromJson<QuestListHelper>(QuestJSONFile.text);
        foreach(QuestItemInfo x in temp2.items){
            QuestList.Add((QuestItemInfo)x);
        }
    }
    public GenericItemInfo FindItem(string itemName){
        foreach(ConsumableInfo x in ConsumablesList){
            if(x.objectSlug==itemName){
                return x;
            }
        }
        foreach(EquipmentInfo x in EquipmentList){
            if(x.objectSlug==itemName){
                return x;
            }
        }
        foreach(QuestItemInfo x in QuestList){
            if(x.objectSlug==itemName){
                return x;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ListHelper{
    public List<EquipmentInfo> items;
}
[System.Serializable]
public class QuestListHelper{
    public List<QuestItemInfo> items;
}
[System.Serializable]
public class ConsumbaleHelper{
    public List<ConsumableInfo> items;
}