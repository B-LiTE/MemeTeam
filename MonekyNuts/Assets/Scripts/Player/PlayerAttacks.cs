using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour {

    PlayerCameraRotation realtimeCamera;
    /*
     
     STILL WORKING ON
    
    conditions to start attacking
    - attackable object
    - within range

    conditions to stop attacking
    - enemy dies
    - clicks on another target
    - starts moving
     
     */
    // Object being attacked
    [SerializeField]
    Vector3 destination;

    // DEBUG - range to attack from
    [SerializeField]
    int attackRange;

    // Reference to coroutine
    Coroutine attackCoroutine;

    void Awake()
    {
        // Set up references
        destination = transform.position;
        realtimeCamera = References.realtimeCamera.GetComponent<PlayerCameraRotation>();
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) attackCoroutine = StartCoroutine(checkMovement());
        else
        {
            if (attackCoroutine != null) StopCoroutine(attackCoroutine);
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
                if (Physics.Raycast(References.realtimeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, attackRange))
                {
                    // ...and we are allowed to attack the object...
                    if (canAttack(hitInfo.transform.tag))
                    {
                        // Start attacking?
                    }
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// Checks if the clicked on object can be attacked
    /// </summary>
    /// <param name="tag">the tag of the clicked object</param>
    /// <returns>Returns true if player can attack it, false if not</returns>
    bool canAttack(string tag)
    {
        switch (tag)
        {
            case "Enemy":
                return true;

            default:
                return false;
        }
    }
}
