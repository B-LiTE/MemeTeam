using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item_Database : MonoBehaviour {

    public Sprite[] inventorySprites = new Sprite[50]; //array of sprites for each inventory item

    [SerializeField]
    public List<Item> allItems = new List<Item>(); //array of all the types of items

	void Awake () 
    {
        
        allItems.Add(new Item(0,"Empty","Empty",0,0)); //first entry in the inventory HAS to be empty!
        allItems.Add(new Weapon_Item(1,"Iron Sword","Weapon",1,10,2,3,1));
		allItems.Add(new Armor_Item(2,"Iron Shield","Armor",1,16,10));
        allItems.Add(new Weapon_Item(3, "Volcano Sword", "Weapon",1,20,3,3.2f,1));
		allItems.Add(new Potion_Item(4, "Potion", "Potion",20, 5, 25));
		allItems.Add(new Weapon_Item(5, "Rainbow Sword", "Weapon",1,50,16,3.5f,1));
		allItems.Add(new Weapon_Item(6, "Jagged Knife", "Weapon",1,30,6,3f,1));
		allItems.Add(new Armor_Item(7,"Iron Helm","Armor",1,18,12));
		allItems.Add(new Armor_Item(8,"Iron Plating","Armor",1,20,15));
		allItems.Add(new Armor_Item(9,"Iron Gauntlets","Armor",1,10,5));
		allItems.Add(new Armor_Item(10,"Iron Boots","Armor",1,11,8));
		allItems.Add(new Armor_Item(11,"Steel Plating","Armor",1,40,28));
		allItems.Add(new Weapon_Item(12, "Mechanism Sword", "Weapon",1,40,7,3f,1));
		allItems.Add(new Potion_Item(13, "Alpha Potion", "Potion",20, 10, 50));
		allItems.Add(new Potion_Item(14, "Omega Potion", "Potion",20, 15, 75));
		allItems.Add(new Weapon_Item(15,"Crossbow","Weapon",1,25,4,18,1f));
		allItems.Add(new Weapon_Item(16,"Phoenix","Weapon",1,28,3,25,1f));
		allItems.Add(new Weapon_Item(17,"Wooden Bow","Weapon",1,10,2,20,1f));
		allItems.Add(new Weapon_Item(18,"Green Bow","Weapon",1,35,5,30,1f));
		allItems.Add(new Weapon_Item(19,"Mechanism Bow","Weapon",1,50,8,25,1f));
		allItems.Add (new Armor_Item(20,"Steel Shield","Armor",1,45,20));
		allItems.Add (new Armor_Item(21,"Steel Helm","Armor",1,35,16));
		allItems.Add (new Armor_Item(22,"Steel Gauntlets","Armor",1,30,12));
		allItems.Add(new Armor_Item(23,"Steel Boots","Armor",1,32,14));
		allItems.Add (new Weapon_Item(24,"Titan Blade","Weapon",1,45,12,4f,1));
		allItems.Add(new Weapon_Item(25,"Soulless Staff","Weapon",1,60,16,20,1));
		allItems.Add (new Weapon_Item(26,"Wizard's Wand","Weapon",1,14,3,15,1));
		allItems.Add (new Weapon_Item(27,"Savage Axe","Weapon",1,30,6,3,1));
		allItems.Add (new Weapon_Item(28,"Ninja Knife","Weapon",1,25,5,2,1));
		allItems.Add (new Weapon_Item(29,"Wind Staff","Weapon",1,30,5,17,1));


	}

}
