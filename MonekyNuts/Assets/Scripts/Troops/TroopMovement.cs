using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TroopBehavior), typeof(TroopStats), typeof(TroopTargetSeeking))]
[RequireComponent(typeof(NavMeshAgent))]
public class TroopMovement : MonoBehaviour {

    TroopBehavior troopBehavior;
    TroopStats troopStats;
    TroopTargetSeeking troopTargetSeeking;

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
        troopBehavior = GetComponent<TroopBehavior>();
        troopStats = GetComponent<TroopStats>();
        navigation = GetComponent<NavMeshAgent>();
        troopTargetSeeking = GetComponent<TroopTargetSeeking>();

        References.stateManager.changeState += onStateChange;
        troopBehavior.changeOfActions += onChangeAction;
        troopBehavior.changeOfIntentions += onChangeIntent;
        troopTargetSeeking.onTargetVisible += onTargetVisible;
        troopBehavior.onTroopDeath += onDeath;
    }

    void onDeath()
    {
        References.stateManager.changeState -= onStateChange;
    }









    /// <summary>
    /// Runs every time the game changes state
    /// </summary>
    void onStateChange()
    {
        if (References.stateManager.CurrentState != StateManager.states.realtime)
        {
            stopMoving();
        }
    }






    /// <summary>
    /// Runs every time the troop changes intentions
    /// </summary>
    void onChangeIntent()
    {
        // If we have an enemy we're going after...
        if (troopBehavior.getIntent() == TroopBehavior.intentions.attackEnemy)
        {
            // Stop wandering, if we were
            stopWanderCoroutine();

            troopBehavior.getTarget().GetComponent<KillableInstance>().alertOnDeath += stopCheckInRangeOfTargetCoroutine;

            // Start checking if we're in range
            startCheckInRangeOfTargetCoroutine();
        }
        // If we have a collectible to gather...
        else if (troopBehavior.getIntent() == TroopBehavior.intentions.getCollectible)
        {
            // Stop wandering, if we were
            stopWanderCoroutine();

            // Stop checking if we're in range
            stopCheckInRangeOfTargetCoroutine();

            troopBehavior.changeAction(TroopBehavior.actions.move);
        }
        // If we're protecting the castle...
        else if (troopBehavior.getIntent() == TroopBehavior.intentions.protectCastle)
        {

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
    /// Runs every time the troop changes actions
    /// </summary>
    void onChangeAction()
    {
        // If the action is to move...
        if (troopBehavior.getAction() == TroopBehavior.actions.move)
        {
            // Stop rotating if we are already
            stopRotateCoroutine();

            // If we have an enemy to follow...
            if (troopBehavior.getIntent() == TroopBehavior.intentions.attackEnemy)
            {
                // ...and we aren't close enough to the target...
                if (!closeEnoughToTarget())
                    // Set our destination to the target's position
                    setDestination(troopBehavior.getTarget().transform.position);
            }


            // If we have a collectible to get...
            else if (troopBehavior.getIntent() == TroopBehavior.intentions.getCollectible)
            {
                setDestination(troopBehavior.getTarget().transform.position);
            }


            // Otherwise, if we aren't close enough to our destination...
            else if (!closeEnoughToDestination())
                // Set our destination
                setDestination(wanderingDestination);
        }
        // If the action is to rotate...
        else if (troopBehavior.getAction() == TroopBehavior.actions.rotate)
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
    /// Checks to see if the troop is within range of its target, changing actions if necessary
    /// </summary>
    /// <returns></returns>
    IEnumerator checkInRangeOfTarget()
    {
        while (true)
        {
            // If the troop can see the target...
            if (troopTargetSeeking.targetVisible)
            {
                while (troopTargetSeeking.targetVisible)
                {
                    // As long as we aren't in range of the target...
                    while (!closeEnoughToTarget() && troopTargetSeeking.targetVisible)
                    {
                        // Change our action to move towards it
                        troopBehavior.changeAction(TroopBehavior.actions.move);

                        yield return new WaitForSeconds(0.25f);
                    }

                    if (!troopTargetSeeking.targetVisible) break;

                    // Now that we are close enough, stop moving and begin attacking
                    stopMoving();
                    troopBehavior.changeAction(TroopBehavior.actions.attack);

                    yield return new WaitForSeconds(0.03f);
                }
            }
            // If the troop can't see the target...
            else
            {
                // Let the troop get close enough to where the target was before continuing
                if (closeEnoughToNavigationDestination())
                {
                    // Since we are at the location that the target was and we still don't see them, stop moving
                    stopMoving();

                    for (int i = 0; i < 3; i++)
                    {
                        // Rotate to try and find it
                        troopBehavior.changeAction(TroopBehavior.actions.rotate);
                        yield return rotateCoroutine;

                        // Idle for a short time
                        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                    }

                    // If we rotated three times and still couldn't find it, give up and start wandering
                    troopBehavior.changeIntent(transform.gameObject);
                }
            }

            yield return new WaitForSeconds(0.03f);
        }
    }







    /// <summary>
    /// Makes the troop wander by picking a point within vision, traveling to it, rotating, and repeating
    /// </summary>
    /// <returns></returns>
    IEnumerator wander()
    {
        while (true)
        {
            // Pick a random point within our vision
            wanderingDestination = troopTargetSeeking.pickRandomPointInSight();
            Debug.DrawLine(transform.position, wanderingDestination, Color.black, 5f);

            // Start traveling to it
            troopBehavior.changeAction(TroopBehavior.actions.move);

            // Wait for troop to finish traveling
            while (!closeEnoughToDestination()) yield return null;

            // Idle for a short time
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));

            // Rotate in a random direction
            troopBehavior.changeAction(TroopBehavior.actions.rotate);
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
        navigation.SetDestination(transform.position);
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
    /// Checks if the troop is close enough to its target
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToTarget()
    {
        // Get the distance to our target
        float distanceToTarget = Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(troopBehavior.getTarget().GetComponent<Collider>().bounds.ClosestPoint(transform.position)));

        if (troopBehavior.getIntent() != TroopBehavior.intentions.wander) return distanceToTarget <= troopStats.attackRange;
        else return distanceToTarget <= movementCutoff;
    }



    /// <summary>
    /// Checks if the troop is close enough to its destination
    /// </summary>
    /// <returns>Returns true if close enough, false if not</returns>
    bool closeEnoughToDestination()
    {
        // Get the distance to our target
        float distanceToDestination = Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(wanderingDestination));

        return distanceToDestination <= movementCutoff;
    }




    bool closeEnoughToNavigationDestination()
    {
        return Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(navigation.destination)) <= movementCutoff;
        //return navigation.remainingDistance <= movementCutoff;
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
