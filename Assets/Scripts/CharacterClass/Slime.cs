using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the first enemy its id will be 0
public class Slime : EnemyTemplate
{
    // Start is called before the first frame update
    void Start(){
        this.Id = 0;
        this.experience=2;
        currentHealth=HP;
    }
    public Slime(){
        Id=0;
    }
    public override void TakeDamage(int damage)
    {
        currentHealth-= damage;
        if(currentHealth<=0){
            OnDeath();
        }

    }
    public override void OnDeath()
    {
        CombatEvents.EnemyDied(this);
        Destroy(this.gameObject);
    }
    public override void enemyAIPattern() {
		
	}
    public override object handleDrops() {
		return null;
	}
}
