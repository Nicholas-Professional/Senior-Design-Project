using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Mage : Classes
{

    public Dictionary<string, Spell> spellList = new Dictionary<string, Spell>();
    public string elementalBoon;

    public Mage(Character player, string elementalBoon) {
        user = player;
        skills.Add(new SiphoningStrike(this));
        this.elementalBoon = elementalBoon; 
        className = "Mage";
        spellList.Add("Firebolt", new Firebolt(this));
        spellList.Add("IceSpike", new IceSpike(this));
        spellList.Add("RockBlast", new RockBlast(this));
    }

    public void setBoon(string Boon) {
        elementalBoon = Boon;
    }

    public override void updatePassives(List<object> updates) {

    }

}