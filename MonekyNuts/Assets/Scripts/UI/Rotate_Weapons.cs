﻿using UnityEngine;
using System.Collections;

public class Rotate_Weapons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void RotateWeapons()
	{
        transform.Rotate(new Vector3(0,0,-30));
	}
}
