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
    }

    void nextLevel()
    {
        if (References.currentLevel == 2) bounds = new Vector4(-830, 830, -440, 440);
        else bounds = new Vector4(-960, 960, -980, 980);

        for (int i = 0; i < References.currentLevel + 5; i++)
            if (!spawnEnemy()) i--;
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

        GameObject newEnemy = (GameObject)GameObject.Instantiate(enemyPrefab, new Vector3(randomX, 500, randomZ), new Quaternion());
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(newEnemy.transform.position, Vector3.down), out hitInfo, 1500)) newEnemy.transform.position = hitInfo.point + new Vector3(0, 1.5f, 0);
        else if (Physics.Raycast(new Ray(newEnemy.transform.position, Vector3.up), out hitInfo, 1500)) newEnemy.transform.position = hitInfo.point + new Vector3(0, 1.5f, 0);
        else
        {
            Destroy(newEnemy);
            if (tries < 1000) return false;
        }
        //newEnemy.transform.LookAt(References.castle.transform);
        return true;
    }


}
