using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

    public string collectableType;

   
	void OnTriggerEnter(Collider Player)
	{
		if (Player.CompareTag ("Player")) 
		{
            if (collectableType == "gold")
            {
                FindObjectOfType<GameStats>().GetComponent<GameStats>().ChangeGoldCount(1);
            }
			Destroy(transform.gameObject);

			//plus add to inventory and stuff
		}
	}

	
}
