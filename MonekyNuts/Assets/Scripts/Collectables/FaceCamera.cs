using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

    Transform target;


	void Start()
	{
		target = References.realtimeCamera.transform;
        References.stateManager.changeState += onChangeState;
        StartCoroutine(faceCamera());
	}

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) target = References.realtimeCamera.transform;
        else target = References.strategicCamera.transform;
    }

    IEnumerator faceCamera()
    {
        while (true)
        {
            transform.LookAt(zeroedYVector(target.position));

            yield return null;
        }
    }

    Vector3 zeroedYVector(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
