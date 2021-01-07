using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shove : Abilities
{
	
	public Shove(Classes playerClass) {
		this.playerClass = playerClass;
		abilityName = "Shove";
		//targetted = true; 
	}
	
	public override void resolve(List<object> targets) {
		EnemyTemplate enemy = (EnemyTemplate)targets[0];
		//Dictionary<string, Stats> enemyStats = enemy.getStats();
		//System.Random rnd = new System.Random();
		//if(playerClass.user.getStats()["STR"].Value + 10 >= enemyStats["STR"].Value + rnd.Next(1, 21)) {
			GameObject target = (GameObject)targets[1];
			GameObject user = (GameObject)targets[2];
			if(user.transform.position.x < target.transform.position.x) {
			target.transform.position = target.transform.position + new Vector3(1f,0,0);
			}
			else if(user.transform.position.x > target.transform.position.x){
			target.transform.position = target.transform.position + new Vector3(-1f,0,0);
			}
			else if(user.transform.position.y < target.transform.position.y) {
			target.transform.position = target.transform.position + new Vector3(0,1f,0);
			}
			else if(user.transform.position.y > target.transform.position.y){
			target.transform.position = target.transform.position + new Vector3(0,-1f,0);
			}
		//}
		
	}

}
