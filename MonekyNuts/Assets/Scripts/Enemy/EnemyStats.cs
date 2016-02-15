using UnityEngine;
using System.Collections;

public class EnemyStats : KillableInstance {

    EnemyBehavior enemyBehavior;

    [SerializeField]
    GameObject troopPrefab;

    [SerializeField]
    int chanceToChange;

    public float attackValue;
    public float secondsBetweenAttacks;
    public float attackRange;

    void Awake()
    {
        base.Awake();
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    protected override void Die()
    {
        enemyBehavior.callOnDeath();

        if (Random.Range(0, 100) > chanceToChange) Instantiate(troopPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
