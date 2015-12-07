using UnityEngine;
using System.Collections;

public class PlayerCameraRotation : MonoBehaviour {
    
    [SerializeField]
    public bool rotating;
    Vector2 lastPoint;
    Camera camera;
    [SerializeField]
    float magnitudeOfRotation;

    // Reference to coroutine
    Coroutine rotationCoroutine;

    void Awake()
    {
        camera = GetComponent<Camera>();
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
            currentMousePosition = camera.ScreenToViewportPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentMousePosition.x <= 0.1f || currentMousePosition.x >= 0.9f)
                {
                    rotating = true;
                    lastPoint = currentMousePosition;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                rotating = false;
                lastPoint = Vector2.zero;
            }

            if (rotating)
            {
                float difference = -(currentMousePosition.x - lastPoint.x);
                transform.RotateAround(References.player.transform.position, Vector3.up, difference * magnitudeOfRotation);
                lastPoint = currentMousePosition;
            }

            yield return null;
        }
    }
}
