using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item_Database : MonoBehaviour {

    public Sprite[] inventorySprites = new Sprite[50];

    [SerializeField]
    public List<Item> allItems = new List<Item>();

	void Awake () 
    {
        //temporary!!!!!!!!!!
        allItems.Add(new Item(0,"Empty","Empty",0)); //first entry in the inventory HAS to be empty!
        allItems.Add(new Weapon_Item(1,"Iron Sword","Weapon",1,666));
		allItems.Add(new Item(2,"Wheel","Magic",3));
        allItems.Add(new Item(3, "Tree", "Magic", 6));
		allItems.Add(new Potion_Item(4, "Potion", "Potion", 5, 50));
		allItems.Add(new Armor_Item (5, "Armor", "Armor", 1, 10));

	}
	
}
