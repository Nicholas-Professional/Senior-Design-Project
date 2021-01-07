using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance {get;set;}
    void Start()
    {
        if(Instance!=null && Instance!=this){
            Destroy(gameObject);
        }
        else{
            Instance=this;
        }
    }
}
