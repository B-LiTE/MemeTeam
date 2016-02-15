using UnityEngine;
using System.Collections;

public class Potion_Item : Item 
{
	public float healAmount;

	public Potion_Item(int itemId, string itemName, string itemType,int itemMaxStack, int goldPrice, float healAmount) 
		: base(itemId, itemName, itemType, itemMaxStack, goldPrice)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;
		this.goldPrice = goldPrice;

		this.healAmount = healAmount;
	}
	public override string ToString ()
	{
		return itemName + "\nHeal Amount: " + healAmount;
	}

}
