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
            // If we are not rotating and the mouse button was let go...
            if (!realtimeCamera.rotating && Input.GetMouseButtonUp(0))
            {
                // ...and we hit an object within range...
                RaycastHit hitInfo;
                if (Physics.Raycast(References.realtimeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
                {
                    // ...and we are allowed to travel to the object...
                    if (canTravelTo(hitInfo.transform.tag))
                    {
                        // Set the destination to be the clicked point
                        destination = hitInfo.point;
                        navigation.SetDestination(destination);
                    }
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// Checks if the clicked on object can be travelled to
    /// </summary>
    /// <param name="tag">the tag of the clicked object</param>
    /// <returns>Returns true if player can travel to it, false if not</returns>
    bool canTravelTo(string tag)
    {
        switch (tag)
        {
            case "Terrain":
            case "Enemy":
            case "Collectible":
                return true;

            default:
                return false;
        }
    }
}
