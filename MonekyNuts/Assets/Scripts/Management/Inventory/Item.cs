using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item {

    public int itemId;
    public string itemName;
    public string itemType;

    public int itemMaxStack;

    public Item(int itemId, string itemName, string itemType, int itemMaxStack)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemMaxStack = itemMaxStack;
     
    }
}

