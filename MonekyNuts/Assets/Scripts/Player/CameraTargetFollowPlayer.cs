using UnityEngine;
using System.Collections;

public class CameraTargetFollowPlayer : MonoBehaviour {

    // The target that we are going to follow
    Transform target;

    void Start()
    {
        // Set up the target reference
        target = References.player.transform;
    }

    void Update()
    {
        // Move to the target position
        transform.position = target.position;
    }
}
