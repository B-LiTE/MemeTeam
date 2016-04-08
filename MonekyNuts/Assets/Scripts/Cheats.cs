using UnityEngine;
using System.Collections;

public static class Cheats {

    public static void nextLevelWithoutLoad()
    {
        References.stateManager.loadNextLevel();
    }

}
