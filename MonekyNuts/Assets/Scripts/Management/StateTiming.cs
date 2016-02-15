using UnityEngine;
using System.Collections;

public class StateTiming : MonoBehaviour {

    [SerializeField]
    public int secondsOfRealtime;

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
        yield return new WaitForSeconds(secondsOfRealtime);
        References.stateManager.CurrentState = StateManager.states.strategy;
        realtimePhaseCoroutine = null;
    }

}
