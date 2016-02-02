using UnityEngine;
using System.Collections;

public class EnemyStats : KillableInstance {

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
