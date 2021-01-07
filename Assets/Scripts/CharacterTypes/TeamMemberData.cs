using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeamMemberData{
    public string characterName;
    public int level;
   // public string background;
    public int exp;
    public Dictionary<string, Stats> stats;
    public List<string> items;

    public List<string> equipment = new List<string>();
    public string className;

    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;
    public string elementalBoon;

    public string[] equipped=new string[4];
}
