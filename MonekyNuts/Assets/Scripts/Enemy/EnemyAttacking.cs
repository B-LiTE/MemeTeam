using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior))]
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
            if (attackCoroutine == null) attackCoroutine = StartCoroutine(attack());
        }
        else
        {
            // If we are attacking, stop attacking
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator attack()
    {
        KillableInstance enemy = enemyBehavior.getTarget().GetComponent<KillableInstance>();
        while (true)
        {
            enemy.Damage(-3);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
