using UnityEngine;
using System.Collections;

public class RandomCollectable : MonoBehaviour {

	//items to instantiant
	[SerializeField]
	GameObject itemPrefab;
	[SerializeField]
	GameObject goldPrefab;

	GameObject objectSpawned;

	int counterAmount;
	int counter;

	void Start () 
	{
		References.stateManager.changeState += onReturnToRealTime;
		counterAmount = (int)Random.Range(2,5);
		counter = counterAmount;
		SpawnRandomInstance();
	}
	void SpawnRandomInstance() //spawns either gold or an item
	{
		if (Random.Range (0, 2) == 0) //50% change to either be a item or gold
		{
			objectSpawned = Instantiate(itemPrefab);
			objectSpawned.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			objectSpawned.GetComponent<PickUpItem>().itemId = References.market.activeArray[(int)Random.Range(0,References.market.activeArray.Length)];
		} 
		else 
		{
			objectSpawned = Instantiate(goldPrefab);
			objectSpawned.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
		}
	}

	void onReturnToRealTime() //after 2 -4 view changes, another item will spawn
	{
		if(References.stateManager.CurrentState == StateManager.states.realtime)
		{
			if(objectSpawned == null)
			{
				counter--;
			}
			if(counter == 0)
			{
				counter = counterAmount;
				SpawnRandomInstance();
			}
		}
	}
}
