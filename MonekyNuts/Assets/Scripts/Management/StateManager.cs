using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public enum states { menu, strategy, realtime };
    [SerializeField]
    states currentState;
    public states CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            if (changeState != null) changeState();
        }
    }

    public delegate void changeStateDelegate();
    public event changeStateDelegate changeState;

    void Awake()
    {
        CurrentState = states.menu;
    }

    // DEBUG - to change between states
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            CurrentState = states.menu;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            CurrentState = states.realtime;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            CurrentState = states.strategy;
    }
}
