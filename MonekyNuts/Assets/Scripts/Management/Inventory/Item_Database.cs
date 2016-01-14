using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item_Database : MonoBehaviour {

    public Sprite[] inventorySprites = new Sprite[10];

    [SerializeField]
    public List<Item> allItems = new List<Item>();

	void Awake () 
    {
        //temporary!!!!!!!!!!
        allItems.Add(new Item(0,"Empty","Empty",0));
        allItems.Add(new Item(1,"Iron Sword","Weapon",1));
		allItems.Add(new Item(2,"Wheel","Weapon",3));
        allItems.Add(new Item(3, "Tree", "Magic", 6));

	}
	
}
