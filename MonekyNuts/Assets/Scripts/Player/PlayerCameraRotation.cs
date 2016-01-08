using UnityEngine;
using System.Collections;

public class PlayerCameraRotation : MonoBehaviour {
    
    // Whether we are rotating or not
    [SerializeField]
    public bool rotating;

    // The last point the mouse occupied
    Vector2 lastPoint;

    // Reference to realtime camera
    Camera realtimeCamera;

    // Amount to rotate by
    [SerializeField]
    float magnitudeOfRotation;

    // Reference to coroutine
    Coroutine rotationCoroutine;

    void Awake()
    {
        realtimeCamera = GetComponent<Camera>();
    }

    void Start()
    {
        References.stateManager.changeState += onStateChange;
        rotating = false;
    }

    void onStateChange()
    {
        if (References.stateManager.CurrentState == StateManager.states.realtime) rotationCoroutine = StartCoroutine(checkRotation());
        else if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
    }

    IEnumerator checkRotation()
    {
        Vector2 currentMousePosition;
        while (true)
        {
            // Set the current mouse position
            currentMousePosition = realtimeCamera.ScreenToViewportPoint(Input.mousePosition);

            // If the mouse button was clicked...
            if (Input.GetMouseButtonDown(0))
            {
                // ...and the mouse is within the "rotating" bounds of the sides of the screen...
                if (currentMousePosition.x <= 0.1f || currentMousePosition.x >= 0.9f)
                {
                    // Start rotating
                    rotating = true;

                    // Set the last mouse point to the current mouse point
                    lastPoint = currentMousePosition;
                }
            }
            // If the mouse button was let go...
            else if (Input.GetMouseButtonUp(0))
            {
                // Stop rotating
                rotating = false;

                // Set the last mouse point to zero
                lastPoint = Vector2.zero;
            }

            // If we are rotating...
            if (rotating)
            {
                // Calculate the difference between the last mouse point and the current point
                float difference = -(currentMousePosition.x - lastPoint.x);

                // Rotate the camera around the player
                transform.RotateAround(References.player.transform.position, Vector3.up, difference * magnitudeOfRotation);

                // Set the last mouse point to the current mouse point in preparation for the next rotation
                lastPoint = currentMousePosition;
            }

            yield return null;
        }
    }
}
