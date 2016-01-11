using UnityEngine;
using System.Collections;

public class Inventory_Click_Detector : MonoBehaviour {

	public int slotIndex; //the location in the inventory this slot represents

	void SlotIsClicked()
	{
		FindObjectOfType<Inventory>().SendMessage("SlotIsClicked",slotIndex);
	}
}
