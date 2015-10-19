using UnityEngine;
using System.Collections;

public class StateTiming : MonoBehaviour {

    void Start()
    {
        StartCoroutine(gameplayStateChanging());
    }

    IEnumerator gameplayStateChanging()
    {
        int minutes = 60;
        References.stateManager.CurrentState = StateManager.states.strategy;
        while (true)
        {
            yield return new WaitForSeconds(3);//2 * minutes);
            References.stateManager.CurrentState = StateManager.states.realtime;
            yield return new WaitForSeconds(3);//1 * minutes);
            References.stateManager.CurrentState = StateManager.states.strategy;
        }
    }

}
