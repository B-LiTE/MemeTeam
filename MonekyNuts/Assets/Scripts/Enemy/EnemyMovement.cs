using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior), typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    // Reference to navigation agent
    NavMeshAgent navigation;

    Coroutine checkInRangeCoroutine;

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

    void onStateChange()
    {
        //if (References.stateManager.CurrentState != StateManager.states.realtime) setDestination(transform.position);
    }

    void onChangeIntent()
    {
        if (enemyBehavior.getIntent() != EnemyBehavior.intentions.wander) checkInRangeCoroutine = StartCoroutine(checkInRange());
        else if (checkInRangeCoroutine != null) StopCoroutine(checkInRangeCoroutine);
    }

    void onChangeAction()
    {
        if (enemyBehavior.getAction() == EnemyBehavior.actions.moveToTarget)
        {
            //                                                                  vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
            //                                                              Vector3.distance between the two within a threshold <- (need to define!!)
            if (enemyBehavior.getIntent() != EnemyBehavior.intentions.wander && navigation.destination != enemyBehavior.getTarget().transform.position) setDestination(enemyBehavior.getTarget().transform.position);
            else if (navigation.destination != enemyBehavior.getDestination()) setDestination(enemyBehavior.getDestination());
        }
    }

    IEnumerator checkInRange()
    {
        while (true)
        {
            float distanceToTarget = Vector3.Distance(transform.position, enemyBehavior.getTarget().transform.position);
            if (enemyBehavior.getTarget() != null && distanceToTarget > 2f) enemyBehavior.changeAction(EnemyBehavior.actions.moveToTarget);
            else if (enemyBehavior.getAction() == EnemyBehavior.actions.moveToTarget && distanceToTarget <= 2f) enemyBehavior.changeAction(EnemyBehavior.actions.attack);

            yield return new WaitForEndOfFrame();
        }
    }

    void setDestination(Vector3 destination)
    {
        navigation.SetDestination(destination);
    }
}
