using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerMouseCommands : MonoBehaviour {

    PlayerBehavior playerBehavior;

    PlayerCameraRotation realtimeCamera;

    EventSystem events;

    // Reference to coroutine
    Coroutine mouseCoroutine;

    void Awake()
    {
        // Set up references
        playerBehavior = GetComponent<PlayerBehavior>();
        realtimeCamera = References.realtimeCamera.GetComponent<PlayerCameraRotation>();
        events = EventSystem.current;
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) mouseCoroutine = StartCoroutine(checkMouseInputs());
        else
        {
            if (mouseCoroutine != null) StopCoroutine(mouseCoroutine);
        }
    }


    IEnumerator checkMouseInputs()
    {
        while (true)
        {
            // If we are not rotating and the mouse button was let go...
            if (!realtimeCamera.rotating && Input.GetMouseButtonUp(0))
            {
                // ...and we didn't just touch a GUI element...
                if (events.currentSelectedGameObject == null)
                {
                    // Get the object that we touched (if any)
                    RaycastHit hitInfo;
                    if (Physics.Raycast(References.realtimeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
                    {
                        // Send what we touched to the player behavior for processing
                        playerBehavior.changeTarget(hitInfo.transform.gameObject, hitInfo.point);
                    }
                }
            }

            yield return null;
        }
    }
}
