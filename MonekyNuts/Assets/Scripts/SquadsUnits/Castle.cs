using UnityEngine;
using System.Collections;

public class Castle : KillableInstance {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K)) Damage(-10);
        UpdateHealthBar();
	}

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
