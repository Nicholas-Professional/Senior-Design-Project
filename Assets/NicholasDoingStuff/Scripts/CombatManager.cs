using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System;



	public enum State{
		startCombat,
		turnStart,
		movement,
		actions,
		endOfTurn,
		endOfCombat
    }


public class CombatManager : MonoBehaviour
{
	public static int opt = 0;
	private int start = 0; 
	private bool startTurn = true;
	private bool endTurn = true;
	private bool movement = true;
	public State state;
	SortedList<int, GameObject> initiatives;
	TeamManager team;
	// Start is called before the first frame update
	void Start()
    {
		var descendingComparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

		System.Random rnd = new System.Random();
		//getting team from the TeamManager
		GameObject[] entities = GameObject.Find("Team").GetComponent<TeamManager>().getAvailableAndAliveTeamMembers();
		//saving TeamManager for later
		team=GameObject.Find("Team").GetComponent<TeamManager>();
		GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
		initiatives = new SortedList<int, GameObject>(descendingComparer);
		foreach (GameObject player in entities) {
			//Debug.Log(rnd.Next(0, 21));
			//rnd.Next(0,21) + Spd
			if(team.checkIfAlive(player)) {
				player.GetComponent<SpriteRenderer>().color = Color.white;
			}
			int initiative = rnd.Next(0, 21) + (int)(player.GetComponent<Character>().getStats()["SPD"].Value);
			try
			{
				initiatives.Add(initiative, player);
				player.GetComponent<HealthBar>().SetMaxHealth(player.GetComponent<Character>().getCurrentHealth());
			}
			catch(ArgumentException)
            {
				int clash = 1;
				while(clash != 0) {
					bool clashOccurred = false; 
					foreach(KeyValuePair<int, GameObject> temp in initiatives) {
						if(initiative + clash == temp.Key) {
							clash++;
							clashOccurred = true;
							break;
						}
					}
					if(!clashOccurred) {
						initiatives.Add(initiative+clash, player);
						clash = 0; 
					}
				}
				player.GetComponent<HealthBar>().SetMaxHealth(player.GetComponent<Character>().getCurrentHealth());
            }
		}
		
		foreach (GameObject enemies in enemy) {
			//Debug.Log(rnd.Next(0, 21));
			//rnd.Next(0,21) + Spd
			int initiative = rnd.Next(0, 21) + (int)(enemies.GetComponent<Grab>().template.getStats()["SPD"].Value);
			try
			{
				initiatives.Add(initiative, enemies);
				enemies.GetComponent<HealthBar>().SetMaxHealth(enemies.GetComponent<Grab>().template.getCurrentHealth());
			}
			catch (ArgumentException)
			{
								int clash = 1;
				while(clash != 0) {
					bool clashOccurred = false; 
					foreach(KeyValuePair<int, GameObject> temp in initiatives) {
						if(initiative - clash == temp.Key) {
							clash++;
							clashOccurred = true;
							break;
						}
					}
					if(!clashOccurred) {
						initiatives.Add(initiative-clash, enemies);
						clash = 0; 
					}
				}
				enemies.GetComponent<HealthBar>().SetMaxHealth(enemies.GetComponent<Grab>().template.getCurrentHealth());
			}
		}
/*		foreach (GameObject allies in ally) {
			//Debug.Log(rnd.Next(0, 21));
			//rnd.Next(0,21) + Spd
			int initiative = rnd.Next(0, 21);
			try
			{
				initiatives.Add(initiative, allies);
				//allies.GetComponent<HealthBar>().SetMaxHealth();
			}
			catch (ArgumentException)
			{
				initiatives.Add(initiative - 1, allies);
				//allies.GetComponent<HealthBar>().SetMaxHealth();
			}
		}
		for(int i = 0; i < initiatives.Count; i++)
        {
			Debug.Log(initiatives.Keys[i]);
			Debug.Log(initiatives.Values[i].name);
        } */
		if(enemy.Length == 0) {
			state = State.endOfCombat;
		}
		else {
		state = State.turnStart;
		}
}

