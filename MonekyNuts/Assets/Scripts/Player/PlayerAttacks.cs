﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerStats))]
public class PlayerAttacks : MonoBehaviour {

    PlayerBehavior playerBehavior;
    PlayerStats playerStats;

    KillableInstance attackTarget;

    // Reference to coroutine
    Coroutine attackCoroutine;

    void Awake()
    {
        playerBehavior = GetComponent<PlayerBehavior>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }







    void onStateChange()
    {
        if (References.stateManager.CurrentState != StateManager.states.realtime)
        {
            changeAttack(false);
        }
    }



    public void changeAttack(bool shouldAttack)
    {
        if (shouldAttack)
        {
            if (attackCoroutine == null) attackCoroutine = StartCoroutine(attack());
        }
        else
        {
            stopAttacking();
        }
    }










    IEnumerator attack()
    {
        attackTarget = playerBehavior.getTarget().GetComponent<KillableInstance>();
        attackTarget.alertOnDeath += stopAttacking;

        while (attackTarget.isAlive)
        {
            while (inRangeOfTarget())
            {
                yield return new WaitForSeconds(playerStats.secondsBetweenAttacks);
                if (inRangeOfTarget()) attackTarget.Damage(playerStats.activeDamage);
            }

            yield return null;
        }
    }











    bool inRangeOfTarget()
    {
        Vector3 targetPosition = new Vector3(playerBehavior.getTarget().transform.position.x, 0, playerBehavior.getTarget().transform.position.z);
        Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);

        return Vector3.Distance(currentPosition, targetPosition) <= playerStats.attackRange;
    }








    void stopAttacking()
    {
        if (attackTarget != null)
        {
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
