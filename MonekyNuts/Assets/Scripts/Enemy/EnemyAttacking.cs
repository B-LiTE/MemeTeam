using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior), typeof(EnemyStats))]
public class EnemyAttacking : MonoBehaviour {

    EnemyBehavior enemyBehavior;
    EnemyStats enemyStats;

    Coroutine attackCoroutine;

    KillableInstance attackTarget;

    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        enemyStats = GetComponent<EnemyStats>();

        References.stateManager.changeState += onStateChange;
        enemyBehavior.changeOfActions += onChangeAction;
        enemyBehavior.onEnemyDeath += onDeath;
    }









    void onDeath()
    {
        References.stateManager.changeState -= onStateChange;
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
        if (enemyBehavior.targetIsPlayer()) attackTarget = References.player.GetComponent<PlayerStats>();
        else if (enemyBehavior.targetIsCastle()) attackTarget = References.castle.GetComponent<Castle>();
        else attackTarget = enemyBehavior.getTarget().GetComponent<KillableInstance>();

        attackTarget.alertOnDeath += stopAttacking;

        while (attackTarget.isAlive)
        {
            yield return new WaitForSeconds(enemyStats.secondsBetweenAttacks);
            
            attackTarget.Damage(enemyStats.attackValue);
        }

        enemyBehavior.changeIntent(this.gameObject);
    }









    void stopAttacking()
    {
        if (attackTarget != null)
        {
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }

        // Stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
