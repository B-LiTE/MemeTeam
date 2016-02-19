using UnityEngine;
using System.Collections;


public abstract class Interactive_Object : MonoBehaviour {
	
	//An object the player can hit "E" while next to to interact with
	//This class's object needs to have a collider!!!
	
	public bool canBeInteracted;
	public bool isInPlayerRange;
	bool throwOnce;
	

	public void Start()
	{
		canBeInteracted = true;
		 
		isInPlayerRange = false;
		throwOnce = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(canBeInteracted)
		{
			if (other.CompareTag("Player"))
			{	
				OnInteraction();
				isInPlayerRange = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			isInPlayerRange = false;
			throwOnce = false;
		}
		
	}
	void Update()
	{

	}
	
	public abstract void OnInteraction();
}
