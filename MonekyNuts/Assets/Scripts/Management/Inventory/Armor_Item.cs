using UnityEngine;
using System.Collections;

public class Armor_Item : Item {

	public float armorValue;

	public Armor_Item(int itemId, string itemName, string itemType, int itemMaxStack, float armorValue_) 
		: base (itemId, itemName, itemType, itemMaxStack)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;

		this.armorValue = armorValue_;
	}
}
