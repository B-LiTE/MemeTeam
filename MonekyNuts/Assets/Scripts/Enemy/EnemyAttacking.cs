using UnityEngine;
using System.Collections;

// Controls enemy attacking

[RequireComponent(typeof(EnemyBehavior), typeof(EnemyStats))]
public class EnemyAttacking : MonoBehaviour {

    // References to other scripts
    EnemyBehavior enemyBehavior;
    EnemyStats enemyStats;

    // Attacking coroutine
    Coroutine attackCoroutine;

    // Reference to target we are attacking
    [SerializeField]
    KillableInstance attackTarget;

    void Awake()
    {
        // Set up references
        enemyBehavior = GetComponent<EnemyBehavior>();
        enemyStats = GetComponent<EnemyStats>();

        // Subscribe to appropriate events
        References.stateManager.changeState += onStateChange;
        enemyBehavior.changeOfActions += onChangeAction;
        enemyBehavior.onEnemyDeath += onDeath;
    }









    // When we die, stop caring about our current state
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
            stopAttacking();
        }
    }








    // Coroutine to control attacking
    IEnumerator attack()
    {
        // Get the appropriate target object
        if (enemyBehavior.targetIsPlayer()) attackTarget = References.player.GetComponent<PlayerStats>();
        else if (enemyBehavior.targetIsCastle()) attackTarget = References.castle.GetComponent<Castle>();
        else attackTarget = enemyBehavior.getTarget().GetComponent<KillableInstance>();

        // Subscribe to the target's death alert
        attackTarget.alertOnDeath += stopAttacking;

        // As long as the target is alive, wait for a short bit, then attack
        while (attackTarget.isAlive)
        {
            yield return new WaitForSeconds(enemyStats.secondsBetweenAttacks / 2);

            attackTarget.Damage(enemyStats.attackValue, this.gameObject);

            yield return new WaitForSeconds(enemyStats.secondsBetweenAttacks / 2);
        }

        // Now that the target is dead, start wandering
        enemyBehavior.changeIntent(this.gameObject);

        stopAttacking();
    }









    // Stop all attacking
    void stopAttacking()
    {
        // If we have a target, unsubscribe from their death event and set them to null
        if (attackTarget != null)
        {
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }

        // If we are attacking, stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
