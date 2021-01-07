using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject==GameObject.Find("Player")){
            Interact();
        }
    }
    public override void Interact()
    {
        Debug.Log("Interacting with object");
    }
}
