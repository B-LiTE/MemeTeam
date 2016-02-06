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

        References.stateManager.changeState += onStateChange;
        enemyBehavior.changeOfActions += onChangeAction;
        enemyBehavior.changeOfIntentions += onChangeIntent;
        enemyTargetSeeking.onTargetVisible += onTargetVisible;
    }

    void Start()
    {
        // Pretend as if we've changed intentions so that we check whether it changed on startup
        //onChangeIntent();
        //onChangeAction();
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
            stopWanderCoroutine();

            // Start checking if we're in range
            startCheckInRangeOfTargetCoroutine();
        }
        // If we're just wandering...
        else
        {
            // Stop checking if we're in range
            stopCheckInRangeOfTargetCoroutine();

            // Start wandering
            startWanderCoroutine();
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
            // Stop rotating if we are already
            stopRotateCoroutine();

            // If we have a target to follow...
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
            // Stop moving if we were
            stopMoving();

            // Stop rotating, if we were, and begin rotating
            runRotateCoroutine();
        }
    }







    /// <summary>
    /// Runs when "onTargetVisible" event in "EnemyTargetSeeking" activates.
    /// </summary>
    void onTargetVisible()
    {
        // If we can see our target, start checking if we're in range
        runCheckInRangeOfTargetCoroutine();
    }







    /// <summary>
    /// Checks to see if the enemy is within range of its target, changing actions if necessary
    /// </summary>
    /// <returns></returns>
    IEnumerator checkInRangeOfTarget()
    {
        while (true)
        {
            // If the enemy can see the target...
            if (enemyTargetSeeking.targetVisible)
            {
                // ...and we aren't close enough, change our action to move towards it
                if (!closeEnoughToTarget()) enemyBehavior.changeAction(EnemyBehavior.actions.moveToTarget);
                // Otherwise, since we ARE close enough...
                else
                {
                    // Stop moving
                    stopMoving();

                    // Change our action to attack
                    enemyBehavior.changeAction(EnemyBehavior.actions.attack);
                }
            }
            // If the enemy can't see the target...
            else
            {
                // Stop moving
                stopMoving();

                for (int i = 0; i < 3; i++)
                {
                    // Rotate to try and find it
                    enemyBehavior.changeAction(EnemyBehavior.actions.rotate);
                    yield return rotateCoroutine;

                    // Idle for a short time
                    yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                }

                // If we rotated three times and still couldn't find it, give up and start wandering
                enemyBehavior.changeIntent(transform.gameObject);
            }

            yield return new WaitForSeconds(0.03f);
        }
    }







    /// <summary>
    /// Makes the enemy wander by picking a point within vision, traveling to it, rotating, and repeating
    /// </summary>
    /// <returns></returns>
    IEnumerator wander()
    {
        while (true)
        {
            // Pick a random point within our vision
            wanderingDestination = enemyTargetSeeking.pickRandomPointInSight();
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
    /// Rotates once in a random direction a random number of degrees, stopping if looking for a target and finding it
    /// </summary>
    /// <returns></returns>
    IEnumerator rotate()
    {
        float startingRotation = transform.rotation.eulerAngles.y;
        float endingRotation = startingRotation + Random.Range(-360f, 360f);

        float time = 0;
        while (time <= 1)
        {
            float newRotation = Mathf.LerpAngle(startingRotation, endingRotation, time);
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, newRotation, transform.rotation.eulerAngles.z));

            yield return null;

            time += (0.01f * rotationSpeed);
        }
    }











    void stopMoving()
    {
        navigation.ResetPath();
        navigation.velocity = Vector3.zero;
    }













    // The "startCoroutine" methods start the coroutine if it ISN'T already started.
    // The "stopCoroutine" methods stop the coroutine if it IS already started.
    // The "runCoroutine" will call the "stopCoroutine" and then the "startCoroutine".

    void stopRotateCoroutine() { if (rotateCoroutine != null) { StopCoroutine(rotateCoroutine); rotateCoroutine = null; } }
    void startRotateCoroutine() { if (rotateCoroutine == null) rotateCoroutine = StartCoroutine(rotate()); }
    void runRotateCoroutine() { stopRotateCoroutine(); startRotateCoroutine(); }


    void stopWanderCoroutine() { if (wanderCoroutine != null) { StopCoroutine(wanderCoroutine); wanderCoroutine = null; } }
    void startWanderCoroutine() { if (wanderCoroutine == null) wanderCoroutine = StartCoroutine(wander()); }
    void runWanderCoroutine() { stopWanderCoroutine(); startWanderCoroutine(); }


    void stopCheckInRangeOfTargetCoroutine() { if (checkInRangeOfTargetCoroutine != null) { StopCoroutine(checkInRangeOfTargetCoroutine); checkInRangeOfTargetCoroutine = null; } }
    void startCheckInRangeOfTargetCoroutine() { if (checkInRangeOfTargetCoroutine == null) checkInRangeOfTargetCoroutine = StartCoroutine(checkInRangeOfTarget()); }
    void runCheckInRangeOfTargetCoroutine() { stopCheckInRangeOfTargetCoroutine(); startCheckInRangeOfTargetCoroutine(); }














    void setDestination(Vector3 destination)
    {
        navigation.SetDestination(zeroedYVector(destination));
    }



    /// <summary>
    /// Checks if the enemy is close enough to its target
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToTarget()
    {
        // Get the distance to our target
        float distanceToTarget = Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(enemyBehavior.getTarget().GetComponent<Collider>().bounds.ClosestPoint(transform.position)));

        return distanceToTarget <= movementCutoff;
    }



    /// <summary>
    /// Checks if the enemy is close enough to its destination
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToDestination()
    {
        // Get the distance to our target
        float distanceToDestination = Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(wanderingDestination));

        return distanceToDestination <= movementCutoff;
    }









    /// <summary>
    /// Returns a copy of a vector with the Y field zeroed out
    /// </summary>
    /// <param name="vector">The vector to zero</param>
    /// <returns></returns>
    Vector3 zeroedYVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
