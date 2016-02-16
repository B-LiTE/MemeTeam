using UnityEngine;
using System.Collections;

public class Purchase_Item : MonoBehaviour {

	public int itemID;
	public Inventory inventory;

	void Start()
	{
		inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
	}
	public void PurchaseItem()
	{
		if (References.gameStats.goldCount >= References.itemDatabase.allItems [itemID].goldPrice) 
		{
			if (inventory.AddItem (itemID)) 
			{ 
				References.gameStats.ChangeGoldCount(-References.itemDatabase.allItems [itemID].goldPrice);
			}
		}
	}
}
