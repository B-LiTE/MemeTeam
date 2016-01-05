using UnityEngine;
using System.Collections;

public class SquadVariables : MonoBehaviour {

	public int unitCount; // how many unites in a squad;
	public float unitHealth; // how much health each individual soldier has;
	private float baseHealth; //base health - if it's only Dr. Meme left he's stronger than usual.
	public float TotHealth; //health of the squad - base health added to all of the unites;

	public float baseArmor;
	public float addedArmor;
	public float TotArmor;

	public float baseAttackPower;
	public float addedAttackPower;
	public float TotAttackPower;

	public GameObject ItemSlot;

	void Start () 
	{
		UpdateStats ();
	}
	void Update () 
	{
	
	}
	void UpdateSprite()
	{
		//called to adjust squad appearance based on their numbers
	}
	void TakeDamage(float damage) //would call this from weapon script.
	{
		float damage_multiplier = 100 / (100 + TotArmor);
			
		TotHealth -= damage * damage_multiplier;
		
		int units_lost = Mathf.FloorToInt((damage * damage_multiplier) / (unitHealth)); //these values need to be ints so rounds down to whole number for units lost. If this formula doesn’t make sense, I can explain. I’m not sure why it works, but the algebra works soooo ;)
			
		unitCount -= units_lost;
		UpdateStats ();
	}
	void UpdateStats() //called to recalculate attack power, armor and health either based off of possible death of units, or equipping new items to soldiers.
	{
		TotHealth = baseHealth + (unitCount * unitHealth);

		//Need to Implement a Class attatched to a player child object to hold equippable items
		//addedArmor = ItemSlot.addedArmor;
		//addedAttack = ItemSlot.addedAttack;
	}
}
