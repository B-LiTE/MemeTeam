using UnityEngine;
using System.Collections;

public class LevelBarrier : MonoBehaviour {

    [SerializeField]
    [Range(2, 3)]
    int destroyAtLevel;

    void Awake()
    {
        References.stateManager.nextLevel += nextLevel;
    }

    void nextLevel()
    {
        if (References.currentLevel >= destroyAtLevel)
        {
            References.stateManager.nextLevel -= nextLevel;
            if (GetComponent<TreeToBillboard>() != null) References.stateManager.changeState -= GetComponent<TreeToBillboard>().changeLOD;
            Destroy(this.gameObject);
        }
    }

}
