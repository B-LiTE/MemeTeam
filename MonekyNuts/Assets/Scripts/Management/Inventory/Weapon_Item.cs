using UnityEngine;
using System.Collections;

public class Weapon_Item : Item {

	public float damage;

	public Weapon_Item(int itemId, string itemName, string itemType, int itemMaxStack, int goldPrice, float damage) 
		: base (itemId, itemName, itemType, itemMaxStack, goldPrice)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;
		this.goldPrice = goldPrice;

		this.damage = damage;
	}
}
