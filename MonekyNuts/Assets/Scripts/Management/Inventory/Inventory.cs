using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    [SerializeField]
    public List<Item> inventory = new List<Item>();
    public int inventorySize = 8; // for now

    public int activeSlot; //this is the index for the spot in the list.
    public GameObject activeItem; //the item in the active slot that the player uses upon attacking enemies

    void Awake()
    {
       for (int i = 0; i < inventorySize; i+=8)
       {
            inventory.Add(new Item(0)); //item ID 0 will be empty gameObjects;
            inventory.Add(new Item(1));
            inventory.Add(new Item(2));
            inventory.Add(new Item(0));
            inventory.Add(new Item(0));
            inventory.Add(new Item(0));
            inventory.Add(new Item(0));
            inventory.Add(new Item(0));
        }
    }
    void Update()
    {
        //Debug.Log(inventory[activeSlot].itemName);
    }

    public void AddItem(Item item)
    {
        bool breakOut = false;
        for (int i = 0; i < inventory.Count && !breakOut; i++)
        {
            if (inventory[i].itemID == 0) //if the slot is empty
            {
                inventory[i] = item;
                breakOut = true;
            }
        }
    }
    public void RemoveItem(int index)
    {
        inventory[index] = new Item(0);
    }
    
}
