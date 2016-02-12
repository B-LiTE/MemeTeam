using UnityEngine;
using System.Collections;

public class EnemyStats : KillableInstance {

    EnemyBehavior enemyBehavior;

    void Awake()
    {
        base.Awake();
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    protected override void Die()
    {
        enemyBehavior.callOnDeath();
        Destroy(gameObject);
    }
}
