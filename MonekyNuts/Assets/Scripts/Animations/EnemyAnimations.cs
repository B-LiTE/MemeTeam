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
        if (enemyBehavior.getAction() == EnemyBehavior.actions.move) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);

        if (enemyBehavior.getAction() == EnemyBehavior.actions.attack) anim.SetBool("Punch", true);
        else anim.SetBool("Punch", false);
    }

    IEnumerator attack()
    {
        anim.SetBool("Punch", true);
        yield return new WaitForSeconds(0.75f);
        anim.SetBool("Punch", false);
    }
}
