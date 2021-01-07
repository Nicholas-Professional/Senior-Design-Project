using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void EnemyEventHandler(EnemyTemplate enemy);
    public static event EnemyEventHandler OnEnemyDeath;
    public static void EnemyDied(EnemyTemplate enemy){
        if(OnEnemyDeath != null)
            OnEnemyDeath(enemy);
    }
}
