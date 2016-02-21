using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TroopBehavior), typeof(TroopStats))]
public class TroopAttacking : MonoBehaviour {

    TroopBehavior troopBehavior;
    TroopStats troopStats;

    Coroutine attackCoroutine;

    KillableInstance attackTarget;

    void Awake()
    {
        troopBehavior = GetComponent<TroopBehavior>();
        troopStats = GetComponent<TroopStats>();

        References.stateManager.changeState += onStateChange;
        troopBehavior.changeOfActions += onChangeAction;
        troopBehavior.onTroopDeath += onDeath;
    }









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








    IEnumerator attack()
    {
        attackTarget = troopBehavior.getTarget().GetComponent<EnemyStats>();

        attackTarget.alertOnDeath += stopAttacking;

        while (attackTarget.isAlive)
        {
            yield return new WaitForSeconds(troopStats.secondsBetweenAttacks / 2);

            attackTarget.Damage(troopStats.attackValue, this.gameObject);

            yield return new WaitForSeconds(troopStats.secondsBetweenAttacks / 2);
        }

        troopBehavior.changeIntent(this.gameObject);
    }









    void stopAttacking()
    {

        // Stop attacking
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        if (attackTarget != null)
        {
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }
    }
}
