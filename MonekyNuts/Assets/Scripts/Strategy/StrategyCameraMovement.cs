using UnityEngine;
using System.Collections;

public class StrategyCameraMovement : MonoBehaviour {

    Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
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
            // Bring the strategy camera in front of the realtime camera
            camera.depth = 1;
        }
        else
        {
            // Push the strategy camera behind the realtime camera
            camera.depth = -1;
        }
    }

}
