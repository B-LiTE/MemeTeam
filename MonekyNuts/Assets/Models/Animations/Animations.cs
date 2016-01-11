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

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Run", true);
        }
        else if (References.player.GetComponent<NavMeshAgent>().remainingDistance <= 1f)//(Vector3.Distance(References.player.GetComponent<NavMeshAgent>().destination, References.player.transform.position) <= References.player.GetComponent<NavMeshAgent>().stoppingDistance)
        {
            anim.SetBool("Run", false);
        }
	
	}
}
