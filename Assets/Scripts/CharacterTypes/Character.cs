using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Character : MonoBehaviour// base class (parent) 
{
    [SerializeField]
    protected internal string characterName; 
    [SerializeField]
    protected internal int Level;
    [SerializeField]
    protected internal int exp;
    [SerializeField]
	protected internal Dictionary<string, Stats> stats = new Dictionary<string, Stats>();
    [SerializeField]
	protected internal Races race;
	[SerializeField]
	protected internal Classes playerClass; 

	public int maxHealth;
	public int currentHealth;
	public int maxMana;
	public int currentMana; 
	public int RequiredExperienceToLevelUp{get{return Level*25;}}

	public int baseHealth = 30;

	public EquipmentInfo[] equiped = {null,null,null,null};

/*	protected internal int STR;
	protected internal int DEX;
	protected internal int INT;
	protected internal int WIS;
	protected internal int END;
	protected internal int VIT;
	protected internal int SPD;
	protected internal int LCK;
	*/
    //protected internal ClassType type;
    private int[] expCaps = new int[6]{100, 350, 1000, 2500, 5000, 10000};
    //Race race;


    public Character()
    {
	    characterName ="No Name";
        Level = 1;
        exp = 0;
		stats.Add("STR", new Stats(10f));
		stats.Add("DEX", new Stats(10f));
		stats.Add("VIT", new Stats(10f));
		stats.Add("INT", new Stats(10f));
		stats.Add("WIS", new Stats(10f));
		stats.Add("SPD", new Stats(10f));
		stats.Add("LCK", new Stats(10f));
		stats.Add("AC", new Stats(10f));
		maxHealth=(Level/2) + (int)(stats["VIT"].Value);
		currentHealth=maxHealth;
		maxMana=(Level/2) + (int)(stats["WIS"].Value);
		currentMana = maxMana;
    }
	//initialize some things
	void Start(){
		CombatEvents.OnEnemyDeath+=EnemyToExperience;
	}

    //protected internal void Levelup (int experience)
    
	public Character getCharacter(){
		return this; 
	}
	
	public int getCurrentHealth() {
		return currentHealth;
	}
	
	public void setName(string n) {
		characterName = n; 
	}
	public string getName(){
		return characterName;
	}

	public int getLevel() {
		return Level; 
	}

	public Classes getClass() {
		return playerClass; 
	}
	
	public void setClass(Classes playerClass) {
		this.playerClass = playerClass;
	}
	
	public Dictionary<string, Stats> getStats() {
		return this.stats;
	}
	
	public void addMods(Dictionary<string, StatMods> mods) {
		foreach(KeyValuePair<string, StatMods> mod in mods) {
			stats[mod.Key].AddModifier(mod.Value);
		}
	}
	
	public void removeMods(object source) {
		foreach(string key in stats.Keys) {
			stats[key].RemoveAllModifiersFromSource(source);
		}
	}
	
	public void EnemyToExperience(EnemyTemplate enemy){
		addExp(enemy.experience);
	}
    public void addExp(int expVal)
    {
		exp+=expVal;
		while(exp >= RequiredExperienceToLevelUp){
			exp-=RequiredExperienceToLevelUp;
			LevelUp();
		}
        /*if (Level < 6)
        {
            exp += expVal;
            {
                while (exp >= expCaps[Level - 1] && Level < 6)
                {
                    exp -= expCaps[Level - 1];
                    LevelUp();
                }
            }
        }
		*/
    }

    public void LevelUp()
    {
		//some code with an arbitary level cap
        /*if(Level < 6)
        Level += 1;
        if(Level == 6)
        {
            exp = 9999;
        }
		*/
		Level++;
		maxHealth=baseHealth+(Level/2) + (int)(stats["VIT"].Value)/2;
		baseHealth = maxHealth;
		
		if(playerClass is Warrior && stats["STR"].Value <= 20f) {
			Debug.Log("Warrior Level-Up");
			stats["STR"].AddModifier(new StatMods(1f));
			stats["VIT"].AddModifier(new StatMods(1f));
		}
		else if(playerClass is Scout && stats["SPD"].Value <= 20f) {
			Debug.Log("Scout Level-Up");
			stats["SPD"].AddModifier(new StatMods(1f));
			stats["LCK"].AddModifier(new StatMods(1f));
		}
		else if(playerClass is Mage && stats["INT"].Value <= 20f) {
			Debug.Log("Mage Level-Up");
			stats["INT"].AddModifier(new StatMods(1f));
			stats["SPD"].AddModifier(new StatMods(1f));
		}
		
    }


	public virtual void TakeDamage(int damage){
		currentHealth -= damage;
		if(currentHealth <= 0) {
			currentHealth = 0;
			OnDeath();
		}
		Debug.Log("Character Current Health: " + currentHealth); 
	}
	
	public virtual int DoDamage() {
		System.Random rnd = new System.Random();
		int damage = (int)(stats["STR"].Value)+rnd.Next(1,16);
		Debug.Log("Character damage dealt: " + damage);
		return damage;
	}
	
	public virtual void OnDeath(){
		GameEvents.OnTeamDeath(gameObject);
		
	}

	public void Heal(int amount){
		if(currentHealth+amount>maxHealth){
			currentHealth=maxHealth;
		}
		else{
			currentHealth=currentHealth+amount;
		}
		gameObject.GetComponent<HealthBar>().SetHealth(currentHealth);
	}

	public void GainMana(int amount){
		if(currentMana+amount>maxMana){
			currentMana=maxMana;
		}
		else{
			currentMana=currentMana+amount;
		}
	}

	public void useMana(int amount){
		if(currentMana-amount<0){
			currentMana=0;
		}
		else{
			currentMana=currentMana-amount;
		}
	}
}
