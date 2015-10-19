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
        if (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            camera.depth = 1;
        }
        else
        {
            camera.depth = -1;
        }
    }

}
