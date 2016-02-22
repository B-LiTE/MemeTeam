using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerStats), typeof(PlayerMouseCommands))]
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttacks))]
public class PlayerBehavior : MonoBehaviour {

    PlayerStats playerStats;
    PlayerMovement playerMovement;
    PlayerAttacks playerAttacks;

    [SerializeField]
    float fieldOfViewAngle;

    [SerializeField]
    GameObject target;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttacks = GetComponent<PlayerAttacks>();
    }




    // Target checking and getters
    public bool targetIsTerrain() { return target != null && LayerMask.LayerToName(target.layer) == "Terrain"; }
    public bool targetIsEnemy() { return target != null && LayerMask.LayerToName(target.layer) == "Enemies"; }
    public bool targetIsCollectible() { return target != null && LayerMask.LayerToName(target.layer) == "Collectible"; }
    public GameObject getTarget() { return target; }





    /// <summary>
    /// Changes the intention of the enemy based on the input recieved
    /// </summary>
    /// <param name="target">The target object or creature</param>
    public void changeTarget(RaycastHit hitInfo)
    {
        target = hitInfo.transform.gameObject;

        playerMovement.goTo(hitInfo.point);
        playerAttacks.changeAttack(targetIsEnemy());
    }












    public bool seesTarget()
    {
        return canSee(target);
    }

    public Coroutine startRotating()
    {
        return playerMovement.startRotating();
    }

















    /// <summary>
    /// Casts a ray towards the target and returns whatever object it hits
    /// </summary>
    /// <param name="target">Object to cast a ray towards</param>
    /// <returns>Returns the object the ray hits</returns>
    RaycastHit raycastTo(GameObject target)
    {
        return raycastTo(target.transform.position);
    }

    /// <summary>
    /// Casts a ray towards a target and returns whatever object it hits
    /// </summary>
    /// <param name="target">Point to cast a ray towards</param>
    /// <returns>Returns the object the ray hits</returns>
    RaycastHit raycastTo(Vector3 target)
    {
        RaycastHit hitInfo;
        Vector3 direction = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);
        Physics.Raycast(transform.position, direction, out hitInfo);
        return hitInfo;
    }








    /// <summary>
    /// Checks whether an object is within the enemy's sight lines
    /// </summary>
    /// <param name="target">Object to test</param>
    /// <returns>Returns true if the object is in sight lines, false if not</returns>
    bool inSightLines(GameObject target)
    {
        return inSightLines(target.transform.position);
    }

    /// <summary>
    /// Checks whether a point is within the enemy's sight lines
    /// </summary>
    /// <param name="target">Point to test</param>
    /// <returns>Returns true if the point is in sight lines, false if not</returns>
    bool inSightLines(Vector3 target)
    {
        Vector3 directionToObject = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);
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

    /// <summary>
    /// Check if the enemy has unobstructed line of sight to the point in space
    /// </summary>
    /// <param name="target">Point in space trying to be seen</param>
    /// <returns>Returns true if unobstructed, false if not</returns>
    bool canSee(Vector3 target)
    {
        if (inSightLines(target))
        {
            RaycastHit hitInfo = raycastTo(target);
            if (hitInfo.transform == null) return true;
        }

        return false;
    }
}
