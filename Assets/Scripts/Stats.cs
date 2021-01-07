using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[Serializable]
public class Stats 
{
	//Base value of the stat
	public float Base;
	private bool SetUp;
	private float _value; 
	//List of all of the Stat modifiers for a particular stat 
	private readonly List<StatMods> statModifiers; 
	
	public Stats(float Basevalue) {
		SetUp = true; 
		Base = Basevalue;
		statModifiers = new List<StatMods>();
	}
	
public float Value { 
	get {
		if(SetUp)  {
		_value = CalculateFinalValue();
		SetUp = false;
		}
		return _value; 
	}
}

//Determine the modified stat final number
private float CalculateFinalValue()
{
    float finalValue = Base;
 
    for (int i = 0; i < statModifiers.Count; i++)
    {
        finalValue += statModifiers[i].value;
    }
    // Rounding gets around dumb float calculation errors (like getting 12.0001f, instead of 12f)
    // 4 significant digits is usually precise enough, but feel free to change this to fit your needs
    return (float)Math.Round(finalValue, 4);
}

//Add modifiers to the List 
public void AddModifier(StatMods mod)
{
    statModifiers.Add(mod);
	SetUp = true; 
}

//Remove modifiers to the List
public bool RemoveModifier(StatMods mod)
{
	SetUp = true; 
    return statModifiers.Remove(mod);
}

public bool RemoveAllModifiersFromSource(object source)
{
    bool didRemove = false;
 
    for (int i = statModifiers.Count - 1; i >= 0; i--)
    {
        if (statModifiers[i].Source == source)
        {
            SetUp= true;
            didRemove = true;
            statModifiers.RemoveAt(i);
        }
    }
    return didRemove;
}
   
 
}
