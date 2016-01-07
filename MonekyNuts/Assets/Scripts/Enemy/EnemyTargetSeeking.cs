using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyBehavior), typeof(SphereCollider))]
public class EnemyTargetSeeking : MonoBehaviour {

    EnemyBehavior enemyBehavior;
    SphereCollider lineOfSight;

    [SerializeField]
    float fieldOfViewAngle;

    Coroutine scanningCoroutine;

    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        lineOfSight = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (inSight(other.gameObject))
        {
            RaycastHit hitInfo = raycastTo(other.gameObject);

            if (other.tag == "Player")
            {
                if (hitInfo.transform.gameObject.tag == "Player") enemyBehavior.changeIntent(other.gameObject);
            }
            else if (other.tag == "Castle")
            {
                if (hitInfo.transform.gameObject.tag == "Castle") enemyBehavior.intent = EnemyBehavior.intentions.attackCastle;//enemyBehavior.changeIntent(other.gameObject);
            }
            else if (other.tag == "Troop")
            {
                if (hitInfo.transform.gameObject.tag == "Troop") enemyBehavior.intent = EnemyBehavior.intentions.attackTroop;//enemyBehavior.changeIntent(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && enemyBehavior.intent == EnemyBehavior.intentions.attackPlayer) enemyBehavior.intent = EnemyBehavior.intentions.wander;
        else if (other.gameObject.tag == "Castle" && enemyBehavior.intent == EnemyBehavior.intentions.attackCastle) enemyBehavior.intent = EnemyBehavior.intentions.wander;
        else if (other.gameObject.tag == "Enemy" && enemyBehavior.intent == EnemyBehavior.intentions.attackTroop) enemyBehavior.intent = EnemyBehavior.intentions.wander;
    }

    RaycastHit raycastTo(GameObject target)
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hitInfo, lineOfSight.radius);
        return hitInfo;
    }
    bool inSight(GameObject target)
    {
        Vector3 directionToObject = target.transform.position - transform.position;
        float angle = Vector3.Angle(directionToObject, transform.forward);

        return angle < fieldOfViewAngle * 0.5f;
    }
}
