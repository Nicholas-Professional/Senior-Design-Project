using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    [SerializeField]
    GameObject player;
    PlayerInfo playerdata;

    public TeamManager manager;

    public void Awake(){
        DontDestroyOnLoad(this);
    }

    void OnEnable(){
        SceneManager.sceneLoaded += SceneChanged;
    }
    void OnDisable(){
        SceneManager.sceneLoaded -=SceneChanged;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance!=null && Instance !=this){
            Destroy(gameObject);
        }
        else{
            Instance=this;
        }
        manager=GameObject.Find("Team").GetComponent<TeamManager>();
    }

    void SceneChanged(Scene scene,LoadSceneMode mode){
        manager=GameObject.Find("Team").GetComponent<TeamManager>();
        if(scene.buildIndex!=0 && manager!=null){

            foreach(GameObject x in manager.teamCharacters){
                //set player health to full when leaving the arena
                Character mc = x.GetComponent<Character>();
                mc.currentHealth=mc.maxHealth;
                mc.currentMana=mc.maxMana;

                //change player color back to normal
                SpriteRenderer ren = player.GetComponent<SpriteRenderer>();
                Color col =new Color(255,255,255);
                ren.color=col;
            }

            manager.ReviveAll();
        }

    }

    public void GameOver(){
        var objects = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject b in objects){
                b.GetComponent<Character>().enabled=(true);
                Debug.Log("Player being activated");
                //Destroy(b);
            }
            
            SceneManager.LoadScene(1);
    }

    
}
