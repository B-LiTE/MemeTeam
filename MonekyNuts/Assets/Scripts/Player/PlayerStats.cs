using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Holds all of the player's stats and inventory methods

public class PlayerStats : KillableInstance {

	public Item activeItem;
	public Item[] armorArray = new Item[9];

	public float baseDamage = 10;
	public float addedDamage;
	public float activeDamage; //add the other two for this one to actually use

	public float baseAttackSpeed;
	public float attackSpeed;
	public float baseAttackRange;
    public float attackRange;
    public float secondsBetweenAttacks;

	public Inventory inventory;
	public float goldCount;

	public RectTransform uiHealthBar;

	public void Start ()
	{
		inventory = Object.FindObjectOfType<Inventory>().GetComponent<Inventory>();

		activeItem = inventory.inventory [1];
		for (int i = 1; i < 9; i++)armorArray [i] = inventory.inventory [i];
		ChangeActiveWeapon(1);
		UpdateDamageStats ();

		totHealth = 100;
		currHealth = totHealth;
		baseAttackRange = 3;
		baseAttackSpeed = 1;
		attackRange = baseAttackRange;

		goldCount = 0;
	}











    // Change the hotwheel active weapon slot
	public void ChangeActiveWeapon(int slotIndex) //pass in the location of where active item has been chenged;
	{		
			inventory.activeSlotIndex = slotIndex;
			RemoveItemStats (activeItem,"Weapon");
			activeItem = inventory.inventory [slotIndex];
			AddItemStats (activeItem,"Weapon");
		

			UpdateDamageStats ();
		
	}

    // Change the hotwheel active armor slot
	public void ChangeActiveArmor(int slotIndex) //pass in the location of where armor has been chenged;
	{
		RemoveItemStats (armorArray[slotIndex],"Armor");
		armorArray [slotIndex] = inventory.inventory [slotIndex];
		AddItemStats (armorArray[slotIndex],"Armor");
	}











    // Remove the stats of the current item from our stats
	public void RemoveItemStats(Item item, string type)
	{
		if(type == "Weapon")
		{
			if(item.itemType == "Weapon")
			{
				Weapon_Item trueItem = item as Weapon_Item;
				addedDamage -= trueItem.damage;
				attackSpeed = baseAttackSpeed;
				attackRange = baseAttackRange;
			}
		}
		else if(type == "Armor")
		{
			if(item.itemType == "Armor")
			{
			Armor_Item trueItem = item as Armor_Item;
			Armor -= trueItem.armorValue;
			}
		}
	}

    // Add the stats of the current item to our stats
	public void AddItemStats(Item item, string type)
	{
		if(type == "Weapon")
		{
			if(item.itemType == "Weapon")
			{
				Weapon_Item trueItem = item as Weapon_Item;
				addedDamage += trueItem.damage;
				attackSpeed = trueItem.attackSpeed;
				attackRange = trueItem.range;
			}
		}
		else if(type == "Armor")
		{
			if(item.itemType == "Armor")
			{
			Armor_Item trueItem = item as Armor_Item;
			Armor += trueItem.armorValue;
			}
		}
	}

    // Update the damage stats
	public void UpdateDamageStats()
	{
		activeDamage = addedDamage + baseDamage;
	}











    // Override the KillableInstance method to include updating the UI health bar
	public override void ChangeHealth (float amount)
	{
		base.ChangeHealth (amount);
		UpdateUIHealthBar ();
	}


    // Override the KillableInstance method to include checking for potions and updating the health bar
	public override void Damage(float amount, GameObject attacker)
	{

		base.Damage (amount, attacker);
		UsePotion();
		UpdateUIHealthBar ();
	}


    // When we die, load the lose screen
    protected override void Die()
    {
        References.stateManager.loadLoseLevel();
    }











    // Check the hotwheel to see if we have a potion. If so, use it if we need to
	public void UsePotion()
	{
		if (currHealth <= (.1f * totHealth)) 
		{
			for(int i = 1;i < 9; i++) //checks the wheel
			{
                // If we found a potion, use it and stop checking for potions
				if(inventory.inventory[i].itemType == "Potion")
				{
					Potion_Item potion = inventory.inventory[i] as Potion_Item;
					ChangeHealth(potion.healAmount);
					inventory.RemoveSingleItem(i);
					break;
				}
			}
		}
	}


    // Updates the player health bar at the top of the screen
	public void UpdateUIHealthBar()
	{
        // If our health is greater than zero, scale the health bar to represent our health
		if (currHealth / totHealth > 0)
        {
            uiHealthBar.localScale = new Vector3(currHealth / totHealth, uiHealthBar.localScale.y, uiHealthBar.localScale.z);
		} 
		else 
		{
            // Otherwise, show none of the health bar
            uiHealthBar.localScale = new Vector3(0, uiHealthBar.localScale.y, uiHealthBar.localScale.z);
		}
	}



	

		 

}