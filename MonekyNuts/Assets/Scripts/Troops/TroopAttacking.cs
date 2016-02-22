using UnityEngine;
using System.Collections;

// Controls troop attacking

[RequireComponent(typeof(TroopBehavior), typeof(TroopStats))]
public class TroopAttacking : MonoBehaviour {

    // References to other scripts
    TroopBehavior troopBehavior;
    TroopStats troopStats;

    // Attacking coroutine
    Coroutine attackCoroutine;

    // Reference to target we are attacking
    KillableInstance attackTarget;

    void Awake()
    {
        // Set up references
        troopBehavior = GetComponent<TroopBehavior>();
        troopStats = GetComponent<TroopStats>();

        // Subscribe to appropriate events
        References.stateManager.changeState += onStateChange;
        troopBehavior.changeOfActions += onChangeAction;
        troopBehavior.onTroopDeath += onDeath;
    }









    // When we die, stop caring about our current state
    void onDeath()
    {
        References.stateManager.changeState -= onStateChange;
    }

    void onStateChange()
    {
        // If we are attacking, stop attacking
        stopAttacking();
    }

    void onChangeAction()
    {
        if (troopBehavior.getAction() == TroopBehavior.actions.attack)
        {
            // Start attacking if we aren't already
            if (attackCoroutine == null) attackCoroutine = StartCoroutine(attack());
        }
        else
        {
            stopAttacking();
        }
    }







    // Coroutine to start attacking
    IEnumerator attack()
    {
        // Get the target object
        attackTarget = troopBehavior.getTarget().GetComponent<EnemyStats>();

        // Subscribe to the target's death alert
        attackTarget.alertOnDeath += stopAttacking;

        // As long as the target is alive, wait for a short bit, then attack
        while (attackTarget.isAlive)
        {
            yield return new WaitForSeconds(troopStats.secondsBetweenAttacks / 2);

            attackTarget.Damage(troopStats.attackValue, this.gameObject);

            yield return new WaitForSeconds(troopStats.secondsBetweenAttacks / 2);
        }

        // Now that the target is dead, start wandering
        troopBehavior.changeIntent(this.gameObject);
    }








    // Stop all attacking
    void stopAttacking()
    {
        // If we are attacking, stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        // If we have a target, unsubscribe from their death event and set them to null
        if (attackTarget != null)
        {
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }
    }
}
