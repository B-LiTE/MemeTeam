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
    public intentions getIntentGiven(GameObject item)
    {
        switch (item.tag)
        {
            case "Player":
                return intentions.attackPlayer;
            case "Castle":
                return intentions.attackCastle;
            case "Troop":
                return intentions.attackTroop;
            default:
                return intentions.wander;
        }
    }




    // Event calls
    public void callChangeOfIntentions() { if (changeOfIntentions != null) changeOfIntentions(); }
    public void callChangeOfActions() { if (changeOfActions != null) changeOfActions(); }




    /// <summary>
    /// Changes the action of the enemy
    /// </summary>
    /// <param name="newAction">Action to change to</param>
    public void changeAction(actions newAction)
    {
        // Change action
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

        // If the target is the player...
        if (targetIsPlayer())
        {
            // ...and we aren't on the "attack player" intent... 
            if (currentIntent != (int)intentions.attackPlayer)
            {
                // Change the intent
                intent = intentions.attackPlayer;
                callChangeOfIntentions();
            }
        }
        // If the target is the castle...
        else if (targetIsCastle())
        {
            // ...and we aren't on the "attack castle" intent...
            if (currentIntent != (int)intentions.attackCastle)
            {
                // Change the intent
                intent = intentions.attackCastle;
                callChangeOfIntentions();
            }
        }
        // If the target is a troop...
        else if (targetIsTroop())
        {
            // ...and we aren't on the "attack troop" intent...
            if (currentIntent != (int)intentions.attackTroop)
            {
                // Change the intent
                intent = intentions.attackTroop;
                callChangeOfIntentions();
            }
        }
        // If the target isn't the player, castle, or a troop, change to the "wandering" intent
        else
        {
            changeIntent(target.transform.position);
        }
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
