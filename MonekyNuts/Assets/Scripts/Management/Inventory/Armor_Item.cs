using UnityEngine;
using System.Collections;

public class Armor_Item : Item {

	public float armorValue;

	public Armor_Item(int itemId, string itemName, string itemType, int itemMaxStack,int goldPrice, float armorValue_) 
		: base (itemId, itemName, itemType, itemMaxStack, goldPrice)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;
		this.goldPrice = goldPrice;

		this.armorValue = armorValue_;
	}
}
