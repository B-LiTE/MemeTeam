using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public enum states { menu, strategy, realtime };
    public states currentState;

    void Awake()
    {
        currentState = states.realtime;
    }
}
