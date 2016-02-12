using UnityEngine;
using System.Collections;

// File for commonly referenced objects
public static class References {

    // Managers
    public static GameObject manager = GameObject.FindGameObjectWithTag("Manager");
    public static StateManager stateManager = manager.GetComponent<StateManager>();
    public static StateTiming stateTiming = manager.GetComponent<StateTiming>();

    // Game stats and information
    public static GameStats gameStats = manager.GetComponent<GameStats>();
    public static Item_Database itemDatabase = manager.GetComponent<Item_Database>();
    public static Inventory inventory = manager.GetComponent<Inventory>();
    public static Market market = manager.GetComponent<Market>();

    // Cameras
    public static Camera realtimeCamera = GameObject.FindGameObjectWithTag("Camera_Realtime").GetComponent<Camera>();
    public static Camera strategicCamera = GameObject.FindGameObjectWithTag("Camera_Strategic").GetComponent<Camera>();

    // Game entities
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject castle = GameObject.FindGameObjectWithTag("Castle");

}
