using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Abilities
{
	
	public Bleed(Classes playerClass) {
		abilityName = "Bleed";
		this.playerClass = playerClass;
	}
	
    public override void resolve(List<object> updates)
    {
		int damage = (int)(updates[0] as EnemyTemplate)?.getCurrentHealth();
        playerClass.Strike((EnemyTemplate)updates[0], 1.5f);
		int finalD = (int)(updates[0] as EnemyTemplate)?.getCurrentHealth();
		int totalD = damage - finalD;
		(updates[1] as CombatManager)?.StartCoroutine(Bleeding((EnemyTemplate)updates[0], 1, totalD, (CombatManager)updates[1]));
    }
	
	private IEnumerator Bleeding(EnemyTemplate target, int duration, int damage, CombatManager manager) {
		Debug.Log("Target is Bleeding");
		int i = 0;
		while(i < duration && target != null) {
			Debug.Log("Current Duration is: " + i);
			Debug.Log("Current turn Entity is: " + manager.getTurnEntity().name);
			if(target == null) {
				i++;
				continue;
			}
			if(manager.getState() == State.turnStart && manager.getTurnEntity() != null && manager.getTurnEntity() == target.gameObject) {
				Debug.Log("Damage is dealt: " + damage/2 + " out of " + manager.getTurnEntity().GetComponent<Grab>().template.getCurrentHealth());
				if(damage/2 > target.getCurrentHealth()) {
					damage = (target.getCurrentHealth())-1;
					target.TakeDamage(damage);
				}
				else{
				target.TakeDamage(damage/2);
				}
				i++;
				if(manager.getTurnEntity() != null || target != null) {
					target.gameObject.GetComponent<HealthBar>().SetHealth(target.getCurrentHealth());
				}
			}
			yield return null; 
			}
			
			
		}
		
	}
