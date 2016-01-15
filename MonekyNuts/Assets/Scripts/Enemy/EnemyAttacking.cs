using UnityEngine;
using System.Collections;

public class EnemyAttacking : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    Coroutine attackCoroutine;

    void Awake()
    {
        
        enemyBehavior = GetComponent<EnemyBehavior>();

        enemyBehavior.changeOfActions += onChangeAction;
    }

    void onChangeAction()
    {
        if (enemyBehavior.getAction() == EnemyBehavior.actions.attack)
        {
            // Start attacking
            attackCoroutine = StartCoroutine(attack());
        }
        else
        {
            // If we are attacking, stop attacking
            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        }
    }

    IEnumerator attack()
    {
        KillableInstance enemy = enemyBehavior.getTarget().GetComponent<KillableInstance>();
        while (true)
        {
            enemy.Damage(-1);

            yield return new WaitForSeconds(2);
        }
    }
}
