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
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.velocity = transform.forward * Mathf.Clamp(rigidbody.velocity.magnitude + playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rigidbody.velocity = -transform.forward * Mathf.Clamp(rigidbody.velocity.magnitude + playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
            }
            else rigidbody.velocity = rigidbody.velocity * Mathf.Clamp(rigidbody.velocity.magnitude - playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);

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
