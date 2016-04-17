using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowTimer : MonoBehaviour {

    [SerializeField]
    Text timer;
    Coroutine realtimeTimerCoroutine;

    void Awake()
    {
        References.stateManager.changeState += onChangeState;
        timer.text = "";
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) realtimeTimerCoroutine = StartCoroutine(realtimeTimer());
    }

    IEnumerator realtimeTimer()
    {
        int time = References.stateTiming.secondsLeftOfRealtime;

        while (time > 0)
        {
            time = References.stateTiming.secondsLeftOfRealtime;
            timer.text = time.ToString();

            yield return new WaitForSeconds(1);
        }
    }
}
