using UnityEngine;
using System.Collections;

public class EnemyAnimations : MonoBehaviour {
    
    EnemyBehavior enemyBehavior;
    private Animator anim;

    void Start()
    {
        enemyBehavior = GetComponentInParent<EnemyBehavior>();
        enemyBehavior.changeOfActions += onActionChange;
        anim = GetComponent<Animator>();
    }

    void onActionChange()
    {
        if (enemyBehavior.getAction() == EnemyBehavior.actions.moveToTarget) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);
    }

	
	// Update is called once per frame
	/*void Update () {

        anim.SetBool("Run", false);

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Run", true);
        }
        else if (GetComponentInParent<NavMeshAgent>().remainingDistance <= 1f)//(Vector3.Distance(References.player.GetComponent<NavMeshAgent>().destination, References.player.transform.position) <= References.player.GetComponent<NavMeshAgent>().stoppingDistance)
        {
            anim.SetBool("Run", false);
        }
	}*/
}
