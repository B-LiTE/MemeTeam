using UnityEngine;
using System.Collections;

public class Weapon_Item : Item {

	public float damage;

	public Weapon_Item(int itemId, string itemName, string itemType, int itemMaxStack, float damage) : base (itemId, itemName, itemType, itemMaxStack)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;
		this.damage = damage;
	}
}
