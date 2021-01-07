using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraugrMage : EnemyTemplate
{
    public Vector3[] aoe;
    public int[] ranges = new int[2];
    // Start is called before the first frame update

    DraugrMage(){
        stats.Add("STR", new Stats(14));
		stats.Add("DEX", new Stats(15));
		stats.Add("VIT", new Stats(14));
		stats.Add("INT", new Stats(8));
		stats.Add("WIS", new Stats(12));
		stats.Add("SPD", new Stats(15));
		stats.Add("LCK", new Stats(7));
        HP = (int)(stats["VIT"].Value*4.4);
        MP = (int)(stats["WIS"].Value*4.2);
        currentHealth=HP;
    }
    void Start()
    {
        this.Id=14;
        this.experience=85;
    }

    public override object handleDrops()
    {
        return null;
    }

    public override void TakeDamage(int damage) {
		currentHealth -= damage;
		if(currentHealth <= 0) {
			currentHealth = 0;
			OnDeath();
		}
		Debug.Log("Draugr Mage Current Health: " + currentHealth); 
	}

    public override void OnDeath() {
		CombatEvents.EnemyDied(this);
        Destroy(this.gameObject);
	}

}
