using UnityEngine;
using System.Collections;

public class Weapon_Item : Item {

	public float damage;
	public float range;
	public float attackSpeed;

	public Weapon_Item(int itemId, string itemName, string itemType, int itemMaxStack, int goldPrice, float damage, float range, float attackSpeed) 
		: base (itemId, itemName, itemType, itemMaxStack, goldPrice)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;
		this.goldPrice = goldPrice;

		this.damage = damage;
		this.range = range;
		this.attackSpeed = attackSpeed;
	}
	public override string ToString ()
	{
		return itemName + "\nDamage: " + damage + "\nRange: " + range + "\n Attack Speed" + attackSpeed;
	}
}
