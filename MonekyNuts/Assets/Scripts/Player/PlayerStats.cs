using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		ChangeActiveWeapon(1);
		UpdateStats ();
		//
		totHealth = 100;
		currHealth = totHealth;
		baseAttackRange = 3;
		baseAttackSpeed = 1;

		goldCount = 0;
	}

	public void ChangeActiveWeapon(int slotIndex) //pass in the location of where active item has been chenged;
	{		
			inventory.activeSlotIndex = slotIndex;
			RemoveItemStats (activeItem,"Weapon");
			activeItem = inventory.inventory [slotIndex];
			AddItemStats (activeItem,"Weapon");
		

			UpdateStats ();
		
	}
	public void ChangeActiveArmor(int slotIndex) //pass in the location of where armor has been chenged;
	{


		RemoveItemStats (armorArray[slotIndex],"Armor");
		armorArray [slotIndex] = inventory.inventory [slotIndex];
		AddItemStats (armorArray[slotIndex],"Armor");
		

		
	}

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
				Debug.Log ("added damage is currently" + addedDamage);
			}
		}
		else if(type == "Armor")
		{
			if(item.itemType == "Armor")
			{
			Armor_Item trueItem = item as Armor_Item;
			armor -= trueItem.armorValue;
			}
		}
	}

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
				Debug.Log ("added damage is currently" + addedDamage);
			}
		}
		else if(type == "Armor")
		{
			if(item.itemType == "Armor")
			{
			Armor_Item trueItem = item as Armor_Item;
			armor += trueItem.armorValue;
			}
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