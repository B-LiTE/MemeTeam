﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerAttacks : MonoBehaviour {

    PlayerBehavior playerBehavior;

    // Range to attack from
    public int attackRange;

    [SerializeField]
    float secondsBetweenAttacks;

    // Reference to coroutine
    Coroutine attackCoroutine;

    void Awake()
    {
        playerBehavior = GetComponent<PlayerBehavior>();
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
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator attack()
    {
        KillableInstance attackTarget = playerBehavior.getTarget().GetComponent<KillableInstance>();
        attackTarget.alertOnDeath += stopAttacking;

        while (attackTarget.isAlive)
        {
            while (inRangeOfTarget())
            {
                yield return new WaitForSeconds(secondsBetweenAttacks);
                
                if (inRangeOfTarget()) attackTarget.Damage(-5);
            }

            yield return null;
        }
    }

    bool inRangeOfTarget()
    {
        Vector3 targetPosition = new Vector3(playerBehavior.getTarget().transform.position.x, 0, playerBehavior.getTarget().transform.position.z);
        Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);

        return Vector3.Distance(currentPosition, targetPosition) <= attackRange;
    }

    void stopAttacking()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
