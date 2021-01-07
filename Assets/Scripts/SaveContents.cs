using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//This will be used to save nearly all of the data related to the same into a single save file
public class SaveContents
{
    public PlayerInfo player;
    public List<TeamMemberData> team = new List<TeamMemberData>();

}
