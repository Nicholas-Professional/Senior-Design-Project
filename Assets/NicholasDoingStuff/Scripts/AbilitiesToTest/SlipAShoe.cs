using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlipAShoe : Abilities
{
	/*public void resolve(Vector3 target, SortedList<int, GameObject> potentialTargets, GameObject character) {
		Dictionary<string, Stats> stats = character.GetComponent<Player>().getStats();
		System.Random rnd = new System.Random();
		foreach(GameObject entity in potentialTargets.Values) {
			if(entity.transform.position == target) {
				if(entity.tag == "Enemy") {
				entity.GetComponent<Grab>().template.TakeDamage((int)(stats[scalingStat].Value+baseAmount+rnd.Next(1,9)));
				entity.GetComponent<HealthBar>().SetHealth(entity.GetComponent<Grab>().template.getCurrentHealth());
				Debug.Log("Enemy was damaged");
				}
				else{
				entity.GetComponent<Player>().TakeDamage((int)(stats[scalingStat].Value+baseAmount+rnd.Next(1,9)));
				}
			}
			else {
				foreach(Vector3 effect in aoe) {
					if(entity.transform.position == target+effect) {
						if(entity.tag == "Enemy") {
						entity.GetComponent<Grab>().template.TakeDamage((int)(stats[scalingStat].Value+baseAmount+rnd.Next(1,9)));
						entity.GetComponent<HealthBar>().SetHealth(entity.GetComponent<Grab>().template.getCurrentHealth());
						Debug.Log("Enemy was damaged");
						}
						else{
						entity.GetComponent<Player>().TakeDamage((int)(stats[scalingStat].Value+baseAmount+rnd.Next(1,9)));
						}
					}
				}
			}
		}
		
	}
	*/
	
	public override void resolve(List<object> Targets) {
		
	}

}
