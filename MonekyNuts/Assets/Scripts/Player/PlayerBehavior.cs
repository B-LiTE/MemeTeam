using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    // INTENTIONS: Intentions are smart. They guide the enemy's behavior through many steps to get to the end goal.
    // Intention methods should use "changeAction" to guide behavior as needed.
    public enum intentions { move, attack, idle };
    [SerializeField]
    intentions intent;

    // ACTIONS: Actions are dumb. They do only one thing and don't care about why (the intention).
    // Actions SHOULD NOT use either "changeAction" or "changeIntention".
    public enum actions { moveToTarget, attack, rotate };
    [SerializeField]
    actions action;






    public delegate void voidDelegate();
    public event voidDelegate changeOfIntentions, changeOfActions;

    [SerializeField]
    GameObject target;






    void Start()
    {
        changeIntent(transform.gameObject);
        changeAction(actions.moveToTarget);
    }




    // Target checking and getters
    public bool targetIsTerrain() { return target != null && target.tag == "Terrain"; }
    public bool targetIsEnemy() { return target != null && target.tag == "Enemy"; }
    public GameObject getTarget() { return target; }
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

        callChangeOfIntentions();
    }
}
