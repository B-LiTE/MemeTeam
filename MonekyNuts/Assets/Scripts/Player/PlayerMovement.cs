using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    PlayerCameraRotation realtimeCamera;

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
        realtimeCamera = References.realtimeCamera.GetComponent<PlayerCameraRotation>();
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
            if (!realtimeCamera.rotating && Input.GetMouseButtonUp(0))
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(References.realtimeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
                {
                    if (hitInfo.transform.tag == "Terrain")
                    {
                        destination = hitInfo.point;
                        navigation.SetDestination(destination);
                    }
                }
            }

            yield return null;
        }
    }
}
