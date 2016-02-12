using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

    Transform target;


	void Start()
	{
		target = References.realtimeCamera.transform;
        StartCoroutine(faceCamera());
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
