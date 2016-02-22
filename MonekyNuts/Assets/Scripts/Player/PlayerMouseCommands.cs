using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// Intercepts mouse clicks/touches and acts on the objects that were clicked

[RequireComponent(typeof(PlayerBehavior), typeof(PlayerMovement))]
public class PlayerMouseCommands : MonoBehaviour {

    // References to other scripts and objects
    PlayerBehavior playerBehavior;
    PlayerMovement playerMovement;
    PlayerCameraRotation realtimeCamera;
    EventSystem events;
    [SerializeField]
    GameObject flag;

    Coroutine flagFollowEnemyCoroutine, flagAtDestinationCoroutine;

    [SerializeField]
    float distanceAboveTarget;

    // Reference to coroutine
    Coroutine mouseCoroutine;

    void Awake()
    {
        // Set up references
        playerBehavior = GetComponent<PlayerBehavior>();
        playerMovement = GetComponent<PlayerMovement>();
        realtimeCamera = References.realtimeCamera.GetComponent<PlayerCameraRotation>();
        events = EventSystem.current;
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        // If we are in realtime, start checking mouse inputs
        if (References.stateManager.CurrentState == StateManager.states.realtime) mouseCoroutine = StartCoroutine(checkMouseInputs());
        else
        {
            // Otherwise, stop checking mouse inputs
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
                    if (Physics.Raycast(References.realtimeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000))
                    {
                        // Send what we touched to the player behavior for processing
                        playerBehavior.changeTarget(hitInfo);
                    }
                    
                    // Place the flag where it needs to be placed
                    if (playerBehavior.targetIsEnemy()) startFlagFollowEnemy();
                    else startFlagAtDestination();
                }
            }

            yield return null;
        }
    }










    // Start the flag following enemy coroutine while stopping the other one
    void startFlagFollowEnemy()
    {
        if (flagAtDestinationCoroutine != null)
        {
            StopCoroutine(flagAtDestinationCoroutine);
            flagAtDestinationCoroutine = null;
        }

        flagFollowEnemyCoroutine = StartCoroutine(flagFollowEnemy());
    }

    IEnumerator flagFollowEnemy()
    {
        flag.SetActive(true);

        while (playerBehavior.targetIsEnemy())
        {
            Vector3 enemyPosition = playerBehavior.getTarget().transform.position;
            flag.transform.position = new Vector3(enemyPosition.x, enemyPosition.y + distanceAboveTarget, enemyPosition.z);

            yield return null;
        }

        flag.SetActive(false);
    }









    // Start the flag floating above destination coroutine while stopping the other one
    void startFlagAtDestination()
    {
        if (flagFollowEnemyCoroutine != null)
        {
            StopCoroutine(flagFollowEnemyCoroutine);
            flagFollowEnemyCoroutine = null;
        }

        flagAtDestinationCoroutine = StartCoroutine(flagAtDestination());
    }

    IEnumerator flagAtDestination()
    {
        flag.SetActive(true);

        if (playerBehavior.targetIsCollectible()) flag.transform.position = new Vector3(playerMovement.navigation.destination.x, playerMovement.navigation.destination.y + 3 + distanceAboveTarget, playerMovement.navigation.destination.z);
        else flag.transform.position = new Vector3(playerMovement.navigation.destination.x, playerMovement.navigation.destination.y + distanceAboveTarget, playerMovement.navigation.destination.z);

        while (playerMovement.navigation.pathPending) yield return null;

        while (!playerBehavior.targetIsEnemy() && playerMovement.navigation.remainingDistance > playerMovement.navigation.stoppingDistance)
        {
            yield return null;
        }

        flag.SetActive(false);
    }
}