	private void Update()
    {
		
		if(state == State.endOfTurn || state == State.actions) {
		for(int i = 0; i < initiatives.Count- 1; i++) {
			if(initiatives.Values[i] != null) {	
				if(initiatives.Values[i].tag == "Player" && !team.checkIfAlive(initiatives.Values[i])){
					initiatives.Values[i].GetComponent<SpriteRenderer>().color = Color.grey;
				}
			}
		}
		}
		
		switch(state)
        {
			case State.turnStart:
				//Start of turn functionality
				if(startTurn) {
					startTurn = false;
					StartCoroutine(StartCheckUp(initiatives.Values[start], () =>
					{
						state = State.movement;
					}));
				}
				//Debug.Log(GridManager.finished);
				break;
			case State.movement:
				//Movement of player and enemies, checks for tiles, and move costs
				//if(Player is clicked on go action state) 
				//if(Player clicks on tile on map attempt pathmaking)
				if (Input.GetMouseButtonDown(0) && initiatives.Values[start].tag != "Enemy" && movement)
				{
					movement = false;
					StartCoroutine(MoveEntity(initiatives.Values[start], () =>
					{
					//if Confirm button is pressed go to next state otherwise revert to original position then prompt movement again
					state = State.actions;
					}));
				}
				else if (initiatives.Values[start] != null && initiatives.Values[start].tag == "Enemy" && movement) {
					movement = false;
					StartCoroutine(MoveEnemy(initiatives.Values[start], () =>
					{
						//if Confirm button is pressed go to next state otherwise revert to original position then prompt movement again
						state = State.actions;
					}));
				}

				break;
			case State.actions:
				//Handle action functions with abilities and items
				/*if (Input.GetMouseButtonDown(0) && opt == 0 && initiatives.Values[start].tag != "Enemy")
				{
					opt = 1;
					Attack(initiatives.Values[start], () =>
					{
						opt = 0; 
						state = State.endOfTurn;
					});
				}
				else if(Input.GetMouseButtonDown(1) && opt == 0 && initiatives.Values[start].tag != "Enemy"){
					opt = 2; 
					Attack(initiatives.Values[start], () =>
					{
						opt = 0;
						state = State.endOfTurn;
					});
				}*//*
				else if(Input.GetKeyDown(KeyCode.Return) && opt == 0 && initiatives.Values[start].tag != "Enemy") {
					opt = 3;
					StartCoroutine(Target(initiatives.Values[start]));
					/*Debug.Log(Targetting.confirmed);
					Targetting.confirmed) {
					opt = 0;
					state = State.endOfTurn;
					}
					else {
					 Debug.Log(opt);
					}
					}); 
					
				}*/
				if(AbilityManager.completed){
					AbilityManager.completed=false;
					state=State.endOfTurn;
				}
				else if (initiatives.Values[start].tag == "Enemy" && opt == 0) {
					Debug.Log("Attempting to call enemyAI");
					opt = -1; 
					initiatives.Values[start].GetComponent<Grab>().template.enemyAIPattern();
					opt = 0; 
					state = State.endOfTurn; 
				}
				//}
				break;
			case State.endOfTurn:
				if (endTurn)
				{
					endTurn = false;
					StartCoroutine(EndTurn(initiatives.Values[start], () =>
					{
						int enemiesLeft = 0;
						for (int i = 0; i < initiatives.Count; i++)
						{
							
							if(initiatives.Values[i] != null) {
							
							if (initiatives.Values[i].tag == "Enemy")
							{
								enemiesLeft++;
							}
						}
						}
						if (enemiesLeft != 0)
						{
							state = State.turnStart;
						}
						else
						{
							state = State.endOfCombat;
						}
					}));
				}
				//Resolve end of turn resolutions like decrementing turn based actives and debuffs
				break;
			case State.endOfCombat:
			//Debug.Log("Combat is finished");

			break;
        }

    }
	
	public State getState() {
		return state; 
	}
	
	public GameObject getTurnEntity() {
		return initiatives.Values[start];
	}

	public IEnumerator Target(GameObject current) {
		
		while(!Targetting.confirmed) {
			yield return null;
		}
		
		while(!AbilityManager.completed) {
			yield return null; 
		}
		//current.GetComponent<SlipAShoe>().resolve(Targetting.lastPos, initiatives, current);
		opt = 0;
		state = State.endOfTurn;
		Debug.Log(state);
	}
	
