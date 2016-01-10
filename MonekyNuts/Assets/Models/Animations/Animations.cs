using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Run", true);
            Debug.Log("Pressed left click.");
        }
        else
        {
            anim.SetBool("Run", false);
        }
	
	}
}
