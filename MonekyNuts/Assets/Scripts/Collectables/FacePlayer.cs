using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour {

    public Transform target;
    public Vector3 lookAt;
	
	// Update is called once per frame
	void Update () 
    {
        lookAt = new Vector3(target.position.x, transform.position.y, target.position.z);
       
        transform.LookAt(lookAt);
        
        
	}
}
