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
	public static MarketArrays marketArrays = manager.GetComponent<MarketArrays>();
	public static SoundEffectsDatabase soundEffects = GameObject.FindObjectOfType<SoundEffectsDatabase>().GetComponent<SoundEffectsDatabase>();

    // Cameras
    public static Camera realtimeCamera = GameObject.FindGameObjectWithTag("Camera_Realtime").GetComponent<Camera>();
    public static Camera strategicCamera = GameObject.FindGameObjectWithTag("Camera_Strategic").GetComponent<Camera>();

    // Game entities
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject castle = GameObject.FindGameObjectWithTag("Castle");
	public static GameObject soundEffect = (GameObject)Resources.Load("OneTimeSoundEffect", typeof(GameObject));

    // Data
    public static int lives = 3;
    public static int lastLevelIndex;
    public static int currentLevel = 1;









    public static void resetReferences()
    {
        // Managers
        manager = GameObject.FindGameObjectWithTag("Manager");
        stateManager = manager.GetComponent<StateManager>();
        stateTiming = manager.GetComponent<StateTiming>();

        // Game stats and information
        gameStats = manager.GetComponent<GameStats>();
        itemDatabase = manager.GetComponent<Item_Database>();
        inventory = manager.GetComponent<Inventory>();
        market = manager.GetComponent<Market>();
	    marketArrays = manager.GetComponent<MarketArrays>();
        soundEffects = GameObject.FindObjectOfType<SoundEffectsDatabase>().GetComponent<SoundEffectsDatabase>();

        // Cameras
        realtimeCamera = GameObject.FindGameObjectWithTag("Camera_Realtime").GetComponent<Camera>();
        strategicCamera = GameObject.FindGameObjectWithTag("Camera_Strategic").GetComponent<Camera>();

        // Game entities
        player = GameObject.FindGameObjectWithTag("Player");
        castle = GameObject.FindGameObjectWithTag("Castle");
        soundEffect = (GameObject)Resources.Load("OneTimeSoundEffect", typeof(GameObject));

        // Data
        lastLevelIndex = Application.loadedLevel;
        currentLevel = 1;
    }

}
