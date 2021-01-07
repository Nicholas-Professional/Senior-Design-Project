using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Warrior : Classes 
{
	string stance = ""; 
	
	public Warrior(Character player) {
		user = player;
		skills.Add(new ArtofWar(this));
		skills.Add(new Shove(this));
		className = "Warrior";
	}
	
	public override void updatePassives(List<object> updates) {
		
		bool changed = false; 
		if(stance != (string)updates[0]) {
		stance = (string)updates[0];
		user.removeMods(this);
		changed = true; 
		}
		
		if(stance == "Offensive" && updates.Count == 2 && changed) {
			Dictionary<string, StatMods> offensive = new Dictionary<string, StatMods>();
			offensive.Add("STR", new StatMods(2f, this));
			offensive.Add("AC", new StatMods(-2f, this));
			user.addMods(offensive);
			Strike((EnemyTemplate)updates[1], 1.25f);
		}
		else if(stance == "Defensive" && changed) {
			Dictionary<string, StatMods> defensive = new Dictionary<string, StatMods>();
			defensive.Add("STR", new StatMods(-2f, this));
			defensive.Add("AC", new StatMods(2f, this));
			user.addMods(defensive);
			}
	}
	
	public string getStance() {
		return stance;
	}
	
	public void setStance(string input) {
		stance = input;
	}
	

	
	
}
