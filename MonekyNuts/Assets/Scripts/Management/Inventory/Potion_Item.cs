using UnityEngine;
using System.Collections;

public class Potion_Item : Item 
{
	public float healAmount;

	public Potion_Item(int itemId, string itemName, string itemType,int itemMaxStack, float healAmount) 
		: base(itemId, itemName, itemType, itemMaxStack)
	{
		this.itemId = itemId;
		this.itemName = itemName;
		this.itemType = itemType;
		this.itemMaxStack = itemMaxStack;

		this.healAmount = healAmount;
	}


}
