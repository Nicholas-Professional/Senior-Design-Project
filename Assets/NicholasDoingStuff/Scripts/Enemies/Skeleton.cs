using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Skeleton : EnemyTemplate
{
	public Vector3[] aoe;
    Skeleton()
    {
		stats.Add("STR", new Stats(14));
		stats.Add("DEX", new Stats(10));
		stats.Add("VIT", new Stats(14));
		stats.Add("INT", new Stats(6));
		stats.Add("WIS", new Stats(8));
		stats.Add("SPD", new Stats(10));
		stats.Add("LCK", new Stats(5));
        HP = (int)(stats["VIT"].Value*3);
        MP = (int)(stats["WIS"].Value*3);
        vulnerabilities = new List<string>{"bludgeoning"};
        resistances = new List<string>{"piercing", "slashing"};
        immunities = new List<string>{"dark"};
        abilities = null;
		currentHealth=HP;
	}

	void Start(){
        this.Id = 2;
        this.experience=10;
    }
	
/*	public override void enemyAIPattern() {
		Debug.Log("Got inside of AI");
		System.Random rnd = new System.Random();
		int option = 1;
		switch(option) {
			case 1:
				Strike(selectedTarget);
				break;
			case 2:
			Scream();
			break;
			default: 
			Strike(selectedTarget);
			break;
		}
	} */

	public override object handleDrops() {
		//Dictionary<string, StatMods> mods = new Dictionary<string, StatMods>();
		//mods.Add("INT", new StatMods(-2f));
		//handleStatMods(mods);
		return null;
		
	}
	
	private void Scream() {
		Debug.Log("Got inside of Scream");
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		System.Random rnd = new System.Random();
		foreach(Vector3 area in aoe) {
			for(float i = aoe[0].x; i < aoe[aoe.Length-1].x+1; i++) {
				foreach(GameObject player in players) {
					//Debug.Log(new Vector3(area.x, i, 0) + transform.position);
					if(player.transform.position == new Vector3(area.x, i, 0) + transform.position) {
						player.GetComponent<Character>().TakeDamage(rnd.Next(1, 6));
						player.GetComponent<HealthBar>().SetHealth(player.GetComponent<Character>().getCharacter().getCurrentHealth());
						Debug.Log("Writhe in agony");
					}
				}
						
			}
		}
	}
	
	public override void TakeDamage(int damage) {
		currentHealth -= damage;
		if(currentHealth <= 0) {
			currentHealth = 0;
			OnDeath();
		}
		Debug.Log("Skeleton Current Health: " + currentHealth); 
	}
	 
	public override void OnDeath() {
		CombatEvents.EnemyDied(this);
        Destroy(this.gameObject);
	}

}
