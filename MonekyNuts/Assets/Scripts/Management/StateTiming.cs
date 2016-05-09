using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateTiming : MonoBehaviour {

    [SerializeField]
    public int secondsOfRealtime, secondsLeftOfRealtime;

    [SerializeField]
    Text timer;
    Coroutine realtimePhaseCoroutine;

    void Awake()
    {
        References.stateManager.nextLevel += nextLevel;
        References.stateManager.CurrentState = StateManager.states.strategy;
        timer.text = "";
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
            secondsLeftOfRealtime = t--;
            timer.text = secondsLeftOfRealtime.ToString();

            yield return new WaitForSeconds(1);
        }

        timer.text = "";
        References.stateManager.CurrentState = StateManager.states.strategy;
        realtimePhaseCoroutine = null;
    }

    void nextLevel()
    {
        timer.text = "";

        StopCoroutine(realtimePhaseCoroutine);
        realtimePhaseCoroutine = null;
    }

}
