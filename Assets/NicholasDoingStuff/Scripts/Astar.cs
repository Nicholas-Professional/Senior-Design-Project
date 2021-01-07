using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Astar
{
    public Spot[,] Spots;
	
    public Astar(Vector3[,] grid, int columns, int rows)
    {
        Spots = new Spot[columns, rows];
    }
    private bool IsValidPath(Vector3[,] grid, Spot start, Spot end)
    {
        if (end == null)
            return false;
        if (start == null)
            return false;
        if (end.Height >= 1)
            return false;
        return true;
    }
    public List<Spot> CreatePath(Vector3[,] grid, Vector3 start, Vector3 end, int length, Dictionary<string, TileTypes> terrainTiles, Tilemap tilemap, List<GameObject> entities, string currentType)
    {
		foreach(GameObject entity in entities) {
				if(entity.transform.position == end){
				//Debug.Log("Attempted to path onto another person");
					return null;
				}
		}
		
		List<GameObject> opposingFaction = new List<GameObject>();
		
		foreach(GameObject entity in entities) {
			if(entity.tag != currentType){
				opposingFaction.Add(entity);
			}
		}
		
        Spot End = null;
        Spot Start = null;
        var columns = Spots.GetUpperBound(0) + 1;
        var rows = Spots.GetUpperBound(1) + 1;
        Spots = new Spot[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j] = new Spot(grid[i, j].x, grid[i, j].y, grid[i, j].z);
            }
        }

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i, j].AddNeighboors(Spots, i, j);
                if (Spots[i, j].X == start.x && Spots[i, j].Y == start.y)
                    Start = Spots[i, j];
                else if (Spots[i, j].X == end.x && Spots[i, j].Y == end.y) {
					//if(end.z == 1) {
						//return null;
					//}
                    End = Spots[i, j];
				}
            }
        }
		
	/*	for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Spots[i,j].G = Distance(Start, Spots[i,j]);
            }
        }
		*/
        if (!IsValidPath(grid, Start, End))
            return null;
        List<Spot> OpenSet = new List<Spot>();
        List<Spot> ClosedSet = new List<Spot>();

        OpenSet.Add(Start);

        while (OpenSet.Count > 0)
        {
            //Find shortest step distance in the direction of your goal within the open set
            int winner = 0;
            for (int i = 0; i < OpenSet.Count; i++)
                if (OpenSet[i].F < OpenSet[winner].F) {
                    winner = i;
					//Debug.Log("I.F < winner.F");
				}
                else if (OpenSet[i].F == OpenSet[winner].F)//tie breaking for faster routing
                    if (OpenSet[i].H < OpenSet[winner].H) {
                        winner = i;
						//Debug.Log("I.H < winner.H");
					}

            var current = OpenSet[winner];

            //Found the path, creates and returns the path
            if (End != null && OpenSet[winner] == End)
            {
                List<Spot> Path = new List<Spot>();
                var temp = current;
                Path.Add(temp);
                while (temp.previous != null)
                {
                    Path.Add(temp.previous);
                    temp = temp.previous;
                }
                if (length - (Path.Count - 1) < 0)
                {
                    Path.RemoveRange(0, (Path.Count - 1) - length);
                }
                return Path;
            }

            OpenSet.Remove(current);
            ClosedSet.Add(current);


            //Finds the next closest step on the grid
            var neighboors = current.Neighboors;
            for (int i = 0; i < neighboors.Count; i++)//look through our current spots neighboors (current spot is the shortest F distance in openSet
            {
                var n = neighboors[i];
                if (!ClosedSet.Contains(n) && n.Height < 1)//Checks to make sure the neighboor of our current tile is not within closed set, and has a height of less than 1
                {
                    var tempG = current.G + 1;//gets a temp comparison integer for seeing if a route is shorter than our current path

                    bool newPath = false;
                    if (OpenSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                    {
                        if (tempG < n.G)//The distance to the end goal from this neighboor is shorter so we need a new path
                        {
							//Debug.Log("temp.G < n.G" + tempG);
							//Debug.Log("temp.G < n.G" + n.G);
                            n.G = tempG;
                            newPath = true;
                        }
                    }
                    else//if its not in openSet or closed set, then it IS a new path and we should add it too openset
                    {
						//Debug.Log("Add to open" + tempG);
						//Debug.Log("Add to open" + n.G);
                        n.G = tempG;
                        newPath = true;
                        OpenSet.Add(n);
                    }
                    if (newPath)//if it is a newPath calculate the H and F and set current to the neighboors previous
                    {
                        n.H = Heuristic(n, End, tilemap, terrainTiles, opposingFaction);
                        n.F = n.G + n.H;
						//Debug.Log("n.F: " + n.F);
                        n.previous = current;
                    }
                }
            }

        }
        return null;
    }

    private int Heuristic(Spot a, Spot b, Tilemap tilemap, Dictionary<string, TileTypes> terrainTiles, List<GameObject> opposingFaction)
    {
        //Debug.Log("Spot A:"+a);
        //Debug.Log("Spot B"+b);
        //Debug.Log("Tilemap"+tilemap);
        BoundsInt bounds2=tilemap.cellBounds;
        TileBase[] allTiles=tilemap.GetTilesBlock(bounds2);
        /*for(int x=0;x<bounds2.size.x;x++){
            for (int y = 0; y < bounds2.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds2.size.x];
                Debug.Log("Tile Coor:");
                if (tile != null) {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                } else {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }*/
       // Debug.Log("TerrainTiles"+terrainTiles);
        //foreach(TileTypes tiles in terrainTiles){
          //  Debug.Log("The tiles exist?: "+tiles);
        //}
        Debug.Log("Opposing Faction"+opposingFaction);
        //manhattan
		//calculate total x cost
        var dx = (int)Math.Abs(Mathf.Floor(a.X) - Mathf.Floor(b.X));
		int xCost = 0;
		if(a.X < b.X) {
			for(int i = 0; i < dx; i++) {
				int cost = 0;
                TileBase theNew=tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(a.X + i, a.Y, a.Height)));
				//foreach(TileTypes tiles in terrainTiles) {
                    /*(Debug.Log("tile Name "+tiles.name);
                    Debug.Log("Vector initial: "+new Vector3(a.X, a.Y + i, a.Height));
                    Debug.Log("Vector to floor"+ Vector3Int.FloorToInt(new Vector3(a.X, a.Y + i, a.Height)));
                    Debug.Log("A height "+a);
                    Debug.Log("Floor to int: "+theNew);
                    if(theNew==null){
                        continue;
                    }
                    Debug.Log("The Tile :"+theNew);
					*/
					//if(String.Equals(tiles.name, theNew.ToString())) {
			if(theNew != null) {		  
				if(terrainTiles.ContainsKey(theNew.ToString())) {
						cost = terrainTiles[theNew.ToString()].cost;
						//break;
					}
			}
			else {
				cost = 1;
			}
				//}
				foreach(GameObject entity in opposingFaction) {
					if(entity.transform.position == new Vector3(a.X + i, a.Y, a.Height)){
						cost = 1000;
					}
				}
				xCost += cost;
			}				
		}
		else {
			for(int i = 0; i < dx; i++) {
				int cost = 0;
                Debug.Log(terrainTiles);
                TileBase theNew=tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(a.X + i, a.Y, a.Height)));
				//foreach(TileTypes tiles in terrainTiles) {
                    
                 //   Debug.Log("tile Name \""+tiles.name+"\"");
                  //  Debug.Log("Vector initial: "+new Vector3(a.X, a.Y + i, a.Height));
                  //  Debug.Log("Vector to floor"+ Vector3Int.FloorToInt(new Vector3(a.X, a.Y + i, a.Height)));
                  //  Debug.Log("A height "+a.Height);
                  //  Debug.Log("Floor to int: |"+theNew+"|");
                    
                   // if(theNew==null){
				   //      continue;
                   // }
                  //  Debug.Log("The Tile :"+theNew);
				  //if(String.Equals(tiles.name, theNew.ToString())) {
			if(theNew != null) {		  
				if(terrainTiles.ContainsKey(theNew.ToString())) {
						cost = terrainTiles[theNew.ToString()].cost;
						//break;
					}
			}
			else {
				cost = 1;
			}
				//}
				foreach(GameObject entity in opposingFaction) {
					if(entity.transform.position == new Vector3(a.X - i, a.Y, a.Height)){
						cost = 1000;
				}
				}
				xCost += cost;
			}
		}
		//Calculate total y cost
        var dy = (int)Math.Abs(Mathf.Floor(a.Y) - Mathf.Floor(b.Y));
		int yCost = 0;
		if(a.Y < b.Y) {
			for(int i = 0; i < dy; i++) {
				int cost = 0;
                TileBase theNew=tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(a.X + i, a.Y, a.Height)));
				//foreach(TileTypes tiles in terrainTiles) {
                 //   Debug.Log("tile Name "+tiles.name);
                  //  Debug.Log("Vector initial: "+new Vector3(a.X, a.Y + i, a.Height));
                  //  Debug.Log("Vector to floor"+ Vector3Int.FloorToInt(new Vector3(a.X, a.Y + i, a.Height)));
                 //   Debug.Log("A height "+a.Height);
                 //   Debug.Log("Floor to int: "+theNew);

                  //  if(theNew==null){
                   //     continue;
                   // }
                  //  Debug.Log("The Tile :"+theNew);
				//	if(String.Equals(tiles.name, theNew.ToString())) {
			if(theNew != null) {		  
				if(terrainTiles.ContainsKey(theNew.ToString())) {
						cost = terrainTiles[theNew.ToString()].cost;
						//break;
					}
			}
			else {
				cost = 1;
			}
				//}
				foreach(GameObject entity in opposingFaction) {
					if(entity.transform.position == new Vector3(a.X, a.Y + i, a.Height)){
						cost = 1000;
				}
				}
				yCost += cost;
			}				
		}
		else {
			for(int i = 0; i < dy; i++) {
				int cost = 0;
                TileBase theNew=tilemap.GetTile(Vector3Int.FloorToInt(new Vector3(a.X + i, a.Y, a.Height)));

				//foreach(TileTypes tiles in terrainTiles) {
                //    Debug.Log("tile Name "+tiles.name);
                 //   Debug.Log("Vector initial: "+new Vector3(a.X, a.Y + i, a.Height));
                //    Debug.Log("Vector to floor"+ Vector3Int.FloorToInt(new Vector3(a.X, a.Y + i, a.Height)));
                 //   Debug.Log("A height "+a.Height);
                 //   Debug.Log("Floor to int: "+theNew);
                 //   if(theNew==null){
                 //       continue;
                 //   }
                 //   Debug.Log("The Tile :"+theNew);
				 // if(String.Equals(tiles.name, theNew.ToString())) {
			if(theNew != null) {		  
				if(terrainTiles.ContainsKey(theNew.ToString())) {
						cost = terrainTiles[theNew.ToString()].cost;
						//break;
					}
			}
			else {
				cost = 1;
			}
				//}
				foreach(GameObject entity in opposingFaction) {
					if(entity.transform.position == new Vector3(a.X, a.Y - i, a.Height)){
						cost = 1000;
				}
				}
				yCost += cost;
			}
		}
        return 1 * (xCost + yCost);

        #region diagonal
        //diagonal
        // Chebyshev distance
        //var D = 1;
        // var D2 = 1;
        //octile distance
        //var D = 1;
        //var D2 = 1;
        //var dx = Math.Abs(a.X - b.X);
        //var dy = Math.Abs(a.Y - b.Y);
        //var result = (int)(1 * (dx + dy) + (D2 - 2 * D));
        //return result;// *= (1 + (1 / 1000));
        //return (int)Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        #endregion
    }
	
