using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Spell
{
	public IceSpike(Mage player) {
		spellName = "IceSpike";
		playerClass = player;
		damage = 8;
		accuracy = 90;
		cost = 10; 
		elementType = "water";	
	}
	
	public override void castSpell(List<object> target) {
		System.Random rnd = new System.Random();
		int finalD = (int)(playerClass.user.getStats()[scalingStat].Value) + rnd.Next(1, damage+1);
		if(playerClass.elementalBoon != null && playerClass.elementalBoon == "water") {
			finalD = (int)(finalD * (1.25f));
		}
		EnemyTemplate mook = (EnemyTemplate)target[0];
		mook.TakeDamage(finalD);
	}

}
