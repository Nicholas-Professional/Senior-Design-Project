using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Classes
{
	public Character user;
	[SerializeField]
	public string className; 
	protected List<Abilities> skills = new List<Abilities>(); 
	
	public Dictionary<string, Abilities> usableAbilities() {
		Dictionary<string, Abilities> availSkills = new Dictionary<string, Abilities>();
		foreach(Abilities skill in skills) {
			if(user.getLevel() >= skills.IndexOf(skill)+1 && skill != null) {
				Debug.Log("Usable skill: " + skill.abilityName);
				availSkills.Add(skill.abilityName, skill);
			}
		}
		if(availSkills.Count > 0) {
			return availSkills;
		}
		else {
			return null; 
		}
		
	}
	
	public abstract void updatePassives(List<object> updates);
	
	//public void resolveSelectedAbility(GameObject target)
	
	//Calculates melee damage, needs to later use the range determined by the weapon and give its damage type over to TakeDamage for the Enemy
	public void Strike(EnemyTemplate target, float modifier = 1f) {
		Dictionary<string, Stats> playerStats = user.getStats();
		System.Random rnd = new System.Random();
		float critChance = Mathf.Round((user.getStats()["LCK"].Value - 10f)/2) * 5;
		int damage = (int)((playerStats["STR"].Value+rnd.Next(1,11))*modifier);
		if(rnd.Next(1,101) >= (int)(100 - critChance)) {
			damage += damage;
			Debug.Log("Player Critical Strike");
		}
		Debug.Log(damage);
		target.TakeDamage(damage);
	}
	

}


