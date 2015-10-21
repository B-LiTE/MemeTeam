using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    // Reference to navigation agent
    NavMeshAgent navigation;

    // Position to travel to
    [SerializeField]
    Vector3 destination;

    // Reference to coroutine
    Coroutine movementCoroutine;

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
        if (References.stateManager.CurrentState == StateManager.states.realtime) movementCoroutine = StartCoroutine(checkMovement());
        else
        {
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        }
    }

    IEnumerator checkMovement()
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                // TO DO: MAKE IT SO CAMERA ROTATES
                //if (Input.mousePosition);

                RaycastHit hitInfo;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
                {
                    if (hitInfo.transform.tag == "Terrain")
                    {
                        destination = hitInfo.point;
                    }
                }
            }

            if (transform.position != destination)
                navigation.SetDestination(destination);

            yield return null;
        }
    }
}
