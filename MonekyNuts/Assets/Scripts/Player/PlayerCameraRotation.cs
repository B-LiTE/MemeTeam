using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PlayerCameraRotation : MonoBehaviour {

    bool rotating;
    Vector2 startingPoint;
    Camera camera;

    // Reference to coroutine
    Coroutine rotationCoroutine;

    void Awake()
    {
        camera = GetComponentInChildren<Camera>();
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
            currentRotation = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (currentRotation.x <= 0.1f || currentRotation.x >= 0.9f )
                {
                    rotating = true;
                    startingPoint = currentRotation;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                rotating = false;
                startingPoint = Vector2.zero;
            }

            if (rotating)
            {
                float difference = currentRotation.x - startingPoint.x;
                Debug.Log(difference);
            }

            yield return null;
        }
    }
}
