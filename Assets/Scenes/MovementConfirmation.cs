using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovementConfirmation : MonoBehaviour
{

    public static bool movementConfirmed = false;
    public CombatManager manager;
    public static Vector3 playerPosition;
    private int i = 0;
    // Update is called once per frame
    void Update()
    {
        if (manager.getTurnEntity().tag == "Player" && manager.getState() == State.movement && i == 0)
        {
            Debug.Log("Player's Start Position: " + manager.getTurnEntity().transform.position);
            playerPosition = manager.getTurnEntity().transform.position;
            Debug.Log("Players position: " + playerPosition);
            i++;
        }
        else if(movementConfirmed)
        {
            i--;
        }
    }
}
