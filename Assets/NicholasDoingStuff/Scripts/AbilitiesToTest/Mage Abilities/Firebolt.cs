using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : Spell
{
	
	public Firebolt(Mage player) {
		spellName = "Firebolt";
		playerClass = player;
		damage = 15;
		accuracy = 70;
		cost = 20; 
		elementType = "fire";	
	}
	
	public override void castSpell(List<object> target) {
		System.Random rnd = new System.Random();
		int finalD = (int)(playerClass.user.getStats()[scalingStat].Value) + rnd.Next(5, damage+1);
		if(playerClass.elementalBoon != null && playerClass.elementalBoon == "fire") {
			finalD = (int)(finalD * (1.25f));
		}
		EnemyTemplate mook = (EnemyTemplate)target[0];
		mook.TakeDamage(finalD);
	}
	
}
