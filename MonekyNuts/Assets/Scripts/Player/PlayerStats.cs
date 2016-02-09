using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : KillableInstance {

	public Item activeItem;
	public Item[] armorArray = new Item[9];

	public float baseDamage = 10;
	public float addedDamage;
	public float activeDamage; //add the other two for this one to actually use

	public Inventory inventory;

	public RectTransform healthBar;
	
	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			Damage (10);
		}
	}

	public void Start ()
	{
		inventory = Object.FindObjectOfType<Inventory>().GetComponent<Inventory>();

		activeItem = inventory.inventory [1];
		for (int i = 1; i < 9; i++)armorArray [i] = inventory.inventory [i];
		AddItemStats(activeItem);
		UpdateStats ();
		//
		totHealth = 100;
		currHealth = totHealth;
	}

	public void ChangeActiveWeapon(int slotIndex) //pass in the location of where active item has been chenged;
	{
		if (inventory.inventory [slotIndex].itemType == "Weapon") {
			inventory.activeSlotIndex = slotIndex;
			RemoveItemStats (activeItem);
			activeItem = inventory.inventory [slotIndex];
			AddItemStats (activeItem);

			UpdateStats ();
		}
	}
	public void ChangeActiveArmor(int slotIndex) //pass in the location of where armor has been chenged;
	{
		RemoveItemStats (armorArray[slotIndex]);
		armorArray [slotIndex] = inventory.inventory [slotIndex];
		AddItemStats (armorArray[slotIndex]);
		
		//UpdateStats ();
	}

	public void RemoveItemStats(Item item)
	{
		if(item.itemType == "Weapon")
		{
			Weapon_Item trueItem = item as Weapon_Item;
			addedDamage -= trueItem.damage;
		}
		else if(item.itemType == "Armor")
		{
			Armor_Item trueItem = item as Armor_Item;
			armor -= trueItem.armorValue;
		}
	}

	public void AddItemStats(Item item)
	{
		if(item.itemType == "Weapon")
		{
			Weapon_Item trueItem = item as Weapon_Item;
			addedDamage += trueItem.damage;
		}
		else if(item.itemType == "Armor")
		{
			Armor_Item trueItem = item as Armor_Item;
			armor += trueItem.armorValue;
		}
	}

	public void UpdateStats()
	{
		activeDamage = addedDamage + baseDamage;
	}

	public override void ChangeHealth (float amount)
	{
		base.ChangeHealth (amount);
		UpdateHealthBar ();
	}
	public override void Damage(float amount)
	{
		base.Damage (amount);
		UsePotion();
		UpdateHealthBar ();
	}
	public void UsePotion()
	{
		if (currHealth <= (.1f * totHealth)) 
		{
			for(int i = 1;i < 9; i++) //checks the wheel
			{
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
	public void UpdateHealthBar()
	{
		if (currHealth / totHealth > 0) {
			healthBar.localScale = new Vector3 (currHealth / totHealth, 
		                                    healthBar.localScale.y, 
		                                    healthBar.localScale.z);
		} 
		else 
		{
			healthBar.localScale = new Vector3 (0, 
			                                    healthBar.localScale.y, 
			                                    healthBar.localScale.z);
		}
	}

    protected override void Die()
    {
        Debug.LogError("he dead");
    }

	

		 

}