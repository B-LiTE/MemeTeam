using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public const int INVENTORY_SIZE = 21; // for now
    [SerializeField]
    public Item[] inventory; //the actual inventory which will store the data of the players items

    public GameObject[] slotsDisplay = new GameObject[INVENTORY_SIZE];//the game object with an image component to display the item
    public GameObject[] slotStackCounters = new GameObject[INVENTORY_SIZE]; //PARALLEL ARRAY stores data of how many items are in each stack;
    public int[] stackInSlots = new int[INVENTORY_SIZE];

    public GameObject[] wheelSlots = new GameObject[8]; //wheel is purely a display that reads from the rest of the inventory
    public GameObject[] wheelSlotStackCounters = new GameObject[8]; //the stack counter things for each wheel item

    public int activeSlotIndex; //this is the index for the spot in the list.
    public Item activeItem; //the item in the active slot that the player uses upon attacking enemies

	bool isItemChangeComplete = true;
	public bool isItemMoving = false;
	public GameObject itemOnCursor;
	public int oldSlotIndex; //where the object was before being picked up. Where it can return to if the window is closed while on cursor
    public StateManager currentState;


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.A))
			AddItem (3);
		
		if (Input.GetKeyDown (KeyCode.S))
			AddItem (2);
	}
	void Awake()
	{
		inventory = new Item[INVENTORY_SIZE];
		for(int i= 0; i < INVENTORY_SIZE;i++)
		{ 
			inventory[i] = GetComponent<Item_Database>().allItems[0];
		}

		activeSlotIndex = 1;
	}
	public bool AddItem(int dropItemId) 
	{

			int dropMaxStack = GetComponent<Item_Database> ().allItems [dropItemId].itemMaxStack;
			bool foundPlace = false; //the item can go into the inventory
			int wherePlaced = 0; //the index where the item is ACTUALLY added
			bool breakOut = false;
			bool foundFirstEmptySlot = false;
		
		if (dropMaxStack > 0 && currentState.CurrentState == StateManager.states.realtime) { //if the item is stackable
			breakOut = false;
			foundFirstEmptySlot = false;
			int emptySlot = -1; //if it doesn't find an existing stack to add to, then it will add to the first found empty instead.
			for (int i = 1; i < 9 && !breakOut; i++) { //1 - 8 is hotbar
				if (inventory [i].itemId == dropItemId && stackInSlots [i] < dropMaxStack) { //if theres an existing stack of the item with room
					stackInSlots [i]++; //just add to the existing count
					foundPlace = true;
					wherePlaced = i;
					
					breakOut = true; //escape the loop
				}
				if (!foundFirstEmptySlot) {
					if (inventory [i].itemType == "Empty") {
						emptySlot = i;
						foundFirstEmptySlot = true;
					}
				}
			}
			if (!foundPlace && emptySlot != -1) { //if there wasnt an existing stack add it to the first empty
				inventory [emptySlot] = GetComponent<Item_Database> ().allItems [dropItemId];
				stackInSlots [emptySlot]++; 
				foundPlace = true;
				wherePlaced = emptySlot;
			}
			if (!foundPlace) { //if the hotbar is filled;
				breakOut = false;
				foundFirstEmptySlot = false;
				emptySlot = -1; //if it doesn't find an existing stack to add to, then it will add to the first found empty instead.
				for (int i = 9; i < 21 && !breakOut; i++) { //1 - 50 is the actual inventory
					if (inventory [i].itemId == dropItemId && stackInSlots [i] < dropMaxStack) { //if theres an existing stack of the item with room
						stackInSlots [i]++; //just add to the existing count
						foundPlace = true;
						wherePlaced = i;
							
						breakOut = true; //escape the loop
					}
					if (!foundFirstEmptySlot) {
						if (inventory [i].itemType == "Empty") {
							emptySlot = i;
							foundFirstEmptySlot = true;
						}
					}
				}
				if (!foundPlace && emptySlot != -1) { //if there wasnt an existing stack add it to the first empty
					inventory [emptySlot] = GetComponent<Item_Database> ().allItems [dropItemId];
					stackInSlots [emptySlot]++; 
					foundPlace = true;
					wherePlaced = emptySlot;
				}
			}
		} 
		else if (dropMaxStack > 0 && currentState.CurrentState == StateManager.states.strategy) 
		{
			breakOut = false;
			foundFirstEmptySlot = false;
			int emptySlot = -1; //if it doesn't find an existing stack to add to, then it will add to the first found empty instead.
			for (int i = 1; i < 21 && !breakOut; i++) { //1 - 21 is whole inventory
				if (inventory [i].itemId == dropItemId && stackInSlots [i] < dropMaxStack) { //if theres an existing stack of the item with room
					stackInSlots [i]++; //just add to the existing count
					foundPlace = true;
					wherePlaced = i;
					
					breakOut = true; //escape the loop
				}
				if (!foundFirstEmptySlot) {
					if (inventory [i].itemType == "Empty") {
						emptySlot = i;
						foundFirstEmptySlot = true;
					}
				}
			}
			if (!foundPlace && emptySlot != -1) { //if there wasnt an existing stack add it to the first empty
				inventory [emptySlot] = GetComponent<Item_Database> ().allItems [dropItemId];
				stackInSlots [emptySlot]++; 
				foundPlace = true;
				wherePlaced = emptySlot;
			}
		}
			else { //if its not stackable
				breakOut = false;
				for (int i = 1; i < INVENTORY_SIZE && !breakOut; i++) { //1 - 50 is the actual inventory
					if (inventory [i].itemId == -1) { //if the slot is empty
						inventory [i] = GetComponent<Item_Database> ().allItems [dropItemId];
						stackInSlots [i]++; //just add to the existing count
						foundPlace = true;
						wherePlaced = i;
					
						breakOut = true; //escape the loop
					}
				}
			}
		
			if (foundPlace) {//if one IS found!
				UpdateInventorySprite (wherePlaced);
				UpdateInventoryStackCounter (wherePlaced);

			if(wherePlaced == activeSlotIndex)
			{
				FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>().ChangeActiveWeapon(activeSlotIndex);
			}
			if(wherePlaced <= 8 && wherePlaced >= 1) FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>().ChangeActiveArmor(wherePlaced);
			
			}
		
			return foundPlace; //returns if it successfully added the item or not
		
		
	}
	public void PickUpDropItem(GameObject item)
	{
		if(AddItem(item.GetComponent<PickUpItem>().itemId))
		{
			Destroy(item);
		}
	}
	void UpdateInventorySprite(int slotIndex)
	{
		if(inventory[slotIndex].itemType != "Empty")
		{
			slotsDisplay[slotIndex].GetComponent<Image>().sprite = GetComponent<Item_Database>().inventorySprites[inventory[slotIndex].itemId];

		}
		else
		{
			slotsDisplay[slotIndex].GetComponent<Image>().sprite = null;
		}
		if(slotIndex <= 8 && slotIndex >= 1)
		{
			
			if(inventory[slotIndex].itemType != "Empty")
			{
				wheelSlots[slotIndex].GetComponent<Image>().sprite = GetComponent<Item_Database>().inventorySprites[inventory[slotIndex].itemId];
                wheelSlots[slotIndex].SetActive(true);
			}
			else
			{
				wheelSlots[slotIndex].SetActive(false);
			}
		}
	}
	void UpdateInventoryStackCounter(int slotIndex)
	{
		if(stackInSlots[slotIndex] > 1)
		{
			slotStackCounters[slotIndex].GetComponent<Text>().text = stackInSlots[slotIndex].ToString();
		}
		else slotStackCounters[slotIndex].GetComponent<Text>().text = "";
		
		if(slotIndex <= 8 && slotIndex >= 1)
		{
			wheelSlotStackCounters[slotIndex].GetComponent<Text>().text = slotStackCounters[slotIndex].GetComponent<Text>().text;
		}
	}

	void SlotIsClicked(int slotIndex)
	{
		isItemChangeComplete = false;
		if(!isItemMoving) //if the item isn't on the cursor, this click means an item is being picked up
		{
			if(inventory[slotIndex].itemType != "Empty")
			{
				isItemMoving = true;
				oldSlotIndex = slotIndex;
				itemOnCursor.SetActive(true);
				itemOnCursor.GetComponent<Item_Attached_Cursor>().ChangeSprite(inventory[slotIndex].itemId);
				itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId = inventory[slotIndex].itemId;
				itemOnCursor.GetComponent<Item_Attached_Cursor>().itemStackCount = stackInSlots[slotIndex];
			}
			
			RemoveItemStack(slotIndex);

		}
		else //if the already is an object on the cursor
		{
			if(inventory[slotIndex].itemType != "Empty") //if there is an object where you are trying to move it
			{
				/* LATER STUFF FOR WHEN STACKS CAN BE SPLIT
				if(itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId == inventory[slotIndex]) //if the item on the cursor is the same as the one on the clicked slot
				{
					if(GetComponent<Item_Database>().allItems[inventory[slotIndex].itemId].maxStack >
					   itemOnCursor.GetComponent<Item_Attached_Cursor>().itemStackCount + stackInSlots[slotIndex])
						//if adding these stacks together would go over the max stack count of the item
					{
					}
					else
					{
					}
				}*/
				int tempID = itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId;
				int tempStackCount = itemOnCursor.GetComponent<Item_Attached_Cursor>().itemStackCount;
				itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId = inventory[slotIndex].itemId;
				itemOnCursor.GetComponent<Item_Attached_Cursor>().itemStackCount = stackInSlots[slotIndex];
				itemOnCursor.GetComponent<Item_Attached_Cursor>().ChangeSprite(itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId);
				
				inventory[slotIndex] = GetComponent<Item_Database>().allItems[tempID];
				stackInSlots[slotIndex] = tempStackCount;
				UpdateInventorySprite(slotIndex);
				UpdateInventoryStackCounter(slotIndex);
			}
			else //however if it is empty! just place it!
			{
				;
				isItemMoving = false;
				itemOnCursor.GetComponent<Item_Attached_Cursor>().ChangeSprite(-1); //get rid of its sprite
				itemOnCursor.transform.position = new Vector3(99999,99999,6);
				itemOnCursor.SetActive(false);
				
				PlaceItem(itemOnCursor.GetComponent<Item_Attached_Cursor>().itemId,slotIndex,
				          itemOnCursor.GetComponent<Item_Attached_Cursor>().itemStackCount);
			}
		}
		isItemChangeComplete = true;
		if(slotIndex == activeSlotIndex)
		{
			FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>().ChangeActiveWeapon(activeSlotIndex);
		}
		if(slotIndex <= 8 && slotIndex >= 1) FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>().ChangeActiveArmor(slotIndex);
	}
	void RemoveItemStack(int slotIndex)
	{
		inventory[slotIndex] = GetComponent<Item_Database>().allItems[0]; //set it to empty;
		stackInSlots[slotIndex] = 0; //set count to zero
		UpdateInventorySprite(slotIndex);
		UpdateInventoryStackCounter(slotIndex);
	}
	public void RemoveSingleItem(int slotIndex) //removes a single item from a stack
	{
		stackInSlots [slotIndex]--;
		UpdateInventoryStackCounter(slotIndex);
		if (stackInSlots [slotIndex] < 1) 
		{
			RemoveItemStack(slotIndex);
		}
	}
	void PlaceItem(int itemID, int slotIndex, int stackCount)
	{
		inventory[slotIndex] = GetComponent<Item_Database>().allItems[itemID];
		stackInSlots[slotIndex] = stackCount;
		UpdateInventorySprite(slotIndex);
		UpdateInventoryStackCounter(slotIndex);
	}
	void Start()
	{
		AddItem(1);
		AddItem(2);
		AddItem(2);
		AddItem(1);
	}


  
    
}
