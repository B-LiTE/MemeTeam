using UnityEngine;
using System.Collections;

public class PlayerCameraRotation : MonoBehaviour {

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
        else
        {
            if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
        }
    }

    IEnumerator checkRotation()
    {
        Vector2 currentRotation;
        while (true)
        {
            currentRotation = camera.ScreenToViewportPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentRotation.x <= 0.1f || currentRotation.x >= 0.9f )
                {
                    rotating = true;
                    lastPoint = currentRotation;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                rotating = false;
                lastPoint = Vector2.zero;
            }

            if (rotating)
            {
                float difference = currentRotation.x - lastPoint.x;
                transform.RotateAround(References.player.transform.position, Vector3.up, difference * magnitudeOfRotation);
                lastPoint = currentRotation;
            }

            yield return null;
        }
    }
}
