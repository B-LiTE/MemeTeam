using UnityEngine;
using System.Collections;

public class Weapon_Item : Item {

	public float damage;
	public float range;
	public float attackSpeed;
	public int modelId;
	public enum weaponType {sword,bow,staff};
	public weaponType thisWeaponType;

	public Weapon_Item(int itemId, string itemName, string itemType, int itemMaxStack, int goldPrice, float damage, float range, float attackSpeed, int modelId, weaponType thisWeaponType) 
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
		this.modelId = modelId;
		this.thisWeaponType = thisWeaponType;
	}
	public override string ToString ()
	{
		return itemName + "\nDamage: " + damage + "\nRange: " + range + "\n Attack Speed: " + attackSpeed;
	}
}
