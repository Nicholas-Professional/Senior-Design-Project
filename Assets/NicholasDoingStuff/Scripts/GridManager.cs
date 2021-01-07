using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
	public List<TileTypes> terrainTiles; 
	private Dictionary<string, TileTypes> terrain = new Dictionary<string, TileTypes>();
    public Vector3[,] spots;
    Astar astar;
    List<Spot> characterPath = new List<Spot>();
    new Camera camera;
    BoundsInt bounds;
	public float speed;
	private	int i;
	private CombatManager manager; 
	private GameObject[] enemies;
	private GameObject[] allies;
	public static bool finished = true;
	private List<GameObject> entities = new List<GameObject>(); 	
    // Start is called before the first frame update
    void Start()
    {
        finished=true;
        Debug.Log("Pre-CompressBounds"+tilemap.cellBounds);
        tilemap.CompressBounds();
		foreach(TileTypes type in terrainTiles) {
			terrain.Add(type.name, type);
		}
        Debug.Log("CompressBounds"+tilemap.cellBounds);
        bounds = tilemap.cellBounds;
        camera = Camera.main;
		manager = GameObject.Find("Manager").GetComponent<CombatManager>();
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		allies = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject enemy in enemies) {
			entities.Add(enemy);
		}
		foreach(GameObject ally in allies) {
			entities.Add(ally);
		}
        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }

    public void AddSpawnedEntites(GameObject input){
        entities.Add(input);
        Debug.Log("Count of Entites in grid manager after adding a spawn enemy: "+entities.Count);
    }
	
	public List<GameObject> getEntities() {
		return entities; 
	}
	
    public void CreateGrid()
    {
        spots = new Vector3[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                
                if (tilemap.HasTile(new Vector3Int(x, y, 0)) && tilemap.GetTile(new Vector3Int(x,y,0)) != null && !entityExistsHere(tilemap.GetCellCenterWorld(new Vector3Int(x,y,0))))
                {
					//Debug.Log("Tile is:" + tilemap.GetTile(new Vector3Int(x, y, 0)).name);
                    spots[i, j] = tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
					//Debug.Log("Succeeded:" + spots[i,j]);
                }        
                else
                {
					Vector3 position = tilemap.GetCellCenterWorld(new Vector3Int(x,y,1));
                    spots[i, j] = position + new Vector3(0,0,1);
					//Debug.Log("Failed:" + spots[i,j]);
                }
            }
        }
    }
	
	private bool entityExistsHere(Vector3 currentPosition) {
		bool exists = false;
		foreach(GameObject entity in entities) {
			if(entity!=null && entity.transform.position == currentPosition) {
				exists = true;
			}				
			
		}
		return exists;
	}
	
	
    // Update is called once per frame
    void Update()
    {
		
		if(EnemySpawner.spawnComplete) {
			EnemySpawner.spawnComplete = false; 
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject enemy in enemies) {
				entities.Add(enemy);
			}
		}
 /*       if (Input.GetMouseButton(1))
        {
            Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(world);
            start = new Vector2Int(gridPos.x, gridPos.y);
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(world);
            roadMap.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), null);
        }
		*/
		
		
		for(int j = 0; j < entities.Count; j++) {
			if(entities[j] == null) {
				entities.RemoveAt(j);
			}
		}
		
		CreateGrid();
		
		
		if(finished) {
            if (Input.GetMouseButtonDown(0) && manager.getState() == State.movement && CombatUI.canMoveAgain)
            {
               // Debug.Log("Got click");
                CombatUI.canMoveAgain = false;
                finished = false; 
                Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tilemap.WorldToCell(world);
                
            // if (roadPath != null && roadPath.Count > 0)
                //    roadPath.Clear();
                //Debug.Log(transform.position);
				int distance = (int)(manager.getTurnEntity().GetComponent<Character>().getStats()["SPD"].Value)/2;
                characterPath = astar.CreatePath(spots, manager.getTurnEntity().transform.position, tilemap.GetCellCenterWorld(gridPos), distance, terrain, tilemap, entities, manager.getTurnEntity().tag);
                if (characterPath == null) {
                    Debug.Log("Invalid path");
                    finished = true; 
                    return;
                }
                i = characterPath.Count-2; 
            }
			else if (manager.getTurnEntity()!=null && manager.getTurnEntity().tag == "Enemy" && manager.getState() == State.movement) {
				GameObject enemy = manager.getTurnEntity().GetComponent<Grab>().template.getClosestPlayer();
				if(enemy != manager.getTurnEntity()) {
				manager.getTurnEntity().GetComponent<Grab>().template.setTarget(enemy);
				}
				characterPath = astar.CreatePath(spots, manager.getTurnEntity().transform.position, manager.getTurnEntity().GetComponent<Grab>().template.getClosestSpace(enemy.transform.position, 1f), (int)(manager.getTurnEntity().GetComponent<Grab>().template.getStats()["SPD"].Value)/3, terrain, tilemap, entities, manager.getTurnEntity().tag);
				finished = false;
				if (characterPath == null) {
                    Debug.Log("Invalid path");
                    finished = true; 
                    return;
                }
				i = characterPath.Count-2;
	
			}
		}
		else{
		    /*Debug.Log("Entered here");
		    //Debug.Log(characterPath.Count-1);
            Debug.Log("Manager tranform"+manager.getTurnEntity().transform.position);
            Debug.Log("The value of i: "+i);
            Debug.Log("The Length of characterPath: "+ characterPath.Count);
            Debug.Log("The Character Path X: "+characterPath[i].X);
            Debug.Log("The Character Path Y: "+characterPath[i].Y);
            Debug.Log("New Vector Path for Grid Manager: "+new Vector3(characterPath[i].X, characterPath[i].Y, 0));
            */
			Vector3Int gridPos = tilemap.WorldToCell(new Vector3(characterPath[0].X, characterPath[0].Y, 0));
			tilemap.SetTileFlags(gridPos, TileFlags.None);
			tilemap.SetColor(gridPos, new Color(155,0,0));
			manager.getTurnEntity().transform.position = Vector3.MoveTowards(manager.getTurnEntity().transform.position, new Vector3(characterPath[i].X, characterPath[i].Y, 0), speed * Time.deltaTime);

            if(Vector3.Distance(manager.getTurnEntity().transform.position, new Vector3(characterPath[i].X, characterPath[i].Y, 0)) <= 0f) {
				i--;
			//	if(i == 1) {
					//foreach(GameObject entity in entities) {
					//	if(entity.transform.position == new Vector3(characterPath[1].X, characterPath[1].Y,0)) {
					//		finished = true;
					//		break;
					//	}				
					//}
				//}
				if(i < 0) {
					tilemap.SetColor(gridPos, new Color(255,255,255));
				    finished = true; 
			    }
			}
			
			//Debug.Log(new Vector3(characterPath[i].X, characterPath[i].Y, characterPath[i].Height));
			//Debug.Log(i);
			
        }
		
	}
}
    
