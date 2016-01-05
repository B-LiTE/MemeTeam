using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadVariables : MonoBehaviour {

	public int unitCount; // how many unites in a squad;
    public List<GameObject> units = new List<GameObject>();

	public float TotHealth; //health of the squad - base health added to all of the unites;

	public GameObject ItemSlot;

	void Start () 
	{
		UpdateStats ();
	}

	void UpdateSprite()
	{
		//called to adjust squad appearance based on their numbers
	}
	
	void UpdateStats() //called to recalculate attack power, armor and health either based off of possible death of units, or equipping new items to soldiers.
	{
		//Need to Implement a Class attatched to a player child object to hold equippable items
		//addedArmor = ItemSlot.addedArmor;
		//addedAttack = ItemSlot.addedAttack;
	}
}
