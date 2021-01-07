using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Targetting : MonoBehaviour
{

	public Tilemap tilemap;
	private Camera cameraName; 
	private Vector3Int lastClick;
	public static Vector3 lastPos; 
	private bool selected = false;
	public static bool confirmed = false; 
	public static bool canceled = false;
	private CombatManager manager;
	public List<Vector3Int> aoe = new List<Vector3Int>(); 
	
	void Start() {
		cameraName = Camera.main;
		manager = GameObject.Find("Manager").GetComponent<CombatManager>();
	}

	void Update() {
		if(Input.GetMouseButtonDown(0) && !selected && manager.getState() == State.actions && AbilityManager.targeting) {
		Debug.Log("Mousing over");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));
			//tilemap.SetTileFlags(gridPos, TileFlags.None);
			//tilemap.SetColor(gridPos, new Color(155,0,0));
			//if(aoe != null) {
			//	foreach(Vector3Int targets in aoe) {	
			//	tilemap.SetTileFlags(gridPos + targets, TileFlags.None);
			//	tilemap.SetColor(gridPos + targets, new Color(155,0,0));
			//	}
			//}
			lastPos = tilemap.GetCellCenterWorld(gridPos);
			//lastClick = gridPos;
			selected = true;
			confirmed = true;
		}
		else if (manager.getState() == State.actions && Input.GetKeyDown(KeyCode.Space) && AbilityManager.targeting) {
			selected = false;
			canceled = true;
		}
		else if(manager.getState() != State.actions && selected) {
			selected = false; 
			confirmed = false; 
		}
		/*
		else if(manager.getState() == State.actions && selected && AbilityManager.targeting && Input.GetKeyDown(KeyCode.Space)) {
			selected = false;
			tilemap.SetColor(lastClick, new Color(255,255,255));
			foreach(Vector3Int targets in aoe) {
			tilemap.SetColor(lastClick + targets, new Color(255,255,255));
			}
		}
		if(selected && AbilityManager.targeting && manager.getState() == State.actions && Input.GetKeyDown(KeyCode.Return)) {
			confirmed = true; 
			
		}
	}
	*/
	}
}
