using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TroopBehavior))]
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

    public override void Damage(float amount, GameObject attacker)
    {
        base.Damage(amount, attacker);

        if (troopBehavior.getIntent() != TroopBehavior.intentions.attackEnemy)
        {
            troopBehavior.changeIntent(attacker);
        }
    }
}
