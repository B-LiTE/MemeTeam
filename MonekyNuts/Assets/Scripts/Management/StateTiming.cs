using UnityEngine;
using System.Collections;

public class StateTiming : MonoBehaviour {

    [SerializeField]
    public int secondsOfRealtime, secondsLeftOfRealtime;

    Coroutine realtimePhaseCoroutine;

    void Start()
    {
        References.stateManager.CurrentState = StateManager.states.strategy;
    }

    public void startRealtimePhase()
    {
        if (realtimePhaseCoroutine == null) realtimePhaseCoroutine = StartCoroutine(realtimePhase());
    }

    IEnumerator realtimePhase()
    {
        References.stateManager.CurrentState = StateManager.states.realtime;

        int t = secondsOfRealtime;
        while (t > 0)
        {
            secondsOfRealtime = t--;

            yield return new WaitForSeconds(1);
        }

        References.stateManager.CurrentState = StateManager.states.strategy;
        realtimePhaseCoroutine = null;
    }

}
