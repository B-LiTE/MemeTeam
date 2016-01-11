using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public const int INVENTORY_SIZE = 20; // for now
    [SerializeField]
    public Item[] inventory = new Item[INVENTORY_SIZE]; //the actual inventory which will store the data of the players items

    public GameObject[] slotsDisplay = new GameObject[INVENTORY_SIZE];//the game object with an image component to display the item
    public GameObject[] slotStackCounters = new GameObject[INVENTORY_SIZE]; //PARALLEL ARRAY stores data of how many items are in each stack;
    public int[] stackInSlots = new int[INVENTORY_SIZE];

    public GameObject[] wheelSlots = new GameObject[11]; //wheel is purely a display that reads from the rest of the inventory
    public GameObject[] wheelSlotStackCounters = new GameObject[11]; //the stack counter things for each wheel item

    public int activeSlotIndex; //this is the index for the spot in the list.
    public Item activeItem; //the item in the active slot that the player uses upon attacking enemies

    void Awake()
    {
       
    }
    void Update()
    {
        //Debug.Log(inventory[activeSlot].itemName);
    }

  
    
}
