using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public enum intentions { attackPlayer, attackCastle, attackTroop, wander };
    public intentions intent;

    public enum actions { moveToTarget, attack, rotate, idle };
    public actions action;

    public GameObject target;
    public Vector3 destination;

    public bool targetIsPlayer() { return target.tag == "Player"; }
    public bool targetIsCastle() { return target.tag == "Castle"; }
    public bool targetIsTroop() { return target.tag == "Troop"; }

    public void changeIntent(GameObject target)
    {
        this.target = target;
        int currentIntent = (int)intent;

        if (targetIsPlayer() && currentIntent >= (int)intentions.attackPlayer) intent = intentions.attackPlayer;
        else if (targetIsCastle() && currentIntent >= (int)intentions.attackCastle) intent = intentions.attackCastle;
        else if (targetIsTroop() && currentIntent >= (int)intentions.attackTroop) intent = intentions.attackTroop;
        else
        {
            destination = target.transform.position;
            intent = intentions.wander;
        }
    }
    public void changeIntent(Vector3 destination)
    {
        this.destination = destination;
        intent = intentions.wander;
    }
}
