using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerStats))]
public class PlayerKeyboardControls : MonoBehaviour {

    PlayerStats playerStats;
    Rigidbody rigidbody;

    void Awake()
    {
        References.stateManager.changeState += onChangeState;
        playerStats = GetComponent<PlayerStats>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) StartCoroutine(checkKeys());
    }

    IEnumerator checkKeys()
    {
        while (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            // Movement
            if (Input.GetKey(KeyCode.W))
            {
                // Set velocity in the forward direction equal to the current velocity magnitude + acceleration, clamped between 0 and maxMoveSpeed
                rigidbody.velocity = transform.forward.normalized * Mathf.Clamp(rigidbody.velocity.magnitude + playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
            }
            /*else
            {
                rigidbody.velocity = -transform.forward.normalized * Mathf.Clamp(rigidbody.velocity.magnitude - playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);

                if (Input.GetKey(KeyCode.S))
                {
                    // Set velocity in the backward direction equal to the current velocity magnitude + acceleration, clamped between 0 and maxMoveSpeed
                    rigidbody.velocity = transform.forward.normalized * Mathf.Clamp(rigidbody.velocity.magnitude - playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
                }
            }*/
            else if (Input.GetKey(KeyCode.S))
            {
                // Set velocity in the backward direction equal to the current velocity magnitude + acceleration, clamped between 0 and maxMoveSpeed
                rigidbody.velocity = -transform.forward.normalized * Mathf.Clamp(rigidbody.velocity.magnitude + playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
            }
                // Set velocity in the current direction equal to the current velocity magnitude - (2 * acceleration), clamped between 0 and maxMoveSpeed
            else rigidbody.velocity = rigidbody.velocity.normalized * Mathf.Clamp(rigidbody.velocity.magnitude - playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);




            // Rotation
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - playerStats.rotationSpeed, transform.rotation.eulerAngles.z));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigidbody.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + playerStats.rotationSpeed, transform.rotation.eulerAngles.z));
            }




            yield return null;
        }

        rigidbody.velocity = Vector3.zero;
    }
}