	public IEnumerator EndTurn(GameObject current, Action onTurnEnd)
    {
		if(current != null) {
		current.GetComponent<SpriteRenderer>().color = Color.white;
		Debug.Log("End Turn of " + current.name);
		}
		bool foundNextPerson = false;
		int i = start; 
		while(!foundNextPerson) {
		i += 1;
		Debug.Log(initiatives.Count);
			if (i > initiatives.Count - 1)
			{
			i = 0;
			}
		if(initiatives.Values[i] != null) {
			if(initiatives.Values[i].tag == "Player" && team.checkIfAlive(initiatives.Values[i])) {
				foundNextPerson = true;
			}
			else if(initiatives.Values[i].tag != "Player") {
			foundNextPerson = true;
			}
		}
			yield return null;
		}
		/*if(initiatives.Values[start] == null && start == initiatives.Count-1) {
			initiatives.Remove(initiatives.Keys[start]);
		}
		else if(initiatives.Values[start] == null && start != initiatives.Count-1) {
			initiatives.Remove(initiatives.Keys[start]);
			start -= 1;
		}
		*/
		/* for(int i = 0; i < initiatives.Count; i++) {
			//checking if enemy or ally was destroyed
			if(initiatives.Values[i] == null) {
				if(i < start || i == initiatives.Count - 1) {
				initiatives.Remove(initiatives.Keys[i]);
				Debug.Log("Entity was destroyed");
				}
				else if (i >= start && i < initiatives.Count-1) {
					start -= 1;
					initiatives.Remove(initiatives.Keys[i]);
					Debug.Log("Entity was destroyed");
				}
			}
			//checking if player/teammember is still alive
			else{
				if(initiatives.Values[i].tag=="Player"){
					//if they are dead remove them from initiaives
					if(!team.checkIfAlive(initiatives.Values[i])){
						if(i < start) {
						initiatives.Values[i].GetComponent<SpriteRenderer>().color = Color.grey;
						initiatives.Remove(initiatives.Keys[i]);
						Debug.Log("Entity was destroyed");
						}
						else {
						start -= 1;
						initiatives.Values[i].GetComponent<SpriteRenderer>().color = Color.grey;
						initiatives.Remove(initiatives.Keys[i]);
						Debug.Log("Entity was destroyed");
						}
						
						//initiatives.Remove(initiatives.Keys[i]);
					}
				}
			}
		} */
		start = i;
		onTurnEnd();
		endTurn = true;
	}

	public IEnumerator StartCheckUp(GameObject current, Action onCheckupComplete)
    {
		if(current == null) {
			state = State.endOfTurn;
			yield return null;
		}
		else {
		Debug.Log("Start of " + current.name + " turn");
		yield return new WaitForSeconds(1.5f);
		current.GetComponent<SpriteRenderer>().color = Color.red;
		//Completed Checkup go to next state
		onCheckupComplete();
		startTurn = true;
		}
    }

	public IEnumerator MoveEntity(GameObject entity, Action onMoveComplete)
    {
		Debug.Log("Moving entity " + entity.name);
		/*while(GridManager.finished) {
			Debug.Log("Waiting for movement");
			yield return null;
		}
		*/
		while(!GridManager.finished) {
			//Debug.Log("Waiting for movement to complete");
			yield return null; 
		}
		while(!MovementConfirmation.movementConfirmed) {
			yield return null;
		}
		MovementConfirmation.movementConfirmed = false;
		onMoveComplete();
		movement = true;
		//give the entity object over to the Grid Manager and perform pathmaking for that entity
		//After movement is completed come back to here and check if the player wants to move on
		//Debug.Log("Finished moving entity");
		
    }
	
		public IEnumerator MoveEnemy(GameObject entity, Action onMoveComplete)
    {
		//Debug.Log("Moving entity " + entity.name);
		while(!GridManager.finished) {
			yield return null; 
		}
		onMoveComplete();
		movement = true;
		//give the entity object over to the Grid Manager and perform pathmaking for that entity
		//After movement is completed come back to here and check if the player wants to move on
		Debug.Log("Finished moving entity");
		
    }

}
