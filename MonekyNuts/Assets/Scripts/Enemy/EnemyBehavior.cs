using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public enum intentions { attackPlayer, attackCastle, attackTroop, wander };
    [SerializeField]
    intentions intent;

    public enum actions { moveToTarget, attack, rotate, idle };
    [SerializeField]
    actions action;

    public delegate void voidDelegate();
    public event voidDelegate changeOfIntentions, changeOfActions;

    [SerializeField]
    GameObject target;
    [SerializeField]
    Vector3 destination;




    // Target checking and getters
    public bool targetIsPlayer() { return target != null && target.tag == "Player"; }
    public bool targetIsCastle() { return target != null && target.tag == "Castle"; }
    public bool targetIsTroop() { return target != null && target.tag == "Troop"; }
    public GameObject getTarget() { return target; }
    public Vector3 getDestination() { return destination; }
    public actions getAction() { return action; }
    public intentions getIntent() { return intent; }




    // Event calls
    public void callChangeOfIntentions() { if (changeOfIntentions != null) changeOfIntentions(); }
    public void callChangeOfActions() { if (changeOfActions != null) changeOfActions(); }




    /// <summary>
    /// Changes the action of the enemy
    /// </summary>
    /// <param name="newAction">Action to change to</param>
    public void changeAction(actions newAction)
    {
        action = newAction;
        callChangeOfActions();
    }





    /// <summary>
    /// Changes the intention of the enemy based on the input recieved
    /// </summary>
    /// <param name="target">The target object or creature</param>
    public void changeIntent(GameObject target)
    {
        this.target = target;
        destination = transform.position;
        int currentIntent = (int)intent;

        if (targetIsPlayer() && currentIntent >= (int)intentions.attackPlayer) intent = intentions.attackPlayer;
        else if (targetIsCastle() && currentIntent >= (int)intentions.attackCastle) intent = intentions.attackCastle;
        else if (targetIsTroop() && currentIntent >= (int)intentions.attackTroop) intent = intentions.attackTroop;

        callChangeOfIntentions();
    }
    
    /// <summary>
    /// Changes the intention of the enemy based on the input recieved
    /// </summary>
    /// <param name="destination">The target destination</param>
    public void changeIntent(Vector3 destination)
    {
        this.destination = destination;
        target = this.gameObject;
        intent = intentions.wander;

        callChangeOfIntentions();
    }
}
