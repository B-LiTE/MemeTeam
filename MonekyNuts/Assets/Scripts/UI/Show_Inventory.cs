using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Show_Inventory : MonoBehaviour {

	bool isInventoryVisible = false;
	public GameObject ButtonText;

	public GameObject Inventory_Storage;
	public GameObject Inventory_Wheel;

	public Inventory inventory;

	void ChangeDisplay()
	{
		if(!inventory.isItemMoving)
		{
		if(isInventoryVisible) ButtonText.GetComponent<Text>().text = "Show Inventory";
		else  ButtonText.GetComponent<Text>().text = "Hide Inventory";

		Inventory_Storage.SetActive(!isInventoryVisible);
		Inventory_Wheel.SetActive(!isInventoryVisible);

		isInventoryVisible = !isInventoryVisible;
		}
	}
}
