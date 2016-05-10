using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class KeepOnGround : MonoBehaviour {

    [SerializeField]
    bool grounded;

    void Start()
    {
        groundEntity();

        grounded = !Application.isPlaying;
        this.enabled = !Application.isPlaying;
    }

    void Update()
    {
        if (grounded) groundEntity();
    }

    void groundEntity()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hitInfo)) transform.position = hitInfo.point + new Vector3(0, transform.localScale.y, 0);
        else if (Physics.Raycast(new Ray(transform.position, Vector3.up), out hitInfo)) transform.position = hitInfo.point + new Vector3(0, transform.localScale.y, 0);
    }

}
