using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Treasure_Chest : Interactive_Object{
	
	[SerializeField]
	List<Item> contents = new List<Item>();
	
	Item_Database database;
	
	public float maxHorizontalDist;
	public float maxVerticalDist;
	
	public bool isOpened = false;
	public Sprite opened;
	
	public void Start()
	{
		base.Start();
		database = FindObjectOfType<Item_Database>().GetComponent<Item_Database>();
		
		for(int i = 0; i <10;i++)
			contents.Add (database.allItems[1]);
	}
	public override void OnInteraction ()
	{
		if(!isOpened)
		{
			for(int i = 0;i < contents.Count;i++)
			{
				GameObject dropItem = new GameObject(contents[i].itemName);
				dropItem.AddComponent<SpriteRenderer>();
				dropItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
				dropItem.GetComponent<SpriteRenderer>().color = Color.clear;
				dropItem.GetComponent<SpriteRenderer>().sprite = database.inventorySprites[contents[i].itemId];
				dropItem.AddComponent<FadeInFromClear>();
				dropItem.AddComponent<FacePlayer>();
				dropItem.GetComponent<FacePlayer>().target = GameObject.FindGameObjectWithTag("Camera_Realtime").transform;
				dropItem.AddComponent<LevitationBehavior>();
				dropItem.GetComponent<LevitationBehavior>().levitationDistance = 0.2f;
				dropItem.GetComponent<LevitationBehavior>().levitationSpeed = .5f;
				dropItem.GetComponent<LevitationBehavior>().speedChangeRate = 0.5f;
				//dropItem.GetComponent<Levitation>().enabled = false;
				dropItem.AddComponent<PickUpItem>();
				dropItem.GetComponent<PickUpItem>().itemId = contents[i].itemId;
				dropItem.AddComponent<SphereCollider>();
				dropItem.GetComponent<SphereCollider>().radius = 2f;
				dropItem.GetComponent<SphereCollider>().isTrigger = true;

				dropItem.transform.position = new Vector3((transform.position.x + ((2 * maxHorizontalDist)) * (((float)(Random.Range(0,100)) / 100))) - maxHorizontalDist, 
				                                          (transform.position.y + ((2 * maxVerticalDist)) * (((float)(Random.Range(0,100)) / 100))) - maxVerticalDist,
				                                          	(transform.position.z + ((2 * maxHorizontalDist)) * (((float)(Random.Range(0,100)) / 100))) - maxHorizontalDist);
			}
			GetComponent<SpriteRenderer>().sprite = opened;
			gameObject.AddComponent<FadeOutDestroy>();
			Destroy(gameObject.GetComponent<NavMeshObstacle>());
			isOpened = true;
			base.canBeInteracted = false;
		}
		
	}
	
	
}