using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scout : Classes
{
	bool Pass = false; 

	public Scout(Character player) {
		user = player;
		skills.Add(new Bleed(this));
		className = "Scout";
	}

    public override void updatePassives(List<object> updates)
    {
        if (user.getLevel() == 2 && !Pass)
            {
                Pass = true;
                Dictionary<string, StatMods> passive = new Dictionary<string, StatMods>();
                passive.Add("SPD", new StatMods(3f, this));
            }

    }
	
}
