﻿using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public enum states { strategy, realtime };
    [SerializeField]
    states currentState;
    public states CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            if (changeState != null) changeState();
            //pause(currentState != states.realtime);
        }
    }

    bool isPaused;

    public delegate void changeStateDelegate();
    public event changeStateDelegate changeState;

    void Awake()
    {
        StartCoroutine(gameStartChangeState());
        isPaused = false;
    }

    void Start()
    {
        StartCoroutine(checkPause());
    }

    IEnumerator gameStartChangeState()
    {
        yield return new WaitForEndOfFrame();
        CurrentState = states.strategy;
    }

    public void pause(bool shouldPause)
    {
        isPaused = shouldPause;
        if (shouldPause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    IEnumerator checkPause()
    {
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isPaused = !isPaused;
                pause(isPaused);
            }

            yield return null;
        }
    }
}
