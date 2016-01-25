using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

	public int itemId;
	public bool canBePickedUp = false;
	public bool isPlayerOver = false;
	float moveSpeed = 2f;

	void Start()
	{
		StartCoroutine(CreationTime());
	}

	void OnTriggerEnter(Collider player)
	{
		if(player.gameObject.CompareTag("Player"))
		{
			isPlayerOver = true;
			StartCoroutine(PickUpSoon());

		}

	}
	void OnTriggerExit(Collider player)
	{
		if(player.gameObject.CompareTag("Player"))
		{
			isPlayerOver = false;

		}
		
	}
	IEnumerator CreationTime()
	{

		yield return new WaitForSeconds(.6f);
		canBePickedUp = true;
	}
	IEnumerator PickUpSoon()
	{
		while(!canBePickedUp || !isPlayerOver)
		{
			yield return null;
		}
		FindObjectOfType<Inventory>().SendMessage("PickUpDropItem",gameObject);
	}
	IEnumerator BeCollected()
	{ 
		GameObject player = FindObjectOfType<PlayerMovement>().gameObject;

		while(Vector3.Distance(transform.position,player.transform.position) > .3f)
		{

			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
			yield return null;
		}
		Destroy (gameObject);
	}
	public void StartBeCollected()
	{
		StartCoroutine(BeCollected());
	}

}
