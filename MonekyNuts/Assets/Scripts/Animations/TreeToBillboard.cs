using UnityEngine;
using System.Collections;

public class TreeToBillboard : MonoBehaviour {

    LODGroup levelOfDetail;

    void Start()
    {
        References.stateManager.changeState += changeLOD;
        levelOfDetail = GetComponent<LODGroup>();
    }

    void changeLOD()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            levelOfDetail.ForceLOD(-1);
        }
        else
        {
            levelOfDetail.ForceLOD(2);
        }
    }
}
