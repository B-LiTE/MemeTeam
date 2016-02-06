using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerAttacks))]
public class PlayerBehavior : MonoBehaviour {
    
    PlayerMovement playerMovement;
    PlayerAttacks playerAttacks;

    [SerializeField]
    GameObject target;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttacks = GetComponent<PlayerAttacks>();
    }




    // Target checking and getters
    public bool targetIsTerrain() { return target != null && LayerMask.LayerToName(target.layer) == "Terrain"; }
    public bool targetIsEnemy() { return target != null && LayerMask.LayerToName(target.layer) == "Enemies"; }
    public GameObject getTarget() { return target; }





    /// <summary>
    /// Changes the intention of the enemy based on the input recieved
    /// </summary>
    /// <param name="target">The target object or creature</param>
    public void changeTarget(GameObject target, Vector3 worldSpaceMouseClick)
    {
        this.target = target;
        playerMovement.goTo(worldSpaceMouseClick);
        playerAttacks.changeAttack(targetIsEnemy());
    }
}
