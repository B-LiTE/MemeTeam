using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyTargetSeeking : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    [SerializeField]
    float fieldOfViewAngle, sensingRadius;

    Coroutine scanningCoroutine;

    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    void Start()
    {
        scanningCoroutine = StartCoroutine(seekTarget());
    }

    IEnumerator seekTarget()
    {
        while (true)
        {
            RaycastHit[] sensedObjects = Physics.SphereCastAll(transform.position, 25, new Vector3(0.1f, 0, 0.1f));
            bool changedIntent = false;
            foreach (RaycastHit item in sensedObjects)
            {
                if (item.transform.tag == "Player" || item.transform.tag == "Castle" || item.transform.tag == "Troop")
                {
                    if (canSee(item.transform.gameObject))
                    {
                        if (item.transform.tag == "Player")
                        {
                            enemyBehavior.changeIntent(item.transform.gameObject);
                            changedIntent = true;
                            break;
                        }
                        else if (item.transform.tag == "Castle")
                        {
                            enemyBehavior.changeIntent(item.transform.gameObject);
                            changedIntent = true;
                        }
                        else if (enemyBehavior.getIntent() != EnemyBehavior.intentions.attackCastle)
                        {
                            enemyBehavior.changeIntent(item.transform.gameObject);
                            changedIntent = true;
                        }
                    }
                }
            }

            if (!changedIntent) enemyBehavior.changeIntent(transform.position + new Vector3(Random.Range(0, 15), 0, Random.Range(0, 15)));

            yield return null;
        }
    }

    /// <summary>
    /// Casts a ray towards the target and returns whatever object it hits
    /// </summary>
    /// <param name="target">Object to cast a ray towards</param>
    /// <returns>Returns the object the ray hits</returns>
    RaycastHit raycastTo(GameObject target)
    {
        RaycastHit hitInfo;
        //Debug.DrawRay(transform.position, (target.transform.position - transform.position).normalized, Color.white, 1);
        Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hitInfo, sensingRadius);
        return hitInfo;
    }
    
    /// <summary>
    /// Checks whether an object is within the enemy's sight lines
    /// </summary>
    /// <param name="target">Object to test</param>
    /// <returns>Returns true if the object is in sight lines, false if not</returns>
    bool inSightLines(GameObject target)
    {
        Vector3 directionToObject = target.transform.position - transform.position;
        float angle = Vector3.Angle(directionToObject, transform.forward);

        return angle < fieldOfViewAngle * 0.5f;
    }
    
    /// <summary>
    /// Check if the gameobject has unobstructed line of sight to the target
    /// </summary>
    /// <param name="target">Object trying to be seen</param>
    /// <returns>Returns true if unobstructed visual, false if not</returns>
    bool canSee(GameObject target)
    {
        if (inSightLines(target))
        {
            RaycastHit hitInfo = raycastTo(target);
            if (hitInfo.transform != null && hitInfo.transform.gameObject == target) return true;
        }

        return false;
    }
}
