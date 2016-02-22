using UnityEngine;
using System.Collections;

public class TroopBehavior : MonoBehaviour {

    // INTENTIONS: Intentions are smart. They guide the troop's behavior through many steps to get to the end goal.
    // Intention methods should use "changeAction" to guide behavior as needed.
    public enum intentions { attackEnemy, getCollectible, protectCastle, wander };
    [SerializeField]
    intentions intent;

    // ACTIONS: Actions are dumb. They do only one thing and don't care about why (the intention).
    // Actions SHOULD NOT use either "changeAction" or "changeIntention".
    public enum actions { move, attack, rotate };
    [SerializeField]
    actions action;






    // Important events
    public delegate void voidDelegate();
    public event voidDelegate changeOfIntentions, changeOfActions, onTroopDeath;

    // Our general target to interact with
    [SerializeField]
    GameObject target;






    void Start()
    {
        // Set up references
        References.stateManager.changeState += onStateChange;
        onTroopDeath += onDeath;

        // Start wandering
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
    public bool targetIsEnemy() { return target != null && LayerMask.LayerToName(target.layer) == "Enemies"; }
    public bool targetIsCollectible() { return target != null && LayerMask.LayerToName(target.layer) == "Collectibles"; }
    public bool targetIsCastle() { return target != null && LayerMask.LayerToName(target.layer) == "Castle"; }
    public GameObject getTarget() { return target; }
    public actions getAction() { return action; }
    public intentions getIntent() { return intent; }
    public intentions getIntentGiven(GameObject item)
    {
        switch (LayerMask.LayerToName(target.layer))
        {
            case "Enemies":
                return intentions.attackEnemy;
            case "Collectibles":
                return intentions.getCollectible;
            case "Castle":
                return intentions.protectCastle;
            default:
                return intentions.wander;
        }
    }




    // Event calls
    public void callChangeOfIntentions() { if (changeOfIntentions != null && References.stateManager.CurrentState == StateManager.states.realtime) changeOfIntentions(); }
    public void callChangeOfActions() { if (changeOfActions != null && References.stateManager.CurrentState == StateManager.states.realtime) changeOfActions(); }
    public void callOnDeath() { if (onTroopDeath != null) onTroopDeath(); }




    /// <summary>
    /// Changes the action of the troop
    /// </summary>
    /// <param name="newAction">Action to change to</param>
    public void changeAction(actions newAction)
    {
        // Change action
        action = newAction;
        callChangeOfActions();
    }





    /// <summary>
    /// Changes the intention of the troop based on the input recieved
    /// </summary>
    /// <param name="target">The target object or creature</param>
    public void changeIntent(GameObject target)
    {
        this.target = target;
        int currentIntent = (int)intent;

        // If the target is an enemy...
        if (targetIsEnemy())
        {
            // ...and we aren't on the "attack enemy" intent... 
            if (currentIntent != (int)intentions.attackEnemy)
            {
                // Change the intent
                intent = intentions.attackEnemy;
                callChangeOfIntentions();
            }
        }
        // If the target is a collectible...
        else if (targetIsCollectible())
        {
            // ...and we aren't on the "get collectible" intent...
            if (currentIntent != (int)intentions.getCollectible)
            {
                // Change the intent
                intent = intentions.getCollectible;
                callChangeOfIntentions();
            }
        }
        // If the target is the castle...
        else if (targetIsCastle())
        {
            // ...and we aren't on the "protect castle" intent...
            if (currentIntent != (int)intentions.protectCastle)
            {
                // Change the intent
                intent = intentions.protectCastle;
                callChangeOfIntentions();
            }
        }
        // If the target isn't an enemy, collectible, or castle, change to the "wandering" intent
        else
        {
            this.target = this.gameObject;
            intent = intentions.wander;
            callChangeOfIntentions();
        }
    }
}
