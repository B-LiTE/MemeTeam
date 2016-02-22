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

    [SerializeField]
    UnityEngine.UI.Image leftGradient, rightGradient;

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
        if (References.stateManager.CurrentState == StateManager.states.realtime)
        {
            realtimeCamera.enabled = true;
            rotationCoroutine = StartCoroutine(checkRotation());

            Vector3 leftBound = realtimeCamera.ViewportToScreenPoint(new Vector3(0.1f, 0, 0));
            Vector3 rightBound = realtimeCamera.ViewportToScreenPoint(new Vector3(0.9f, 0, 0));

            leftGradient.rectTransform.rect.Set(0, 0, leftBound.x, Screen.height);
            rightGradient.rectTransform.rect.Set(rightBound.x, 0, Screen.width, Screen.height);

            leftGradient.enabled = true;
            rightGradient.enabled = true;
        }
        else
        {
            if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
            realtimeCamera.enabled = false;
            leftGradient.enabled = false;
            rightGradient.enabled = false;
        }
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
