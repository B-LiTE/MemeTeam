using UnityEngine;
using System.Collections;

public class PlayerStats : KillableInstance {

	public Item activeItem;

	public float baseDamage = 10;
	public float addedDamage;
	public float activeDamage; //add the other two for this one to actually use

	public Inventory inventory;
	
	public void Update(){
		Debug.Log ("" + activeDamage);
	}
	public void Start ()
	{
		inventory = Object.FindObjectOfType<Inventory>().GetComponent<Inventory>();

		activeItem = inventory.inventory [1];
		AddItemStats(activeItem);
		UpdateStats ();
	}
	public void ChangeActiveWeapon(int slotIndex) //pass in the location of where active item has been chenged;
	{
		RemoveItemStats (activeItem);
		activeItem = inventory.inventory [slotIndex];
		AddItemStats (activeItem);

		UpdateStats ();
	}
	public void RemoveItemStats(Item item)
	{
		if(item.itemType == "Weapon")
		{
			Weapon_Item trueItem = item as Weapon_Item;
			addedDamage -= trueItem.damage;
		}
	}
	public void AddItemStats(Item item)
	{
		if(item.itemType == "Weapon")
		{
			Weapon_Item trueItem = item as Weapon_Item;
			addedDamage += trueItem.damage;
		}
	}
	public void UpdateStats()
	{
		activeDamage = addedDamage + baseDamage;
	}
    public override void Die()
    {

    }

}