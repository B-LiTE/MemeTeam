using UnityEngine;
using System.Collections;

// File for commonly referenced objects
public static class References {

    // Managers and cameras
    public static StateManager stateManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();
    public static Camera realtimeCamera = GameObject.FindGameObjectWithTag("Camera_Realtime").GetComponent<Camera>();
    public static Camera strategicCamera = GameObject.FindGameObjectWithTag("Camera_Strategic").GetComponent<Camera>();

    // Game entities
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject castle = GameObject.FindGameObjectWithTag("Castle");

}
