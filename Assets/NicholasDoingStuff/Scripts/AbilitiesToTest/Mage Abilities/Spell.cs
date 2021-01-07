using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
	protected string spellName;
	[SerializeField]
	protected Mage playerClass;
	[SerializeField]
	protected int damage;
	[SerializeField]
	protected int accuracy;
	[SerializeField]	
	protected string saveStat; 
	protected string scalingStat = "INT";
	[SerializeField]
	protected List<Vector3> aoe = new List<Vector3>();
	[SerializeField]
	protected int cost;
	[SerializeField]
	protected string elementType; 
	
	public abstract void castSpell(List<object> target);

	public string getElementalType() {
		return elementType; 
	}
}
