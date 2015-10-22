using UnityEngine;
using System.Collections;

public static class References {

    public static StateManager stateManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();
    public static Camera realtimeCamera = GameObject.FindGameObjectWithTag("Camera_Realtime").GetComponent<Camera>();
    public static Camera strategicCamera = GameObject.FindGameObjectWithTag("Camera_Strategic").GetComponent<Camera>();
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");

}
