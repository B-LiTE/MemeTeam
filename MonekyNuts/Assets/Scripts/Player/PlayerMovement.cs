using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerStats), typeof(PlayerAttacks))]
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    PlayerBehavior playerBehavior;
    PlayerStats playerStats;
    PlayerAttacks playerAttacks;

    // Reference to navigation agent
    NavMeshAgent navigation;

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

    public Coroutine startRotating(float degrees)
    {
        return StartCoroutine(rotate(degrees));
    }

    IEnumerator rotate(float degrees)
    {
        isRotating = true;
        float t = 0;
        float current = transform.rotation.eulerAngles.y;
        while (t <= 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, Mathf.Lerp(current, current + degrees, t), transform.rotation.z));

            t += 0.1f;
            yield return null;
        }
        isRotating = false;
    }
}
