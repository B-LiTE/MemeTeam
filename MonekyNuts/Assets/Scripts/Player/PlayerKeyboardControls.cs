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
                if (Input.GetKey(KeyCode.LeftShift)) rigidbody.velocity = transform.forward * playerStats.movementSpeed * 2;
                else rigidbody.velocity = transform.forward * playerStats.movementSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift)) rigidbody.velocity = -transform.forward * playerStats.movementSpeed * 2;
                else rigidbody.velocity = -transform.forward * playerStats.movementSpeed;
            }
            else rigidbody.velocity = Vector3.zero;

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
