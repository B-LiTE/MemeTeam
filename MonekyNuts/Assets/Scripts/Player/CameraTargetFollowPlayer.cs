using UnityEngine;
using System.Collections;

public class CameraTargetFollowPlayer : MonoBehaviour {

    Transform target;

    void Start()
    {
        target = References.player.transform;
    }

    void Update()
    {
        transform.position = target.position;
    }
}
