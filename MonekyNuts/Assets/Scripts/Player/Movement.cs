using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    // Reference to state manager
    StateManager manager;

    // Reference to rigidbody
    Rigidbody rigidbody;

    // Position to travel to
    Vector3 destination;

    void Awake()
    {
        // Set up references
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();
        rigidbody = GetComponent<Rigidbody>();
        destination = transform.position;
    }

    void Start()
    {
        StartCoroutine(checkKeys());
    }

    IEnumerator checkKeys()
    {
        float forwardVelocity, horizontalVelocity;

        while (manager.currentState == StateManager.states.realtime)
        {
            if (Input.GetKey(KeyCode.W))
                forwardVelocity = 5;
            else if (Input.GetKey(KeyCode.S))
                forwardVelocity = -5;
            else forwardVelocity = 0;

            if (Input.GetKey(KeyCode.D))
                horizontalVelocity = 5;
            else if (Input.GetKey(KeyCode.A))
                horizontalVelocity = -5;
            else horizontalVelocity = 0;

            rigidbody.velocity = new Vector3(horizontalVelocity, rigidbody.velocity.y, forwardVelocity);

            yield return null;
        }
    }
}
