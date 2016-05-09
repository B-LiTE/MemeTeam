using UnityEngine;
using System.Collections;

public class EnlargeMapPoint : MonoBehaviour {

    int currentScale = 1;
    EnemyStats enemyStats;
    TroopStats troopStats;

    void Awake()
    {
        switch (transform.parent.tag)
        {
            case "Troop":
                troopStats = GetComponentInParent<TroopStats>();
                troopStats.alertOnDeath += death;
                break;

            case "Enemy":
                enemyStats = GetComponentInParent<EnemyStats>();
                enemyStats.alertOnDeath += death;
                break;
        }
        References.stateManager.nextLevel += nextLevel;
        rescale();
    }

    void nextLevel()
    {
        rescale();
    }

    void death()
    {
        References.stateManager.nextLevel -= nextLevel;
    }

    void rescale()
    {
        for (int i = currentScale; i < References.currentLevel; i++)
        {
            transform.localScale *= 2;
        }
        currentScale = References.currentLevel;
    }

}
