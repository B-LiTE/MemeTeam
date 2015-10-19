using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {

    // Reference to state manager
    StateManager manager;

    // Reference to rigidbody
    NavMeshAgent navigation;

    // Position to travel to
    [SerializeField]
    Vector3 destination;

    // Reference to coroutine
    Coroutine movementCoroutine;

    void Awake()
    {
        // Set up references
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();
        navigation = GetComponent<NavMeshAgent>();
        destination = transform.position;
    }

    void Start()
    {
        manager.changeState += onStateChange;
    }

    void onStateChange()
    {
        if (manager.CurrentState == StateManager.states.realtime) movementCoroutine = StartCoroutine(checkMovement());
        else
        {
            Debug.Log("stopping coroutine");
            if (movementCoroutine != null) StopCoroutine(movementCoroutine);
            Debug.Log("coroutine (should be) stopped");
        }
    }

    IEnumerator checkMovement()
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse button/touch detected");
                RaycastHit hitInfo;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100))
                {
                    Debug.Log("Physics raycast hit something!");
                    Debug.Log("hit: " + hitInfo.transform.gameObject.name);
                    if (hitInfo.transform.tag == "Terrain")
                    {
                        Debug.Log("changing destination...");
                        destination = hitInfo.point;
                    }
                }
            }

            if (transform.position != destination)
                navigation.SetDestination(destination);

            yield return null;
        }
    }
}
