using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class NPC : Interactable
{
    public string[] dialogue;
    public string NPCname;

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }

    }
    // TODO: turn virtual to override when interactable is made instead
    public override void Interact(){
        //TODO: implement dialogue system
        DialogueSystem.Instance.AddNewDialogue(dialogue,NPCname,this);
    }
}
