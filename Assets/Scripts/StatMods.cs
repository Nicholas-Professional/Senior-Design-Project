using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatMods 
{
	public readonly float value;
	public readonly object Source; 
	
	public StatMods(float val, object source) {
		value = val; 
		Source = source; 
	}
	
	public StatMods(float val) : this(val, null){}

}
