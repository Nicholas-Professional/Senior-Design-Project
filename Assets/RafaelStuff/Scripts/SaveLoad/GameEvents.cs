using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static System.Action<Equipment> EquipmentAddedToInventory;
    public static System.Action<string> TooltipActivated;
    public static System.Action<Goal> GoalCompleted;
    public static System.Action TooltipDeactivated;
    public static System.Action SaveInitiated;
    public static System.Action LoadInitiated;
    public static System.Action DeleteAllSaveFilesInitiated;

    public static System.Action<GameObject> PlayerDied;

    

    public static void OnItemAddedToInventory(Equipment item){
        EquipmentAddedToInventory?.Invoke(item);
    }
    public static void OnTooltipActivated(string text){
        TooltipActivated?.Invoke(text);
    }
    public static void OnTooltipDeactivated(){
        TooltipDeactivated?.Invoke();
    }
    public static void OnSaveInitiated(){
        SaveInitiated?.Invoke();
    }
    public static void OnLoadInitiated(){
        LoadInitiated?.Invoke();
    }
    public static void OnGoalCompleted(Goal goal){
        GoalCompleted?.Invoke(goal);
    }
    public static void OnTeamDeath(GameObject character){
        PlayerDied?.Invoke(character);
    }
}
