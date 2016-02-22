using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Treasure_Chest : Interactive_Object{
	
	[SerializeField]
	List<Item> contents = new List<Item>();
	[SerializeField]
	GameObject collectable;
	
	Item_Database database;
	
	public float maxHorizontalDist;
	public float maxVerticalDist;
	
	public bool isOpened = false;
	public Sprite opened;
	
	public void Start()
	{
		base.Start();
		database = FindObjectOfType<Item_Database>().GetComponent<Item_Database>();
		
		for(int i = 0; i <3;i++)
			contents.Add (database.allItems[References.market.activeArray[(int)Random.Range(1,References.market.activeArray.Length)]]);
	}
	public override void OnInteraction ()
	{
		if(!isOpened)
		{
			for(int i = 0;i < contents.Count;i++)
			{
				GameObject newCollectable = (GameObject)Instantiate(collectable);
                newCollectable.GetComponent<PickUpItem>().itemId = contents[i].itemId;
                newCollectable.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newCollectable.GetComponent<SpriteRenderer>().color = Color.clear;
                newCollectable.AddComponent<FadeInFromClear>();

                newCollectable.transform.position = new Vector3((transform.position.x + ((2 * maxHorizontalDist)) * (((float)(Random.Range(0, 100)) / 100))) - maxHorizontalDist, 
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