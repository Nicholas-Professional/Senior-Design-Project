using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour
{
    // Start is called before the first frame update
    public int Id;
    public int experience;
    public int currentHealth;
    public int maxHealth;

    public virtual void TakeDamage(int damage)
    {
        //TODO:
        //implement status effects and apply them here if needed

    }
    public void PerformAttack(){

    }
    public virtual void OnDeath()
    {
    }
}
