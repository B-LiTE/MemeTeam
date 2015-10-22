using UnityEngine;
using System.Collections;

public class PlayerCameraRotation : MonoBehaviour {

    public bool rotating;
    Vector2 lastPoint;
    Camera camera;
    [SerializeField]
    float magnitudeOfRotation;
    float distanceFromPlayer;
    Vector3 playerDestination;

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
        distanceFromPlayer = Vector3.Distance(References.player.transform.position, transform.position);
        playerDestination = References.player.transform.position;
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
                camera.transform.RotateAround(playerDestination, Vector3.up, difference * magnitudeOfRotation);
                lastPoint = currentRotation;
            }

            yield return null;
        }
    }

    IEnumerator moveCamera(Vector3 destination)
    {
        float time = 0;
        Vector3 startingPosition = transform.position;
        while(transform.position != destination)
        {
            transform.position = Vector3.Lerp(startingPosition, destination, time);
            time += 0.01f;
            yield return null;
        }
    }

    public void startMovingCamera(Vector3 playerStartPosition, Vector3 playerEndPosition)
    {
        float xDifference = playerEndPosition.x - playerStartPosition.x;
        float zDifference = playerEndPosition.z - playerStartPosition.z;

        StartCoroutine(moveCamera(new Vector3(transform.position.x + xDifference, transform.position.y, transform.position.z + zDifference)));

        playerDestination = playerEndPosition;
    }
}
