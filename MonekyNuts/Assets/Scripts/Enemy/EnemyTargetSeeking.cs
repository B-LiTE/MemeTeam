﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyTargetSeeking : MonoBehaviour {

    EnemyBehavior enemyBehavior;

    [SerializeField]
    public bool targetVisible;

    [SerializeField]
    LayerMask visibleLayers, targetLayers;

    [SerializeField]
    float fieldOfViewAngle, sensingRadius;

    Coroutine seekTargetCoroutine, canSeeTargetCoroutine;

    public delegate void voidDelegate();
    public event voidDelegate onTargetVisible;

    void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    void Start()
    {
        seekTargetCoroutine = StartCoroutine(seekTarget());
        canSeeTargetCoroutine = StartCoroutine(canSeeTarget());
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
            Collider[] sensedObjects = Physics.OverlapSphere(transform.position, sensingRadius, targetLayers);

            int intentChoice = (int)EnemyBehavior.intentions.wander;
            foreach (Collider item in sensedObjects)
            {
                GameObject currentItem = item.transform.gameObject;

                // If the enemy can see the item...
                if (canSee(currentItem))
                {
                    // ...and our target is the same as the item...
                    if (enemyBehavior.getTarget() == currentItem)
                    {
                        // Set our choice and continue
                        intentChoice = (int)enemyBehavior.getIntent();
                        continue;
                    }

                    // If the item is the same or higher priority than the current item...
                    if ((int)enemyBehavior.getIntentGiven(currentItem) <= intentChoice)
                    {
                        // Change the intent
                        enemyBehavior.changeIntent(currentItem);
                        intentChoice = (int)enemyBehavior.getIntentGiven(currentItem);

                        // If the item is the player, break out of the loop since the player is the highest priority
                        if (enemyBehavior.targetIsPlayer()) break;
                    }
                }
                // If the enemy can't see the item...
                else
                {
                    // ...but our target is the item...
                    if (enemyBehavior.getTarget() == currentItem)
                    {
                        // The target is around here somewhere (because they're still in the sensing radius)
                        intentChoice = (int)enemyBehavior.getIntent();
                    }
                }
            }

            // If we haven't changed intentions, start wandering
            if (intentChoice == (int)EnemyBehavior.intentions.wander) enemyBehavior.changeIntent(this.gameObject);

            yield return new WaitForSeconds(0.03f);
        }
    }








    IEnumerator canSeeTarget()
    {
        while (true)
        {
            if (canSee(enemyBehavior.getTarget()))
            {
                if (!targetVisible)
                {
                    targetVisible = true;
                    if (onTargetVisible != null) onTargetVisible();
                }
            }
            else targetVisible = false;

            yield return new WaitForSeconds(0.05f);
        }
    }









    public Vector3 pickRandomPointInSight()
    {
        Vector3 chosenPoint = transform.position;

        float deltaZ = Random.Range(4f, sensingRadius);
        float deltaX = Random.Range(-deltaZ, deltaZ);

        chosenPoint.z = (transform.forward.z * deltaZ);
        chosenPoint.x = (transform.right.x * deltaX);

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
        Vector3 direction = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
        Physics.Raycast(transform.position, direction, out hitInfo, sensingRadius, visibleLayers);
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