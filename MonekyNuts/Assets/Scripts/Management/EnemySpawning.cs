using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Vector4 bounds;

    StateTiming stateTiming;

    int strategyTimes = 3;

    void Start()
    {
        stateTiming = References.stateTiming;
        References.stateManager.changeState += checkSpawnEnemy;
        References.stateManager.nextLevel += nextLevel;

        spawnEnemies(5);
    }

    void nextLevel()
    {
        if (References.currentLevel == 2) bounds = new Vector4(-830, 830, -440, 440);
        else bounds = new Vector4(-960, 960, -980, 980);

        spawnEnemies(References.currentLevel + 5);
    }

    void checkSpawnEnemy()
    {
        if (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            if (--strategyTimes <= 0)
            {
                strategyTimes = 3;

                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    if (!spawnEnemy()) i--;
                }
            }
        }
    }

    int tries = 0;

    void spawnEnemies(int number)
    {
        for (int i = 0; i < number; i++)
            if (!spawnEnemy()) i--;
    }


    bool spawnEnemy()
    {
        tries++;
        float randomX, randomZ;

        if (Random.Range(0, 2) == 0)
        {
            if (Random.Range(0, 2) == 0) randomX = bounds.x;
            else randomX = bounds.y;

            randomZ = Random.Range(bounds.z, bounds.w);
        }
        else
        {
            if (Random.Range(0, 2) == 0) randomZ = bounds.z;
            else randomZ = bounds.w;

            randomX = Random.Range(bounds.x, bounds.y);
        }

        Vector3 testingVector = new Vector3(randomX, 100, randomZ);

        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(testingVector, Vector3.down), out hitInfo)) testingVector = hitInfo.point + new Vector3(0, enemyPrefab.transform.localScale.y, 0);
        else if (Physics.Raycast(new Ray(testingVector, Vector3.up), out hitInfo)) testingVector = hitInfo.point + new Vector3(0, enemyPrefab.transform.localScale.y, 0);

        GameObject.Instantiate(enemyPrefab, testingVector, new Quaternion());

        return true;
    }


}
