using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphoningStrike : Abilities
{
	
	public SiphoningStrike(Classes playerClass) {
		this.playerClass = playerClass;
		abilityName = "SiphoningStrike";
		targetted = true; 
	}
	
	
	public override void resolve(List<object> target) {
		EnemyTemplate mook = (EnemyTemplate)target[0];
		int damageH = mook.getCurrentHealth();
		int damageL = 0; 
		playerClass.Strike(mook);
		if(mook != null){
			damageL = mook.getCurrentHealth();			
		}
		if(playerClass.user.currentMana + (damageH - damageL)/2 >= playerClass.user.maxMana) {
			playerClass.user.currentMana = playerClass.user.maxMana;
		}
		else{
			playerClass.user.currentMana += (damageH-damageL)/2;
		}
	}
}
