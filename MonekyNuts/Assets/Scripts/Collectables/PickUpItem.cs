﻿using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider Player)
	{
		if (Player.CompareTag ("Player")) 
		{
			Destroy(gameObject);

			//plus add to inventory and stuff
		}
	}

	
}