using UnityEngine;
using System.Collections;

// Controls the player's behavior and acts as a liason between the various Player scripts

[RequireComponent(typeof(PlayerStats), typeof(PlayerMouseCommands))]
[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttacks))]
public class PlayerBehavior : MonoBehaviour {

    // Set up the references to the other scripts
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    PlayerAttacks playerAttacks;

    // The range of view the player model has
    [SerializeField]
    float fieldOfViewAngle;

    // The object we are interacting with
    [SerializeField]
    GameObject target;

    // Set up references
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
        // Set the target to be the object we clicked
        target = hitInfo.transform.gameObject;

        // Start going to it and attack it if it's an enemy
        playerMovement.goTo(hitInfo.point);
        playerAttacks.changeAttack(targetIsEnemy());
    }












    // Public function for if the player can see the target
    public bool seesTarget()
    {
        return canSee(target);
    }

    // Allows starting of player rotation
    public Coroutine startRotating()
    {
        return playerMovement.startRotating();
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
    /// Check if the enemy has unobstructed line of sight to the point in space
    /// </summary>
    /// <param name="target">Point in space trying to be seen</param>
    /// <returns>Returns true if unobstructed, false if not</returns>
    bool canSee(GameObject target)
    {
        // If the target is within our vision...
        if (inSightLines(target.transform.position))
        {
            // ...and there's nothing blocking our view, return true
            RaycastHit hitInfo = raycastTo(target.transform.position);
            if (hitInfo.transform != null && hitInfo.transform.name == target.name) return true;
        }

        // Otherwise, we can't see it. Return false
        return false;
    }
}
