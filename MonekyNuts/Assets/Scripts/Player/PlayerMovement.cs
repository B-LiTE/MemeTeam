using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBehavior), typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {

    PlayerBehavior playerBehavior;

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
        navigation = GetComponent<NavMeshAgent>();
    }

    public void goTo(Vector3 destination)
    {
        if (playerBehavior.targetIsEnemy())
        {
            if (followTargetCoroutine == null) followTargetCoroutine = StartCoroutine(followTarget());
        }
        else
        {
            if (followTargetCoroutine != null)
            {
                StopCoroutine(followTargetCoroutine);
                followTargetCoroutine = null;
            }

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

            yield return null;
        }
    }

    bool inRangeOfTarget()
    {
        return Vector3.Distance(zeroedYVector(transform.position), zeroedYVector(playerBehavior.getTarget().transform.position)) <= cutoffDistance;
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
