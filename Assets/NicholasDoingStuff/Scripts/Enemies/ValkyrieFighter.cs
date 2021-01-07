using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValkyrieFighter : EnemyTemplate
{
    public Vector3[] aoe;
    public int[] ranges = new int[2];
    // Start is called before the first frame update

    ValkyrieFighter(){
        stats.Add("STR", new Stats(14));
		stats.Add("DEX", new Stats(15));
		stats.Add("VIT", new Stats(20));
		stats.Add("INT", new Stats(8));
		stats.Add("WIS", new Stats(5));
		stats.Add("SPD", new Stats(15));
		stats.Add("LCK", new Stats(7));
        HP = (int)(stats["VIT"].Value*5.5);
        MP = (int)(stats["WIS"].Value*3);
        currentHealth=HP;
    }
    void Start()
    {
        this.Id=16;
        this.experience=100;
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
		Debug.Log("Valkyrie Fighter Current Health: " + currentHealth); 
	}

    public override void OnDeath() {
		CombatEvents.EnemyDied(this);
        Destroy(this.gameObject);
	}

}
