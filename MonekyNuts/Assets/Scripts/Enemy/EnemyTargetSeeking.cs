using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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











    /// <summary>
    /// Continually scans for targets, changing intent when needed
    /// </summary>
    /// <returns></returns>
    IEnumerator seekTarget()
    {
        while (true)
        {
            // Get all important objects in the sensing radius
            RaycastHit[] sensedObjects = Physics.SphereCastAll(transform.position, 25, new Vector3(0.1f, 0, 0.1f))
                .Where(x => x.transform.tag == "Player" || x.transform.tag == "Castle" || x.transform.tag == "Troop").ToArray();

            int state = (int)EnemyBehavior.intentions.wander;
            foreach (RaycastHit item in sensedObjects)
            {
                // If the enemy can see the item...
                if (canSee(item.transform.gameObject))
                {
                    // ...and the item is a higher priority than the current item...
                    if ((int)enemyBehavior.getIntentGiven(item.transform.gameObject) < state)
                    {
                        // Change the intent
                        enemyBehavior.changeIntent(item.transform.gameObject);
                        state = (int)enemyBehavior.getIntentGiven(item.transform.gameObject);

                        // If the item is the player, break out of the loop since the player is the highest priority
                        if (item.transform.tag == "Player") break;
                    }
                }
            }

            // If we haven't changed intentions, start wandering
            if (state == (int)EnemyBehavior.intentions.wander) enemyBehavior.changeIntent(this.gameObject);

            yield return null;
        }
    }









    public Vector3 pickRandomPointInSight()
    {
        Vector3 chosenPoint = transform.position;

        chosenPoint.z = transform.position.z + Random.Range(3f, sensingRadius);
        chosenPoint.x = transform.position.x + Random.Range(-chosenPoint.z, chosenPoint.z);

        return chosenPoint;
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
    /// Checks whether a point is within the enemy's sight lines
    /// </summary>
    /// <param name="target">Point to test</param>
    /// <returns>Returns true if the point is in sight lines, false if not</returns>
    bool inSightLines(Vector3 target)
    {
        Vector3 directionToObject = target - transform.position;
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
