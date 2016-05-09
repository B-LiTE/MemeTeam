using UnityEngine;
using System.Collections;

public class PlayerKeyboardAttacks : MonoBehaviour {

    [SerializeField]
    CapsuleCollider attackSwordCollider;

    [SerializeField]
    GameObject ArrowPrefab;

    [SerializeField]
    [Range(-15, 90)]
    float angleOfArrowLaunch;

    PlayerStats playerStats;

    Coroutine attackSwordCoroutine, swordDetectionCoroutine, throwArrowCoroutine;
    System.Collections.Generic.List<GameObject> enemies;

    Animations animations;
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        References.stateManager.changeState += onChangeState;
        animations = GetComponentInChildren<Animations>();
        enemies = new System.Collections.Generic.List<GameObject>();
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.strategy) stopAttackSwordCoroutine();
    }

    public void startAttackSwordCoroutine()
    {
        stopThrowArrowCoroutine();
        if (attackSwordCoroutine == null) attackSwordCoroutine = StartCoroutine(attackSword());
    }
    public void stopAttackSwordCoroutine()
    {
        if (attackSwordCoroutine != null)
        {
            StopCoroutine(attackSwordCoroutine);
            attackSwordCoroutine = null;
        }
        if (swordDetectionCoroutine != null)
        {
            StopCoroutine(swordDetectionCoroutine);
            swordDetectionCoroutine = null;
        }
        enemies.Clear();
    }

    IEnumerator attackSword()
    {
        attackSwordCollider.enabled = true;
        swordDetectionCoroutine = StartCoroutine(swordDetection());

        while (animations.anim.GetBool("Punch"))
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().Damage(playerStats.activeDamage, References.player);
            }

            yield return new WaitForSeconds(playerStats.secondsBetweenAttacks);
        }

        attackSwordCollider.enabled = false;
        enemies.Clear();
        stopAttackSwordCoroutine();
    }

    IEnumerator swordDetection()
    {
        while (true)
        {
            enemies.Clear();

            foreach (Collider entity in Physics.OverlapSphere(attackSwordCollider.bounds.center, attackSwordCollider.height))
            {
                if (entity.tag == "Enemy") enemies.Add(entity.transform.gameObject);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    public void startThrowArrowCoroutine()
    {
        stopAttackSwordCoroutine();
        if (throwArrowCoroutine == null) throwArrowCoroutine = StartCoroutine(throwArrow());
    }

    public void stopThrowArrowCoroutine()
    {
        if (throwArrowCoroutine != null)
        {
            StopCoroutine(throwArrowCoroutine);
            throwArrowCoroutine = null;
        }
    }

    IEnumerator throwArrow()
    {
        while (animations.anim.GetBool("Punch"))
        {
            Vector3 arrowPosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            Quaternion arrowRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x - angleOfArrowLaunch, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
            GameObject.Instantiate(ArrowPrefab, arrowPosition, arrowRotation);

            yield return new WaitForSeconds(0.75f);
        }

        stopThrowArrowCoroutine();
    }

}
