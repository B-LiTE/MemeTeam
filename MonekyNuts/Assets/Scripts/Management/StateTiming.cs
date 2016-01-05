using UnityEngine;
using System.Collections;

public class StateTiming : MonoBehaviour {

    // DEBUG - number of seconds of strategy and realtime parts
    [SerializeField]
    int secondsOfStrategy, secondsOfRealtime;

    void Start()
    {
        StartCoroutine(gameplayStateChanging());
    }

    IEnumerator gameplayStateChanging()
    {
        int secondsInAMinute = 60;
        References.stateManager.CurrentState = StateManager.states.strategy;
        while (true)
        {
            yield return new WaitForSeconds(secondsOfStrategy);//2 * minutes);
            References.stateManager.CurrentState = StateManager.states.realtime;
            yield return new WaitForSeconds(secondsOfRealtime);//1 * minutes);
            References.stateManager.CurrentState = StateManager.states.strategy;
        }
    }

}
