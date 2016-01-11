using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item {

    public int itemID;
    public string itemName;
    public string itemType;

    public int itemMaxStack;

    public Item(int itemID, string itemName, string itemType, int itemMaxStack)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemMaxStack = itemMaxStack;
        //sets up variables based off of database data we can pull up using item ID

        /*
        //temporary test stuff!
        if (itemID == 0)
        {
            itemName = "Empty";
            itemType = "Empty";
        }
        if (itemID == 1)
        {
            itemName = "Sword";
            itemType = "Weapon";
        }
        if (itemID == 2)
        {
            itemName = "Wand";
            itemType = "Weapon";
        }
        if (itemID == 3)
        {
            itemName = "Iron Chestplate";
            itemType = "Armor";
        }*/
    }
}

