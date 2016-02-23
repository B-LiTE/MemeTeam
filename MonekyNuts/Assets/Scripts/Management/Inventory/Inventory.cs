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



	void Awake() //the game starts by filling the inventory with empty game objects
	{
        References.stateManager.changeState += UpdateInventorySprites;
        References.stateManager.changeState += UpdateInventorySpriteCounters;
		inventory = new Item[INVENTORY_SIZE];
		for(int i= 0; i < INVENTORY_SIZE;i++)
		{ 
			inventory[i] = GetComponent<Item_Database>().allItems[0];
		}

		activeSlotIndex = 1;
	}
	public bool AddItem(int dropItemId) //adds an item to the inventory - returns false if not enough room
	{

			int dropMaxStack = GetComponent<Item_Database> ().allItems [dropItemId].itemMaxStack;
			bool foundPlace = false; //the item can go into the inventory
			int wherePlaced = 0; //the index where the item is ACTUALLY added
			bool breakOut = false;
			bool foundFirstEmptySlot = false;
		
		if (dropMaxStack > 0 && currentState.CurrentState == StateManager.states.realtime) { //if the item is stackable and in realtime - send new stacks to wheel
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
				if (!foundFirstEmptySlot) 
				{
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
				for (int i = 9; i < 21 && !breakOut; i++) { //9 - 20 is the storage
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
		else if (dropMaxStack > 0 && currentState.CurrentState == StateManager.states.strategy) //if its strategy view, add it to wheel first
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
	public bool AddItemStorage(int dropItemId)
	{
		int dropMaxStack = GetComponent<Item_Database> ().allItems [dropItemId].itemMaxStack;
		int wherePlaced = 0;
		bool foundPlace = false;
		bool breakOut = false;
		if (dropMaxStack > 0) 
		{
			breakOut = false;
			bool foundFirstEmptySlot = false;
			int emptySlot = -1; //if it doesn't find an existing stack to add to, then it will add to the first found empty instead.
			for (int i = 9; i < 21 && !breakOut; i++) { //9 - 21 is only the storage
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
		}
		return foundPlace;
	}
	public void PickUpDropItem(GameObject item) //called by collectable items when player walks over them
	{
		if(AddItem(item.GetComponent<PickUpItem>().itemId))
		{
			//item.SendMessage("PlayPickUpItem");
			GameObject sound = (GameObject)Instantiate(Resources.Load ("OneTimeSoundEffect", typeof (GameObject)));
			sound.GetComponent<AudioSource>().clip = References.soundEffects.soundEffects[1]; //play sound when pick up item
			Destroy(item);
		}
	}
	public void TroopPickUpItem(GameObject item) //called by collectable items when troop walks over them
	{
		if(AddItemStorage(item.GetComponent<PickUpItem>().itemId))
		{
			Destroy(item);
		}
	}
    void UpdateInventorySprites() //updates all the inventory sprites on the wheel
    {
        for (int i = 1; i < 9; i++)
        {
            UpdateInventorySprite(i);
        }
    }
    void UpdateInventorySpriteCounters() //updates all the inventory sprites on the wheel
    {
        for (int i = 1; i < 9; i++)
        {
            UpdateInventoryStackCounter(i);
        }
    }
	void UpdateInventorySprite(int slotIndex) //updates the sprite of a specific slot
	{
		if(inventory[slotIndex].itemType != "Empty") //make the sprite empty
		{
			slotsDisplay[slotIndex].GetComponent<Image>().sprite = GetComponent<Item_Database>().inventorySprites[inventory[slotIndex].itemId];
			slotsDisplay[slotIndex].gameObject.SetActive(true);

		}
		else //get the sprite for the current item
		{
			slotsDisplay[slotIndex].GetComponent<Image>().sprite = null;
			slotsDisplay[slotIndex].gameObject.SetActive(false);
		}
		if(slotIndex <= 8 && slotIndex >= 1) //if its an item also on the wheel then need to update additional sprites
		{
			
			if(inventory[slotIndex].itemType != "Empty")
			{
				wheelSlots[slotIndex].GetComponent<Image>().sprite = GetComponent<Item_Database>().inventorySprites[inventory[slotIndex].itemId];
                if(References.stateManager.CurrentState == StateManager.states.realtime) wheelSlots[slotIndex].SetActive(true);
			}
			else
			{
				wheelSlots[slotIndex].SetActive(false); //deactivate the sprite if empty
			}
		}
	}
	void UpdateInventoryStackCounter(int slotIndex) //updates the display counter
	{
		if(stackInSlots[slotIndex] > 1)
		{
			slotStackCounters[slotIndex].GetComponent<Text>().text = stackInSlots[slotIndex].ToString();
		}
		else slotStackCounters[slotIndex].GetComponent<Text>().text = "";
		
		if(slotIndex <= 8 && slotIndex >= 1)
		{
			wheelSlotStackCounters[slotIndex].GetComponent<Text>().text = slotStackCounters[slotIndex].GetComponent<Text>().text;
            
            if (References.stateManager.CurrentState == StateManager.states.realtime) wheelSlotStackCounters[slotIndex].SetActive(true);
            else wheelSlotStackCounters[slotIndex].SetActive(false);
		}
	}

	void SlotIsClicked(int slotIndex) //when the user taps on an inventory slot
	{
		Item prevItem = inventory[slotIndex];
		if(slotIndex != 0)
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
			if(slotIndex <= 8 && slotIndex >= 1) 
				FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>().ChangeActiveArmor(slotIndex);
		}
		else //this means the trash is being clicked
		{
			isItemMoving = false;
			itemOnCursor.GetComponent<Item_Attached_Cursor>().ChangeSprite(-1); //get rid of its sprite
			itemOnCursor.transform.position = new Vector3(99999,99999,6);
			itemOnCursor.SetActive(false);
		}
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
	void PlaceItem(int itemID, int slotIndex, int stackCount) //place a stack f items onto a slot
	{
		inventory[slotIndex] = GetComponent<Item_Database>().allItems[itemID];
		stackInSlots[slotIndex] = stackCount;
		UpdateInventorySprite(slotIndex);
		UpdateInventoryStackCounter(slotIndex);
	}



  
    
}
