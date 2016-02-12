using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    // INTENTIONS: Intentions are smart. They guide the enemy's behavior through many steps to get to the end goal.
    // Intention methods should use "changeAction" to guide behavior as needed.
    public enum intentions { attackPlayer, attackCastle, attackTroop, wander };
    [SerializeField]
    intentions intent;

    // ACTIONS: Actions are dumb. They do only one thing and don't care about why (the intention).
    // Actions SHOULD NOT use either "changeAction" or "changeIntention".
    public enum actions { move, attack, rotate };
    [SerializeField]
    actions action;






    public delegate void voidDelegate();
    public event voidDelegate changeOfIntentions, changeOfActions, onEnemyDeath;

    [SerializeField]
    GameObject target;






    void Start()
    {
        References.stateManager.changeState += onStateChange;
        onEnemyDeath += onDeath;

        changeIntent(transform.gameObject);
        changeAction(actions.move);
    }


    void onDeath()
    {
        References.stateManager.changeState -= onStateChange;
    }




    void onStateChange()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            callChangeOfIntentions();
            callChangeOfActions();
        }
    }




    // Target checking and getters
    public bool targetIsPlayer() { return target != null && LayerMask.LayerToName(target.layer) == "Player"; }
    public bool targetIsCastle() { return target != null && LayerMask.LayerToName(target.layer) == "Castle"; }
    public bool targetIsTroop() { return target != null && LayerMask.LayerToName(target.layer) == "Troop"; }
    public GameObject getTarget() { return target; }
    public actions getAction() { return action; }
    public intentions getIntent() { return intent; }
    public intentions getIntentGiven(GameObject item)
    {
        switch (LayerMask.LayerToName(target.layer))
        {
            case "Player":
                return intentions.attackPlayer;
            case "Castle":
                return intentions.attackCastle;
            case "Troops":
                return intentions.attackTroop;
            default:
                return intentions.wander;
        }
    }




    // Event calls
    public void callChangeOfIntentions() { if (changeOfIntentions != null && References.stateManager.CurrentState == StateManager.states.realtime) changeOfIntentions(); }
    public void callChangeOfActions() { if (changeOfActions != null && References.stateManager.CurrentState == StateManager.states.realtime) changeOfActions(); }
    public void callOnDeath() { if (onEnemyDeath != null) onEnemyDeath(); }




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
            this.target = this.gameObject;
            intent = intentions.wander;
            callChangeOfIntentions();
        }
    }
}
