using UnityEngine;
using System.Collections;

// Controls player movement

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerStats), typeof(PlayerAttacks))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    // References to other scripts
    PlayerBehavior playerBehavior;
    PlayerStats playerStats;
    PlayerAttacks playerAttacks;

    // Reference to navigation agent
    public NavMeshAgent navigation;

    // Reference to coroutine
    Coroutine followTargetCoroutine;
    Coroutine rotationCoroutine;

    // Distance to stop moving towards target
    [SerializeField]
    float cutoffDistance;

    // Whether we are rotating
    bool isRotating = false;

    void Awake()
    {
        // Set up references
        playerBehavior = GetComponent<PlayerBehavior>();
        playerStats = GetComponent<PlayerStats>();
        playerAttacks = GetComponent<PlayerAttacks>();
        navigation = GetComponent<NavMeshAgent>();

        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        // If we are in strategy view, stop moving or following our target
        if (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            stopMoving();
            stopFollowTargetCoroutine();
        }
    }










    // Goes to a specific destination in the world
    public void goTo(Vector3 destination)
    {
        if (playerBehavior.targetIsEnemy())
        {
            followTargetCoroutine = StartCoroutine(followTarget());
        }
        else
        {
            stopFollowTargetCoroutine();

            navigation.SetDestination(zeroedYVector(destination));
        }
    }

    // Follow a moving target
    IEnumerator followTarget()
    {
        // Set the target we're following
        KillableInstance attackTarget = playerBehavior.getTarget().GetComponent<KillableInstance>();

        // As long as the target is still alive...
        while (attackTarget.isAlive)
        {
            // ...and while we aren't in range of the target...
            while (!inRangeOfTarget())
            {
                // Move to the enemy
                navigation.SetDestination(playerBehavior.getTarget().transform.position);

                yield return new WaitForSeconds(0.25f);
            }

            // Since we are now in range of the target, stop moving
            stopMoving();

            yield return null;
        }
    }












    // Stop moving and reset the pathfinding system
    void stopMoving()
    {
        navigation.SetDestination(transform.position);
        navigation.velocity = Vector3.zero;
    }

    // Stop the follow target coroutine
    void stopFollowTargetCoroutine()
    {
        if (followTargetCoroutine != null)
        {
            StopCoroutine(followTargetCoroutine);
            followTargetCoroutine = null;
        }
    }

    // Check if the distance between us and our target is less than our attacking range
    bool inRangeOfTarget()
    {
        return Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(playerBehavior.getTarget().transform.position)) <= 2 * playerStats.attackRange / 3;
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












    // Public method to access

    public Coroutine startRotating()
    {
        return StartCoroutine(rotate());
    }

    // Rotate to face our target
    IEnumerator rotate()
    {
        // Get our starting and ending rotations
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(playerBehavior.getTarget().transform.position - transform.position);

        isRotating = true;
        float t = 0;
        while (t <= 1)
        {
            // Gradually rotate between the start and end points
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            t += 0.1f;
            yield return null;
        }
        isRotating = false;
    }
}
