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
		if(inventory.AddItem(itemID)) //if theres inventory space
		{
			//reduce gold
		}
	}
}
