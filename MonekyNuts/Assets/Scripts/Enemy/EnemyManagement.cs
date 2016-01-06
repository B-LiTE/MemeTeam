using UnityEngine;
using System.Collections;

public class EnemyManagement : MonoBehaviour {

    public enum states { idle, scanning, moving, attacking }

    public states currentState;

    void Awake()
    {
        currentState = states.idle;
    }
}
