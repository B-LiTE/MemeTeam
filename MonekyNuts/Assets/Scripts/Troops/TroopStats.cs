using UnityEngine;
using System.Collections;

public class TroopStats : KillableInstance {

    TroopBehavior troopBehavior;

    public float attackValue;
    public float secondsBetweenAttacks;
    public float attackRange;

    void Awake()
    {
        base.Awake();
        troopBehavior = GetComponent<TroopBehavior>();
    }

    protected override void Die()
    {
        troopBehavior.callOnDeath();
        Destroy(gameObject);
    }
}
