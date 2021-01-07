using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hightlight : MonoBehaviour
{
	public Tilemap tilemap;
	private Camera cameraName; 
	private Vector3Int lastClick;
	private bool selected = false;
	private CombatManager manager;
	public GridManager gridManager;
	
	void Start() {
		cameraName = Camera.main;
		manager = GameObject.Find("Manager").GetComponent<CombatManager>();
	}
		
	void Update() {		
		
		if(Input.GetMouseButtonDown(0) && !selected && manager.getState() == State.movement) {
		Debug.Log("Mousing over");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3Int gridPos = tilemap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));
			//tilemap.SetTileFlags(gridPos, TileFlags.None);
			//tilemap.SetColor(gridPos, new Color(155,0,0));
			lastClick = gridPos;
			selected = true;
		}
		else if(manager.getState() != State.movement && selected) {
			//tilemap.SetColor(lastClick, new Color(255,255,255));
			selected = false; 
		}
	}
	
/*	private void OnMouseExit() {
		Debug.Log("Mouse Exit");
			tilemap.SetTileFlags(lastClick, TileFlags.None);
			tilemap.SetColor(lastClick, new Color(255,255,255));
	}
	*/
}


