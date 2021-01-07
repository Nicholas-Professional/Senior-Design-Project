using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalControl : MonoBehaviour
{
    public int SceneIndexToMoveTo;
    public bool inCombatScene;

    public GameObject button;

    void Start(){
        if(!inCombatScene){
            button.SetActive(true);
        }
    }
    void Update(){
        if(inCombatScene){
            //check if all enemies dead
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemy.Length == 0){
                button.SetActive(true);
            }
            else{
                button.SetActive(false);
            }
        }
    }

    public void MoveToScene(){
        SceneManager.LoadScene(SceneIndexToMoveTo);
    }
}
