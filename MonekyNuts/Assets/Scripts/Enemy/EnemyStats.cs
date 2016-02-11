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
        Debug.Log("dead");
        //enemyBehavior.callOnDeath();
        Destroy(gameObject);
    }
}
