using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior), typeof(NavMeshAgent), typeof(EnemyTargetSeeking))]
public class EnemyMovement : MonoBehaviour {

    EnemyBehavior enemyBehavior;
    EnemyTargetSeeking enemyTargetSeeking;

    // Reference to navigation agent
    NavMeshAgent navigation;

    [SerializeField]
    Vector3 wanderingDestination;

    Coroutine checkInRangeOfTargetCoroutine, wanderCoroutine, rotateCoroutine;

    [SerializeField]
    float movementCutoff, rotationSpeed;

    void Awake()
    {
        // Set up references
        enemyBehavior = GetComponent<EnemyBehavior>();
        navigation = GetComponent<NavMeshAgent>();
        enemyTargetSeeking = GetComponent<EnemyTargetSeeking>();
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
            // Stop wandering, if we were
            if (wanderCoroutine != null)
            {
                StopCoroutine(wanderCoroutine);
                wanderCoroutine = null;
            }

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
            // ...and we aren't already wandering, start wandering
            if (wanderCoroutine == null) wanderCoroutine = StartCoroutine(wander());
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
            rotateCoroutine = null;
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
                setDestination(wanderingDestination);
        }
        // If the action is to rotate...
        else if (enemyBehavior.getAction() == EnemyBehavior.actions.rotate)
        {
            // Start rotating if we aren't already
            if (rotateCoroutine == null) rotateCoroutine = StartCoroutine(rotate());
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







    /// <summary>
    /// Makes the enemy wander by picking a point within vision, travelling to it, rotating, and repeating
    /// </summary>
    /// <returns></returns>
    IEnumerator wander()
    {
        while (true)
        {
            // Pick a random point within our vision
            int i = 0;
            do
            {
                wanderingDestination = enemyTargetSeeking.pickRandomPointInSight();
                while (navigation.pathPending) yield return null;
                if (i++ > 50) break;
            } while (navigation.pathStatus != NavMeshPathStatus.PathComplete);
            Debug.Log("took " + i + " + long");
            Debug.DrawLine(transform.position, wanderingDestination, Color.black, 5f);

            // Start traveling to it
            enemyBehavior.changeAction(EnemyBehavior.actions.moveToTarget);

            // Wait for enemy to finish traveling
            while (!closeEnoughToDestination()) yield return null;

            // Idle for a short time
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));

            // Rotate in a random direction
            enemyBehavior.changeAction(EnemyBehavior.actions.rotate);
            yield return rotateCoroutine;

            // Idle for a short time
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        }
    }









    /// <summary>
    /// Rotates in a random direction a random number of degrees
    /// </summary>
    /// <returns></returns>
    IEnumerator rotate(float degrees = 0)
    {
        float startingRotation = transform.rotation.eulerAngles.y;

        float endingRotation;
        if (degrees == 0) endingRotation = startingRotation + Random.Range(-360f, 360f);
        else endingRotation = degrees;

        float time = 0;
        while (time <= 1)
        {
            float newRotation = Mathf.LerpAngle(startingRotation, endingRotation, time);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z));

            yield return null;

            time += (0.01f * rotationSpeed);
        }
    }











    void setDestination(Vector3 destination)
    {
        navigation.SetDestination(destination);
        Debug.Log("path status: " + navigation.pathStatus);
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
        float distanceToDestination = Vector3.Distance(transform.position, wanderingDestination);

        return distanceToDestination <= movementCutoff;
    }
}
