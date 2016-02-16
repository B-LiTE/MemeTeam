using UnityEngine;
using System.Collections;

public class RandomCollectable : MonoBehaviour {

	[SerializeField]
	GameObject itemPrefab;
	[SerializeField]
	GameObject goldPrefab;

	void Start () 
	{
		if (Random.Range (0, 2) == 0) 
		{
			GameObject item = Instantiate(itemPrefab);
			item.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			item.GetComponent<PickUpItem>().itemId = References.market.activeArray[(int)Random.Range(0,References.market.activeArray.Length)];
			Destroy (gameObject);
		} 
		else 
		{
			GameObject gold = Instantiate(goldPrefab);
			gold.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
