using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerStats), typeof(PlayerAttacks))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    PlayerBehavior playerBehavior;
    PlayerStats playerStats;
    PlayerAttacks playerAttacks;

    // Reference to navigation agent
    public NavMeshAgent navigation;

    // Reference to coroutine
    Coroutine followTargetCoroutine;

    [SerializeField]
    float cutoffDistance;

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
        if (References.stateManager.CurrentState != StateManager.states.realtime)
        {
            stopMoving();
            stopFollowTargetCoroutine();
        }
    }

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

    IEnumerator followTarget()
    {
        KillableInstance attackTarget = playerBehavior.getTarget().GetComponent<KillableInstance>();

        while (attackTarget.isAlive)
        {
            while (!inRangeOfTarget())
            {
                navigation.SetDestination(playerBehavior.getTarget().transform.position);

                yield return new WaitForSeconds(0.25f);
            }

            stopMoving();

            yield return null;
        }
    }

    void stopMoving()
    {
        navigation.SetDestination(transform.position);
        navigation.velocity = Vector3.zero;
    }

    void stopFollowTargetCoroutine()
    {
        if (followTargetCoroutine != null)
        {
            StopCoroutine(followTargetCoroutine);
            followTargetCoroutine = null;
        }
    }

    bool inRangeOfTarget()
    {
        return Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(playerBehavior.getTarget().transform.position)) <= playerStats.attackRange;
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




    bool isRotating = false;
    Coroutine rotationCoroutine;

    public Coroutine startRotating()
    {
        return StartCoroutine(rotate());
    }

    IEnumerator rotate()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.LookRotation(playerBehavior.getTarget().transform.position - transform.position);

        isRotating = true;
        float t = 0;
        while (t <= 1)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            t += 0.1f;
            yield return null;
        }
        isRotating = false;
    }
}
