using UnityEngine;
using System.Collections;

public class CameraHiding : MonoBehaviour {

    [SerializeField]
    StateManager.states hideDuringState;

    Camera camera;

    void Awake()
    {
        References.stateManager.changeState += onStateChange;
        camera = GetComponent<Camera>();
    }

    void onStateChange()
    {
        if (References.stateManager.CurrentState == hideDuringState)
        {
            camera.cullingMask = 1 << 0;
        }
        else camera.enabled = true;
    }

}
