using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//will contain the information about the player that will get saved and loaded
public class PlayerInfo{
    public string playerName;
    public int gold;
    public int level;
    //public string background;
    public int exp;
    public Dictionary<string, Stats> stats = new Dictionary<string, Stats>();
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<string> equipment = new List<string>();
    public string className;

    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;

    public string elementalBoon;

    public string[] equipped=new string[4];
}