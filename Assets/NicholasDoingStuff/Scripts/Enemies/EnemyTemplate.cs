using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.Linq;



public abstract class EnemyTemplate : MonoBehaviour{ 
public Dictionary<string, Stats> stats = new Dictionary<string, Stats>();
protected int HP;
protected int MP;
public List<String> vulnerabilities; 
public List<String> resistances; 
public List<String> immunities;  
public List<object> abilities; 
public int Id;
public int experience;
protected int currentHealth;
protected GameObject selectedTarget;

public List<GenericItem> drops;


//At individual enemy level, each enemy must be assigned int expValue

//protected abstract void templatingEnemyType();

public virtual void enemyAIPattern() {
		Debug.Log("Got inside of AI");
		System.Random rnd = new System.Random();
		int option = 1;
		switch(option) {
			case 1:
				Strike(selectedTarget);
				break;
			default: 
			Strike(selectedTarget);
			break;
		}
	
}

public void handleStatMods(Dictionary<string, StatMods> modifiers) {
	for(int i = 0; i < modifiers.Count; i++) {
		//Debug.Log(stats[modifiers.ElementAt(i).Key].Value);
		stats[modifiers.ElementAt(i).Key].AddModifier(modifiers.ElementAt(i).Value); 
		Debug.Log(stats[modifiers.ElementAt(i).Key].Value);
	}
}

public float calculateDamage(Dictionary<String, float> damage) {
	//Perform needed calculation logic to determine final damage done from spell, ability, or attack based resistances, immunities
    return 0; 
}

public Dictionary<string, Stats> getStats(){
	return stats; 
}

public abstract void TakeDamage(int damage);
public abstract void OnDeath();
	
//Handles what gear or materials will be dropped by enemies by their template type could also be moved to the base enemy level instead
public abstract object handleDrops();

public int getCurrentHealth() {
	return currentHealth;
}

	public GameObject getClosestPlayer() {
		GameObject target = null;
		GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
		float closest = 2000000f;
		foreach(GameObject temp in player) {
			if(closest > Vector3.Distance(gameObject.transform.position, temp.transform.position)) {
				if(getClosestSpace(temp.transform.position, 1) != new Vector3(0,0,0) && TeamManager.Instance.checkIfAlive(temp) || player.Length == 1) {
				closest = Vector3.Distance(gameObject.transform.position, temp.transform.position);
				target = temp;
				}
			}
		}
		if(target == null) {
			Debug.Log("No target found");
			target = gameObject;
		}
		Debug.Log("Closest space to target is: " +  getClosestSpace(target.transform.position, 1));
		return target; 
	}		

	 public Vector3 getClosestSpace(Vector3 targetPos, float range) {
		Vector3 finalPosition = new Vector3(0,0,0);
		float shortestDist = 100000000f;
		List<Vector3> spots = new List<Vector3>();
			for(int i = (int)range*-1; i <= (int)range; i++) {
				for(int j = (int)range*-1; j <= (int)range; j++) {
					Vector3 temp = new Vector3(targetPos.x + i, targetPos.y + j, 0);
					if(Vector3.Distance(targetPos, temp) <= range && Vector3.Distance(targetPos, temp) >= range-1) {
						spots.Add(temp);
					}						
				}
			}
			
			bool occupied = false; 
			foreach(Vector3 pos in spots) {
				Debug.Log("Spots found are" + pos);
				if(shortestDist > Vector3.Distance(gameObject.transform.position, pos)) {
					foreach(GameObject entity in GameObject.Find("Grid Manager").GetComponent<GridManager>().getEntities()) {
						if(pos == entity.transform.position && entity != gameObject) {
							Debug.Log("Another person is in the most optimal spot, finding another available spot "  + entity.name);
							occupied = true; 
							break;
						}
					}
					if(!occupied) {
					shortestDist = Vector3.Distance(gameObject.transform.position, pos);
					finalPosition = pos; 
					}
					occupied = false; 
				}
			}
			Debug.Log("Final Position is " + finalPosition);
			return finalPosition;
	 }
	 
	 	public void Strike(GameObject target) {
		System.Random rnd = new System.Random();
		float critChance = Mathf.Round((stats["LCK"].Value - 10f)/2) * 5;
		int damage = ((int)(stats["STR"].Value))/3+rnd.Next(1, 7);
		if(rnd.Next(1, 101) >= (int)(100 - critChance)) {
			Debug.Log("Enemy Critical Strike");
			damage += damage;
		}			
		bool hit = false;
		Debug.Log("Got to Strike for: " + gameObject.name);
		Debug.Log("Target tag is: " + target.tag);
		if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= 1f && target.tag != "Enemy") {
					target.GetComponent<Character>().TakeDamage(damage);
					Debug.Log("Enemy damage dealt: " + damage);
					target.GetComponent<HealthBar>().SetHealth(target.GetComponent<Character>().getCharacter().getCurrentHealth());
					hit = true;
		}
		if(!hit) {
			Debug.Log("Failed to hit player");
		}
		
	}
	 
	public void setTarget(GameObject target) {
		selectedTarget = target; 
	}

}
//Example of base enemy of Undead class
/*
Skeleton Fighter: 
Str: 15
Dex: 15
Con: 18
Int: 4
Wis: 5
End: 13
Spd: 10
Lck: 5

Vulnerabilities: Bludgeoning, Light
Resistances: Piercing, Dark, cold
Immunities: Poison

Exp value: 50xp 

Abilities:

Rattling Scream: All enemies within 2 tiles of you must make a Wisdom Save or be frightened of you. Cooldown 3 turns. 

Attack: Make a weapon attack with your shortsword. 

Shove: Make a target enemy make a Str. save if they fail move them two 2 tiles and they are knocked prone. 

Drops: Hardy Bones, Scrap Metal, Rusted Shortsword, Tattered Leather Armor 

*/