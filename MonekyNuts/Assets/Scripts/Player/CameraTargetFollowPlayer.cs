using UnityEngine;
using System.Collections;

public class CameraTargetFollowPlayer : MonoBehaviour {

    [SerializeField]
    Transform target;

    void Update()
    {
        transform.position = target.position;
    }
}
