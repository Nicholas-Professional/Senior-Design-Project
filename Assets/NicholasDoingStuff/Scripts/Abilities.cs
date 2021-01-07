using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public abstract class Abilities {

	[SerializeField]
	protected internal Classes playerClass; 
	public string abilityName;
	public int baseAmount = 0;
	public string scalingStat;
	public string damageType;
	public string debuff;
	public string buff;
	public int cost; 
	public bool blast;
	public bool targetted; 
	public List<Vector3> aoe = new List<Vector3>();
	public abstract void resolve(List<object> requirements);
	
}

//[System.Serializable]
/*public class Passive: Abilities {

	public string triggersOn;

	public Passive() {
		
	}

	public void resolve() {
		
	}
	

	

	
	
}*/

