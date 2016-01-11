using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

    public string collectableType;
	public int itemId;

   
	void OnTriggerEnter(Collider Player)
	{
		if (Player.CompareTag ("Player")) 
		{
			/*
            if (collectableType == "gold")
            {
                FindObjectOfType<GameStats>().GetComponent<GameStats>().ChangeGoldCount(1);
            }*/
			FindObjectOfType<Inventory>().PickUpDropItem(gameObject);

			//plus add to inventory and stuff
		}
	}

	
}
