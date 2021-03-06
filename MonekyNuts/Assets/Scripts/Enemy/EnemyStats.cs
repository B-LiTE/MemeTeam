﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior))]
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
	void Start()
	{
		StartCoroutine (checkHealth ());
	}
	IEnumerator checkHealth()
	{
		while (true) {
			if (currHealth <= 0)
				Die ();
			yield return new WaitForSeconds(0.5f);
		}
	}

    protected override void Die()
    {
        enemyBehavior.callOnDeath();

        if (Random.Range(0, 100) > chanceToChange) Instantiate(troopPrefab, transform.position, transform.rotation);
		if (Random.Range (0, 100) < chanceGold)
			Instantiate (gold, new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z), transform.rotation);

        References.stateManager.tickDownEnemy();

        Destroy(gameObject);
    }

    public override void Damage(float amount, GameObject attacker)
    {
        base.Damage(amount, attacker);

        if (enemyBehavior.getIntent() == EnemyBehavior.intentions.wander || enemyBehavior.getIntent() == EnemyBehavior.intentions.attackCastle)
        {
            enemyBehavior.changeIntent(attacker);
        }
    }
}
