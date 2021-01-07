using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlast : Spell
{
	
	
	public RockBlast(Mage player) {
		spellName = "RockBlast";
		damage = 12;
		accuracy = 80;
		cost = 15; 
		elementType = "earth";
		playerClass = player;
	}
    
	public override void castSpell(List<object> target) {
		System.Random rnd = new System.Random();
		int finalD = (int)(playerClass.user.getStats()[scalingStat].Value) + rnd.Next(3, damage+1);
		if(playerClass.elementalBoon != null && playerClass.elementalBoon == "earth") {
			finalD = (int)(finalD * (1.25f));
		}
		EnemyTemplate mook = (EnemyTemplate)target[0];
		mook.TakeDamage(finalD);
	}
	
}
