using UnityEngine;
using System.Collections;

public class EnemyAnimations : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Run", false);

        /*if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Run", true);
        }
        else if (References.enemy.GetComponent<NavMeshAgent>().remainingDistance <= 1f)//(Vector3.Distance(References.player.GetComponent<NavMeshAgent>().destination, References.player.transform.position) <= References.player.GetComponent<NavMeshAgent>().stoppingDistance)
        {
            anim.SetBool("Run", false);
        }
	*/
	}
}
