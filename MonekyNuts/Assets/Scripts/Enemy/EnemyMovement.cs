using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior), typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    // Reference to navigation agent
    NavMeshAgent navigation;

    Coroutine checkInRangeOfTargetCoroutine;

    [SerializeField]
    float movementCutoff;

    void Awake()
    {
        // Set up references
        enemyBehavior = GetComponent<EnemyBehavior>();
        navigation = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
        enemyBehavior.changeOfActions += onChangeAction;
        enemyBehavior.changeOfIntentions += onChangeIntent;
    }









    /// <summary>
    /// Runs every time the game changes state
    /// </summary>
    void onStateChange()
    {
        //if (References.stateManager.CurrentState != StateManager.states.realtime) setDestination(transform.position);
    }






    /// <summary>
    /// Runs every time the enemy changes intentions
    /// </summary>
    void onChangeIntent()
    {
        // If we have a target we're going after...
        if (enemyBehavior.getIntent() != EnemyBehavior.intentions.wander)
        {
            // ...and we aren't already checking if we're in range...
            if (checkInRangeOfTargetCoroutine == null)
            {
                // Start checking if we're in range
                checkInRangeOfTargetCoroutine = StartCoroutine(checkInRangeOfTarget());
            }
        }
        // Otherwise, stop checking if we are in range
        else if (checkInRangeOfTargetCoroutine != null)
        {
            StopCoroutine(checkInRangeOfTargetCoroutine);
            checkInRangeOfTargetCoroutine = null;
        }
        // If we're just wandering...
        else
        {
            enemyBehavior.changeAction(EnemyBehavior.actions.moveToTarget);
        }
    }





    /// <summary>
    /// Runs every time the enemy changes actions
    /// </summary>
    void onChangeAction()
    {
        // If the action is to move...
        if (enemyBehavior.getAction() == EnemyBehavior.actions.moveToTarget)
        {
            // ...and the intent isn't to wander...
            if (enemyBehavior.getIntent() != EnemyBehavior.intentions.wander)
            {
                // ...and we aren't close enough to the target...
                if (!closeEnoughToTarget())
                    // Set our destination to the target's position
                    setDestination(enemyBehavior.getTarget().transform.position);
            }
            // Otherwise, if we aren't close enough to our destination...
            else if (!closeEnoughToDestination())
                // Set our destination
                setDestination(enemyBehavior.getDestination());
        }
    }






    /// <summary>
    /// Checks to see if the enemy is within range of its target, changing actions if necessary
    /// </summary>
    /// <returns></returns>
    IEnumerator checkInRangeOfTarget()
    {
        while (true)
        {
            // If we aren't close enough, change our action to move towards it
            if (!closeEnoughToTarget()) enemyBehavior.changeAction(EnemyBehavior.actions.moveToTarget);
            // Otherwise, if the action is currently set to move...
            else if (enemyBehavior.getAction() == EnemyBehavior.actions.moveToTarget)
            {
                // Reset our path and stop moving
                navigation.ResetPath();
                navigation.velocity = Vector3.zero;

                // Change our action to attack
                enemyBehavior.changeAction(EnemyBehavior.actions.attack);
            }

            yield return null;
        }
    }









    void setDestination(Vector3 destination)
    {
        navigation.SetDestination(destination);
    }

    /// <summary>
    /// Checks if the enemy is close enough to its target
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToTarget()
    {
        // Get the distance to our target
        float distanceToTarget = Vector3.Distance(transform.position, enemyBehavior.getTarget().transform.position);

        return distanceToTarget <= movementCutoff;
    }

    /// <summary>
    /// Checks if the enemy is close enough to its destination
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToDestination()
    {
        // Get the distance to our target
        float distanceToDestination = Vector3.Distance(transform.position, enemyBehavior.getDestination());

        return distanceToDestination <= movementCutoff;
    }
}
