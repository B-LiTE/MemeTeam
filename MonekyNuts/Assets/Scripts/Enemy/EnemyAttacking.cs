using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyAttacking : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    Coroutine attackCoroutine;

    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();

        References.stateManager.changeState += onStateChange;
        enemyBehavior.changeOfActions += onChangeAction;
        //enemyBehavior.onEnemyDeath += onDeath;
    }

    void onDeath()
    {
        //References.stateManager.changeState -= onStateChange;
    }

    void onStateChange()
    {
        // If we are attacking, stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    void onChangeAction()
    {
        if (enemyBehavior.getAction() == EnemyBehavior.actions.attack)
        {
            // Start attacking if we aren't already
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
        KillableInstance attackTarget;
        if (enemyBehavior.targetIsPlayer()) attackTarget = References.player.GetComponent<PlayerStats>();
        else if (enemyBehavior.targetIsCastle()) attackTarget = References.castle.GetComponent<Castle>();
        else attackTarget = enemyBehavior.getTarget().GetComponent<KillableInstance>();

        attackTarget.alertOnDeath += stopAttacking;

        while (attackTarget.isAlive)
        {
            yield return new WaitForSeconds(1.5f);
            
            attackTarget.Damage(-3);
        }

        enemyBehavior.changeIntent(this.gameObject);
    }

    void stopAttacking()
    {
        // Stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
