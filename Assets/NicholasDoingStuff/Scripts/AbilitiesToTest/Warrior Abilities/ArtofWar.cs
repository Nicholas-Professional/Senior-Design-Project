using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtofWar : Abilities 
{
	
	public ArtofWar(Classes playerClass) {
		abilityName = "ArtOfWar";
		this.playerClass=playerClass;
	}
	
	public override void resolve(List<object> stance) {
		Debug.Log(playerClass);
		playerClass.updatePassives(stance);
	}

}
