using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Races {
	
	protected string name;
	protected string description;
	protected Dictionary<string, StatMods> racials = new Dictionary<string, StatMods>();
		//private Abilities[] racialAlb;
		
		public string getName() {
			return this.name; 
		}
		
		public string getDescription() {
			return this.description;
		}
		
		public Dictionary<string, StatMods> getRacials() {
			return this.racials; 
		}
		
	
}

public class Human : Races {
	
	public Human() {
		name = "Human";
		description = "A jack of all trades, a hardy people that brave any harsh conditions thrown their way";
		racials.Add("STR", new StatMods(1, this));
		racials.Add("DEX", new StatMods(1, this));
		racials.Add("VIT", new StatMods(1, this));
		racials.Add("INT", new StatMods(1, this));
		racials.Add("WIS", new StatMods(1, this));
		racials.Add("SPD", new StatMods(1, this));
		racials.Add("LCK", new StatMods(1, this));
	}
	
}

public class Elf : Races{
	
	public Elf() {
		name = "Elf";
		description = "Graceful, elegant, long lived, they strive for balance with nature and perserving their way of life";
		racials.Add("DEX", new StatMods(2, this));
		racials.Add("INT", new StatMods(2, this));
		racials.Add("SPD", new StatMods(2, this));
		racials.Add("VIT", new StatMods(-2, this));
		racials.Add("STR", new StatMods(-2, this));
	}
	
	
}

public class Dwarf : Races{
	
	public Dwarf() {
		name = "Dwarf";
		description = "Hardy and stout with a temper to match, they are stubborn folk with a mighty swing and sturdy frame";
		racials.Add("STR", new StatMods(2, this));
		racials.Add("VIT", new StatMods(2, this));
		racials.Add("WIS", new StatMods(2, this));
		racials.Add("DEX", new StatMods(-2, this));
		racials.Add("INT", new StatMods(-2, this));
	}
	
	
}

