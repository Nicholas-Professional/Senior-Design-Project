using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Interactable : MonoBehaviour
{
    private bool hasInteracted;
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }

    }
    void Update(){
    }
    public virtual void Interact(){
        Debug.Log("Interacting with base class");
    }
}
