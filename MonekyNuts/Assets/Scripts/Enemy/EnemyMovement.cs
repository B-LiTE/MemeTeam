using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    // Reference to navigation agent
    NavMeshAgent navigation;

    // DEBUG - Position to travel to
    [SerializeField]
    Vector3 destination;

    void Awake()
    {
        // Set up references
        navigation = GetComponent<NavMeshAgent>();
        destination = transform.position;
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        if (References.stateManager.CurrentState != StateManager.states.realtime) setDestination(transform.position);
    }

    void setDestination(Vector3 destination)
    {
        this.destination = destination;
        navigation.SetDestination(destination);
    }
}