/*	    private int Distance(Spot a, Spot b)
    {
        //manhattan
        var dx = (int)Math.Abs(Mathf.Floor(a.X) - Mathf.Floor(b.X));
        var dy = (int)Math.Abs(Mathf.Floor(a.Y) - Mathf.Floor(b.Y));
        return 1 * (dx + dy);

        #region diagonal
        //diagonal
        // Chebyshev distance
        //var D = 1;
        // var D2 = 1;
        //octile distance
        //var D = 1;
        //var D2 = 1;
        //var dx = Math.Abs(a.X - b.X);
        //var dy = Math.Abs(a.Y - b.Y);
        //var result = (int)(1 * (dx + dy) + (D2 - 2 * D));
        //return result;// *= (1 + (1 / 1000));
        //return (int)Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        #endregion
    }
	*/
}
public class Spot
{
    public float X;
    public float Y;
    public int F;
    public int G;
    public int H;
    public float Height = 0;
    public List<Spot> Neighboors;
    public Spot previous = null;
    public Spot(float x, float y, float height)
    {
        X = x;
        Y = y;
        G = 0;
        H = 0;
		F = 0;
        Neighboors = new List<Spot>();
        Height = height;
    }
    public void AddNeighboors(Spot[,] grid, int x, int y)
    {
        if (x < grid.GetUpperBound(0))
            Neighboors.Add(grid[x + 1, y]);
        if (x > 0)
            Neighboors.Add(grid[x - 1, y]);
        if (y < grid.GetUpperBound(1))
            Neighboors.Add(grid[x, y + 1]);
        if (y > 0)
            Neighboors.Add(grid[x, y - 1]);
        #region diagonal
        //if (X > 0 && Y > 0)
        //    Neighboors.Add(grid[X - 1, Y - 1]);
        //if (X < Utils.Columns - 1 && Y > 0)
        //    Neighboors.Add(grid[X + 1, Y - 1]);
        //if (X > 0 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X - 1, Y + 1]);
        //if (X < Utils.Columns - 1 && Y < Utils.Rows - 1)
        //    Neighboors.Add(grid[X + 1, Y + 1]);
        #endregion
    }

}

[System.Serializable]
public class TileTypes {
	public string name;
	public int cost;
	public int damage;
	public string bonus;
	
	public TileTypes() {
		
	}
}
