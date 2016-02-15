using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RealtimeButton : MonoBehaviour {

    Image image;
    Button button;
    Text text;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        References.stateManager.changeState += onChangeState;
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            image.enabled = false;
            button.enabled = false;
            text.enabled = false;
        }
        else
        {
            image.enabled = true;
            button.enabled = true;
            text.enabled = true;
        }
    }

    public void startRealtime()
    {
        References.stateTiming.startRealtimePhase();
    }
}
