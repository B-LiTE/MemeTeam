using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerStats))]
public class PlayerKeyboardControls : MonoBehaviour {

    PlayerStats playerStats;
    PlayerKeyboardAttacks playerAttacks;
    Rigidbody rigidbody;
    Animations animations;

    [SerializeField]
    float groundedMaxYVelocity, airMaxYVelocity, minYVelocity, distanceCheck;

    void Awake()
    {
        References.stateManager.changeState += onChangeState;
        playerStats = GetComponent<PlayerStats>();
        playerAttacks = GetComponent<PlayerKeyboardAttacks>();
        rigidbody = GetComponent<Rigidbody>();
        animations = GetComponentInChildren<Animations>();

        groundedMaxYVelocity = 5;
        airMaxYVelocity = -15;
        minYVelocity = -100;
        distanceCheck = 2.25f;
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) StartCoroutine(checkKeys());
    }

    IEnumerator checkKeys()
    {
        Vector3 movementVector;
        float movementMagnitude, yVelocity;
        while (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            movementVector = transform.forward.normalized;
            movementMagnitude = Mathf.Clamp(rigidbody.velocity.magnitude + playerStats.movementAcceleration, 0, playerStats.maxMoveSpeed);
            yVelocity = Mathf.Clamp(rigidbody.velocity.y, minYVelocity, (isGrounded() ? groundedMaxYVelocity : airMaxYVelocity));

            // Movement
            if (Input.GetKey(KeyCode.W))
            {
                animations.playRunningAnimation();

                // Set velocity in the forward direction equal to the current velocity magnitude + acceleration, clamped between 0 and maxMoveSpeed
                rigidbody.velocity = new Vector3(movementVector.x * movementMagnitude,
                    yVelocity,
                    movementVector.z * movementMagnitude);
                playerStats.rotationSpeed = 2;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                animations.playRunningAnimation();

                // Set velocity in the backward direction equal to the current velocity magnitude + acceleration, clamped between 0 and maxMoveSpeed
                rigidbody.velocity = new Vector3(-movementVector.x * movementMagnitude,
                    yVelocity,
                    -movementVector.z * movementMagnitude);
                playerStats.rotationSpeed = 2;
            }
            // Set velocity in the current direction equal to the current velocity magnitude - (2 * acceleration), clamped between 0 and maxMoveSpeed
            else
            {
                rigidbody.velocity = new Vector3(
                    rigidbody.velocity.normalized.x * tendTowardsZero(rigidbody.velocity.magnitude, playerStats.movementAcceleration),
                    yVelocity,
                    rigidbody.velocity.normalized.z * tendTowardsZero(rigidbody.velocity.magnitude, playerStats.movementAcceleration));
                playerStats.rotationSpeed = 3.25f;

                animations.turnOffAll();
            }




            // Rotation
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - playerStats.rotationSpeed, transform.rotation.eulerAngles.z));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigidbody.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + playerStats.rotationSpeed, transform.rotation.eulerAngles.z));
            }




            // Attacks
            if (Input.GetMouseButton(0))
            {
                animations.playPunchingAnimation();
                if (playerStats.activeItem != null && playerStats.activeItem.itemType == "Weapon")
                {
                    Weapon_Item temp = playerStats.activeItem as Weapon_Item;

                    if (temp.thisWeaponType != Weapon_Item.weaponType.sword)
                    {
                        playerAttacks.startThrowArrowCoroutine();
                    }
                    else
                    {
                        playerAttacks.startAttackSwordCoroutine();
                    }
                }
                else
                {
                    playerAttacks.startAttackSwordCoroutine();
                }
                
            }



            yield return null;
        }

        rigidbody.velocity = Vector3.zero;
    }

    bool isGrounded()
    {
        return Physics.Raycast(new Ray(transform.position, Vector3.down), distanceCheck);
    }

    float tendTowardsZero(float number, float step)
    {
        step = Mathf.Abs(step);

        if (number > 0)
        {
            number -= step;
            if (number < 0) number = 0;
        }
        else
        {
            number += step;
            if (number > 0) number = 0;
        }

        return number;
    }
}
