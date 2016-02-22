using UnityEngine;
using System.Collections;

// Controls player attacking

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerStats))]
public class PlayerAttacks : MonoBehaviour {

    // References to the behavior, stats, and animations
    PlayerBehavior playerBehavior;
    PlayerStats playerStats;
    [SerializeField]
    Animations playerAnimations;

    // The target we are trying to attack
    KillableInstance attackTarget;

    // Reference to coroutine
    Coroutine attackCoroutine;

    void Awake()
    {
        // Set up references
        playerBehavior = GetComponent<PlayerBehavior>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        // Add to the change state event
        References.stateManager.changeState += onStateChange;
    }







    // Whenever we change state...
    void onStateChange()
    {
        // If we are now in strategy view...
        if (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            // Stop attacking
            changeAttack(false);
        }
    }



    // Starts or stops attacking
    public void changeAttack(bool shouldAttack)
    {
        // If we should attack...
        if (shouldAttack)
        {
            // ...and we aren't already, start attacking
            if (attackCoroutine == null) attackCoroutine = StartCoroutine(attack());
        }
        else
        {
            // Stop attacking
            stopAttacking();
        }
    }










    // Coroutine to check when to damage the target
    IEnumerator attack()
    {
        // Get the target and subscribe to its alert event
        attackTarget = playerBehavior.getTarget().GetComponent<KillableInstance>();
        attackTarget.alertOnDeath += stopAttacking;

        // As long as the target is alive...
        while (attackTarget.isAlive)
        {
            // ...if we are in range of it...
            while (inRangeOfTarget())
            {
                // Wait for a short bit
                yield return new WaitForSeconds(playerStats.secondsBetweenAttacks / 2);

                // If we can see the target...
                if (playerBehavior.seesTarget())
                {
                    // ...and we are still in range of it...
                    if (inRangeOfTarget())
                    {
                        // Start the attacking animation
                        playerAnimations.attackAnimation(true);

                        // Damage the target
                        attackTarget.Damage(playerStats.activeDamage, this.gameObject);

                        // Wait for the animation to finish
                        yield return new WaitForSeconds(playerStats.secondsBetweenAttacks / 2);

                        // Stop the attacking animation
                        playerAnimations.attackAnimation(false);
                    }
                }
                // If we can't see the target...
                else
                {
                    // Pause script execution and rotate towards the target
                    yield return playerBehavior.startRotating();
                }
            }

            yield return null;
        }
    }











    // Checks if we are in range of the target
    bool inRangeOfTarget()
    {
        // Get our position and our target's position
        Vector3 targetPosition = new Vector3(playerBehavior.getTarget().transform.position.x, 0, playerBehavior.getTarget().transform.position.z);
        Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);

        // Check if we are within attacking range
        return Vector3.Distance(currentPosition, targetPosition) <= playerStats.attackRange;
    }








    // Stops attacking
    void stopAttacking()
    {
        // Stop the animation
        playerAnimations.attackAnimation(false);

        // If we have a target...
        if (attackTarget != null)
        {
            // Unsubscribe from its alert event and set the reference to null
            attackTarget.alertOnDeath -= stopAttacking;
            attackTarget = null;
        }

        // If we were busy attacking...
        if (attackCoroutine != null)
        {
            // Stop attacking and set the reference to null
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}
