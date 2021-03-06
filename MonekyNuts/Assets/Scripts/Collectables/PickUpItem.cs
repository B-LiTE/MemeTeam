﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour {

	public int itemId;
	public bool canBePickedUp = false;
	public bool isPlayerOver = false;
	float moveSpeed = 2f;

	[SerializeField]
	AudioClip clip;

	void Start()
	{
		StartCoroutine(CreationTime());
		GetComponent<SpriteRenderer> ().sprite = References.itemDatabase.inventorySprites [itemId];
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.CompareTag ("Player")) 
		{
			FindObjectOfType<Inventory>().SendMessage("PickUpDropItem",gameObject);

		} 
		else if (player.CompareTag ("Troop")) 
		{
			FindObjectOfType<Inventory>().SendMessage("TroopPickUpItem",gameObject);
		}

	}
	void OnTriggerExit(Collider player)
	{
		if(personCanPickUp(player.gameObject))
		{
			isPlayerOver = false;

		}
		
	}
	IEnumerator CreationTime()
	{

		yield return new WaitForSeconds(.6f);
		canBePickedUp = true;
	}
	IEnumerator PickUpSoon(string type)
	{
		while(!canBePickedUp || !isPlayerOver)
		{
			yield return null;
		}
		FindObjectOfType<Inventory>().SendMessage("PickUpDropItem",gameObject);
	}
	/*IEnumerator BeCollected()
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
	}*/

    bool personCanPickUp(GameObject person)
    {
        switch (person.tag)
        {
            case "Player":
            case "Troop":
                return true;

            default:
                return false;
        }
    }
	public void PlayPickUpItem()
	{
		GameObject sound = (GameObject)Instantiate(Resources.Load ("OneTimeSoundEffect", typeof (GameObject)));
		sound.GetComponent<AudioSource>().clip = clip;
	}

}
