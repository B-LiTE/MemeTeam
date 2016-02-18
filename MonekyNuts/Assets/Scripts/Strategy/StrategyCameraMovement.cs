using UnityEngine;
using System.Collections;

public class StrategyCameraMovement : MonoBehaviour {

    Camera strategyCamera;
    Camera actionCamera;

    void Awake()
    {
        strategyCamera = GetComponent<Camera>();
        actionCamera = References.realtimeCamera;
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
    }

    void onStateChange()
    {
        // If we are in the strategy phase...
        if (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            // Enable the camera
            strategyCamera.enabled = true;
            actionCamera.enabled = false;

            // Bring the strategy camera in front of the realtime camera
            strategyCamera.depth = 1;
        }
        else
        {
            // Disable the camera
            actionCamera.enabled = true;
            strategyCamera.enabled = false;

            // Push the strategy camera behind the realtime camera
            strategyCamera.depth = -1;
        }
    }

}
