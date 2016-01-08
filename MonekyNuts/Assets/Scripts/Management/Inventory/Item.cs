using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item {

    public int itemID;
    public string itemName;
    public string itemType;

    public Item(int itemID)
    {
        this.itemID = itemID;
        //sets up variables based off of database data we can pull up using item ID

        //temporary test stuff!
        if (itemID == 0)
        {
            itemName = "Sword";
            itemType = "Weapon";
        }
        if (itemID == 1)
        {
            itemName = "Wand";
            itemType = "Weapon";
        }
        if (itemID == 2)
        {
            itemName = "Iron Chestplate";
            itemType = "Armor";
        }
    }
}

