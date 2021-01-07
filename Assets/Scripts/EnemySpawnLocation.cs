using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{
    public Transform location;
    public GameObject enemy;

    public void SpawnEnemyAtLocation(){
        Debug.Log("Triggered Special Spawener");
        Instantiate(enemy,location.position,Quaternion.identity);
    }
}
