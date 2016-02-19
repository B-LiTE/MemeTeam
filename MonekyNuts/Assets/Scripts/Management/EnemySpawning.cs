using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject walkableGround;

    StateTiming stateTiming;

    int strategyTimes = 3;

    float minX, maxX, minZ, maxZ;

    void Start()
    {
        Bounds bounds = walkableGround.GetComponent<Collider>().bounds;
        minX = bounds.min.x;
        maxX = bounds.max.x;
        minZ = bounds.min.z;
        maxZ = bounds.max.z;
        stateTiming = References.stateTiming;
        References.stateManager.changeState += spawnEnemy;
    }

    void spawnEnemy()
    {
        if (--strategyTimes <= 0)
        {
            strategyTimes = 3;

            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                float randomX, randomZ;

                if (Random.Range(0, 2) == 0)
                {
                    if (Random.Range(0, 2) == 0) randomX = minX;
                    else randomX = maxX;

                    randomZ = Random.Range(minZ, maxZ);
                }
                else
                {
                    if (Random.Range(0, 2) == 0) randomZ = minZ;
                    else randomZ = maxZ;

                    randomX = Random.Range(minX, maxX);
                }

                GameObject newEnemy = (GameObject)GameObject.Instantiate(enemyPrefab, new Vector3(randomX, 0, randomZ), new Quaternion());
                newEnemy.transform.LookAt(References.castle.transform);
            }
        }
    }


}
