using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should only trigger at start of combat scene
public class EnemySpawner : MonoBehaviour
{
    public GridManager gridManager;
    public UIQuest specialLevelQuest;
    public int numberOfRandomEnemies;
    public List<GameObject> randomEnemies;
	public static bool spawnComplete = false;
    
    public List<EnemySpawnLocation> specialSpawns;

    
    // Start is called before the first frame update
    void Start()
    {
        Vector3[,] spots=new Vector3[gridManager.spots.GetLength(0),gridManager.spots.GetLength(1)];
        for(int x =0; x<gridManager.spots.GetLength(0);x++){
            for(int y =0; y <gridManager.spots.GetLength(1);y++){
                if (gridManager.spots[x, y].z != 1)
                {
                    spots[x, y] = gridManager.spots[x, y];
                }
            }
        }
        Player player=GameObject.Find("Player").GetComponent<Player>();
        bool specialQuestFound=false;
        foreach(Quest x in player.quests){
            if(x.QuestID==specialLevelQuest.questInfo.QuestID){
                specialQuestFound=true;
                break;
            }
        }
        //if the level's special quest is active then activate the special enemy spawns
        if(specialQuestFound){
            foreach(EnemySpawnLocation x in specialSpawns){
                x.SpawnEnemyAtLocation();
            }
        }
        //else use spot and a random number generator that will be used to determine if
        else{
        
            int numOfEnemies=Random.Range(1,numberOfRandomEnemies+1);
            Debug.Log("Trying to spawn: " + numberOfRandomEnemies);
            GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
            GameObject[] enemies=GameObject.FindGameObjectsWithTag("Enemy");
            for(int i=0;i<numOfEnemies;i++){
                Debug.Log("Trying to spawn: " + i);
                
                int colLimit=spots.GetLength(1);
                Debug.Log("ColLimit: " + colLimit);
                int rowLimit=spots.Length/colLimit;
                Debug.Log("RowLimit: " + rowLimit);
                int maxSpaces=rowLimit*colLimit;
                bool locationFound=false;
                //should shuffle the spots
                Shuffle<Vector3>(spots);
                Vector3 spawnLoc=new Vector3(0,0,0);
                for(int rows=0; rows<rowLimit; rows++){
                    for(int cols=0;cols<colLimit;cols++){
                        //checks each player if they are at the spot
                        if(spots[rows,cols].z==1 || spots[rows,cols] == new Vector3(0f,0f,0f)){
                            locationFound=false;
                            break;
                        }
                        foreach(GameObject play in players){
                            if(play.transform.position!=spots[rows,cols]){
                                locationFound=true;
                            }
                            else{
                                locationFound=false;
                            }
                        }
                        //checks each enemy if they are at the spot
                        foreach(GameObject play in enemies){
                            if(play.transform.position!=spots[rows,cols]){
                                locationFound=true;
                            }
                            else{
                                locationFound=false;
                            }
                        }
                        if(locationFound){
							Debug.Log("Spot selected " + spots[rows,cols]);
                            spawnLoc=spots[rows,cols];
                            break;
                        }
                    }
                    if(locationFound){
                        break;
                    }
                }
                if(locationFound){
                    Debug.Log("Spawning Random enemies");
                    GameObject newEnemy = Instantiate(randomEnemies[Random.Range(0,randomEnemies.Count)],spawnLoc,Quaternion.identity);
                    gridManager.AddSpawnedEntites(newEnemy);
                    enemies=GameObject.FindGameObjectsWithTag("Enemy");
                    locationFound=false;
                }
            }
        }
		spawnComplete = true;
    }

    public static void Shuffle<T>( T[,] array){
        int lengthRow = array.GetLength(1);
        for(int i = array.Length-1;i>0;i--){
            int i0 = i/lengthRow;
            int i1= i % lengthRow;
            int j=Random.Range(0,i+1);
            int j0=j/lengthRow;
            int j1=j%lengthRow;

            T temp = array[i0,i1];
            array[i0,i1]=array[j0,j1];
            array[j0,j1]=temp;
        }
    }
}
