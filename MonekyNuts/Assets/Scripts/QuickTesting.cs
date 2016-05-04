using UnityEngine;
using System.Collections;

public class QuickTesting : MonoBehaviour {

	Inventory inventory;

	void Start () 
	{
		inventory = FindObjectOfType<Inventory> ().GetComponent<Inventory> ();
		inventory.AddItem (2);
		inventory.AddItem (16);
		inventory.AddItem (19);
		inventory.AddItem (1);
		inventory.AddItem (24);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
