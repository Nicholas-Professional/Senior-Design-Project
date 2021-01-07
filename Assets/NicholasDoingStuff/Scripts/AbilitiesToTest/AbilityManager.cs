using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class AbilityManager : MonoBehaviour
{
	public static bool completed = false; 
	public static bool targeting = false;
	public CombatManager manager; 
	public GameObject shove;
	public GameObject stance;
	private GameObject selectedTarget;
	
	public void Start() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject player in players) {
			Debug.Log(player.name + "The name of the character: "+ player.GetComponent<Character>().characterName);
			if(player!=null && player.GetComponent<Character>().getClass().className == "Warrior") {
				Warrior temp = (Warrior)player.GetComponent<Character>().getClass();
				temp.setStance("");
			}
		}
	}
	
	
	public void Status() {
		Dictionary<string, Stats> stats = manager.getTurnEntity().GetComponent<Character>().getStats();
		foreach(KeyValuePair<string, Stats> stat in stats) {
			Stats current = stat.Value; 
			Debug.Log(stat.Key + ": " + current.Value);
		}
		completed = true; 
	}

	public void StrikeWarrior() {
            Character player = manager.getTurnEntity().GetComponent<Character>();
			Debug.Log("The Player's Max Health"+player.maxHealth);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			bool attackHappened=false;
            foreach(GameObject enemy in enemies) {
				if(Vector3.Distance(manager.getTurnEntity().transform.position, enemy.transform.position) <= 1f) {
					player.getClass().Strike(enemy.GetComponent<Grab>().template);
					enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
					attackHappened=true;
				}
            }
			if(attackHappened){
            	completed = true;
			}
	}

	public void Strike() {
		int range = 1;
            Character player = manager.getTurnEntity().GetComponent<Character>();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			bool attackHappened=false;
			if(selectedTarget != null) {
				//Debug.Log("Target found attempting to Strike");
				player.getClass().Strike(selectedTarget.GetComponent<Grab>().template);
				selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
				attackHappened=true;
				selectedTarget = null;
			}
            /*foreach(GameObject enemy in enemies) {
				if(Vector3.Distance(manager.getTurnEntity().transform.position, enemy.transform.position) <= 1f) {
					player.getClass().Strike(enemy.GetComponent<Grab>().template);
					enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
					attackHappened=true;
					break;
				}
            }
			*/
			if(attackHappened){
            	completed = true;
			}
			}));    
	}
	
	public void AttemptShove() {
		int range = 1; 
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		StartCoroutine(ApplicableTargets(range, player, enemies, () => {
		Dictionary<string, Abilities> playerAbilities = player.getClass().usableAbilities();
		if(playerAbilities.ContainsKey("Shove")) {
						List<object> requirements = new List<object>();
						requirements.Add(selectedTarget.GetComponent<Grab>().template);
						requirements.Add(selectedTarget);
						requirements.Add(manager.getTurnEntity());
						playerAbilities["Shove"].resolve(requirements);
						completed = true;
						selectedTarget = null;
					}
					else {
						Debug.Log("This ability is unusable right now");
					}
		}));
		//foreach(GameObject enemy in enemies) {
			//if(Targetting.lastPos == enemy.transform.position) {
				//if(Vector3.Distance(manager.getTurnEntity().transform.position, enemy.transform.position) <= 1f) {
					
	/*	else {
			Debug.Log("Shove failed");
			completed = true;
		}
		*/
	}
	
	public void ChangeStance() {
		shove.SetActive(false);
		stance.SetActive(false);
	}
	
	public void DefensiveStance() {
		Character player = manager.getTurnEntity().GetComponent<Character>();
		Dictionary<string, Abilities> playerAbilities = player.getClass().usableAbilities();
		if(playerAbilities.ContainsKey("ArtOfWar")) {
			List<object> requirements = new List<object>();
			requirements.Add("Defensive");
			playerAbilities["ArtOfWar"].resolve(requirements);
			completed = true;
			Debug.Log("Switched to defensive stance");
		}
		completed = true; 
	}
	
	public void OffensiveStance() {
			Character player = manager.getTurnEntity().GetComponent<Character>();
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			Dictionary<string, Abilities> playerAbilities = player.getClass().usableAbilities();
			foreach(GameObject enemy in enemies) {
				//if(Targetting.lastPos == enemy.transform.position) {
					if(Vector3.Distance(manager.getTurnEntity().transform.position, enemy.transform.position) <= 1f) {
						if(playerAbilities.ContainsKey("ArtOfWar")) {
							List<object> requirements = new List<object>();
							requirements.Add("Offensive");
							requirements.Add(enemy.GetComponent<Grab>().template);
							playerAbilities["ArtOfWar"].resolve(requirements);
							enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
							break; 
						}
						if(completed) {
							break; 
						}
					}
				//}
			}
			
		if(completed) {
			Debug.Log("Switched to offensive stance");
		}
		else{
			Debug.Log("Failed to switch stance");
			completed = true;
		}
	}


	public void FireSpell() {
		//StartCoroutine(Firebolt());
		int range = 5; 
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			//Debug.Log("Atttempting to cast firebolt");
							if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("Firebolt")) {
							List<object> requirements = new List<object>(); 
							requirements.Add(selectedTarget.GetComponent<Grab>().template);
							(player.getClass() as Mage)?.spellList["Firebolt"].castSpell(requirements);
							selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
						}
		}));
	}
	
		public void WaterSpell() {
		//StartCoroutine(IceSpike());
		int range = 10; 
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			//Debug.Log("Atttempting to cast firebolt");
							if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("IceSpike")) {
							List<object> requirements = new List<object>(); 
							requirements.Add(selectedTarget.GetComponent<Grab>().template);
							(player.getClass() as Mage)?.spellList["IceSpike"].castSpell(requirements);
							selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
						}
		}));
	}
	
		public void EarthSpell() {
		StartCoroutine(RockBlast());
		int range = 7; 
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			//Debug.Log("Atttempting to cast firebolt");
							if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("RockBlast")) {
							List<object> requirements = new List<object>(); 
							requirements.Add(selectedTarget.GetComponent<Grab>().template);
							(player.getClass() as Mage)?.spellList["RockBlast"].castSpell(requirements);
							selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
						}
		}));
		}

	public void SiphoningStrike() {
		Debug.Log("Got inside of the AbilityManager");
		int range = 1; 
			Character player = manager.getTurnEntity().GetComponent<Character>();
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			Dictionary<string, Abilities> playerAbilities = player.getClass().usableAbilities();
			Debug.Log(player.getClass());
							if(playerAbilities.ContainsKey("SiphoningStrike")) {
							Debug.Log("Got inside of siphoning strike");
							List<object> requirements = new List<object>();
							requirements.Add(selectedTarget.GetComponent<Grab>().template);
							playerAbilities["SiphoningStrike"].resolve(requirements);
							selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
							
						}
			}));
	}


	public void PassTurn() {
		completed = true;
	}


	public void Bleed() {
		Debug.Log("Got inside of the AbilityManager");
			int range = 1;
			Character player = manager.getTurnEntity().GetComponent<Character>();
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			StartCoroutine(ApplicableTargets(range, player, enemies, () => {
			Dictionary<string, Abilities> playerAbilities = player.getClass().usableAbilities();
					if(playerAbilities.ContainsKey("Bleed")) {
					Debug.Log("Got inside of bleed");
					List<object> requirements = new List<object>();
					requirements.Add(selectedTarget.GetComponent<Grab>().template);
					requirements.Add(manager);
					playerAbilities["Bleed"].resolve(requirements);
					selectedTarget.GetComponent<HealthBar>().SetHealth(selectedTarget.GetComponent<Grab>().template.getCurrentHealth());
					completed = true;  
					}
			}));
	}


	IEnumerator RockBlast() {
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		AbilityManager.targeting = true;
		while(!Targetting.confirmed) {
			yield return null; 
		}
		AbilityManager.targeting = false;
		Targetting.confirmed = false; 
				foreach(GameObject enemy in enemies) {
					if(enemy.transform.position == Targetting.lastPos) {
						if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("RockBlast")) {
							List<object> requirements = new List<object>(); 
							requirements.Add(enemy.GetComponent<Grab>().template);
							(player.getClass() as Mage)?.spellList["RockBlast"].castSpell(requirements);
							enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
							break;
						}
					}
				}
		completed = true;
	}
	
	IEnumerator Firebolt() {
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		AbilityManager.targeting = true;
		while(!Targetting.confirmed) {
			yield return null; 
		}
		AbilityManager.targeting = false;
		Targetting.confirmed = false; 
	foreach(GameObject enemy in enemies) {
				if(enemy.transform.position == Targetting.lastPos) {
					if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("Firebolt")) {
						List<object> requirements = new List<object>(); 
						requirements.Add(enemy.GetComponent<Grab>().template);
						(player.getClass() as Mage)?.spellList["Firebolt"].castSpell(requirements);
						enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
						completed = true; 
						break;
					}
					
				}
				
			}
	}
	
	IEnumerator IceSpike() {
		Character player = manager.getTurnEntity().GetComponent<Character>();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		AbilityManager.targeting = true;
		while(!Targetting.confirmed) {
			yield return null; 
		}
		AbilityManager.targeting = false;
		Targetting.confirmed = false; 
				foreach(GameObject enemy in enemies) {
					if(enemy.transform.position == Targetting.lastPos) {
						if((bool)(player.getClass() as Mage)?.spellList.ContainsKey("IceSpike")) {
							List<object> requirements = new List<object>(); 
							requirements.Add(enemy.GetComponent<Grab>().template);
							(player.getClass() as Mage)?.spellList["IceSpike"].castSpell(requirements);
							enemy.GetComponent<HealthBar>().SetHealth(enemy.GetComponent<Grab>().template.getCurrentHealth());
							completed = true; 
							break;
						}
					}
				}
		completed = true;
	}
	
	IEnumerator ApplicableTargets(int range,Character player, GameObject[] enemies, Action ActionToPerform) {
		Tilemap terrain = GameObject.Find("Terrain").GetComponent<Tilemap>();
		List<Vector3Int> targets = new List<Vector3Int>();
		Dictionary<Vector3, GameObject> enemiesInRange = new Dictionary<Vector3, GameObject>();
		foreach(GameObject enemy in enemies) {
			if(Vector3.Distance(player.gameObject.transform.position, enemy.transform.position) <= range) {
				Vector3Int gridPos = terrain.WorldToCell(enemy.transform.position);
				terrain.SetTileFlags(gridPos, TileFlags.None);
				terrain.SetColor(gridPos, new Color(155,0,0));
				targets.Add(gridPos);
				enemiesInRange.Add(enemy.transform.position, enemy);
			}
		}
		
		if(enemies.Length != 0 && enemiesInRange.Count != 0) {
		targeting = true;
		bool select = false;
			while(!select && !Targetting.canceled) {
				if(Targetting.confirmed) {
					if(enemiesInRange.ContainsKey(new Vector3(Targetting.lastPos.x, Targetting.lastPos.y, 0))) {
						//Debug.Log("Enemy Selected in range");
						selectedTarget = enemiesInRange[new Vector3(Targetting.lastPos.x, Targetting.lastPos.y,0)];
						select = true;
						continue;
					}
					Targetting.confirmed = false;
				}
				yield return null;
			}
			foreach(Vector3Int target in targets) {
				terrain.SetColor(target, Color.white);
			}
			targeting = false;
			if(!Targetting.canceled) {
			ActionToPerform();
			}
			else {
				Targetting.canceled = false;
			}
		}
		yield return null;
	}
}

