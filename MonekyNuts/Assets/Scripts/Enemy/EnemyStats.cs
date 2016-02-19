using UnityEngine;
using System.Collections;

public class EnemyStats : KillableInstance {

    EnemyBehavior enemyBehavior;

    [SerializeField]
    GameObject troopPrefab;

    [SerializeField]
    int chanceToChange;

	[SerializeField]
	GameObject gold;
	[SerializeField]
	int chanceGold;

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
		if (Random.Range (0, 100) < chanceGold)
			Instantiate (gold, new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z), transform.rotation);

        Destroy(gameObject);
    }
}
