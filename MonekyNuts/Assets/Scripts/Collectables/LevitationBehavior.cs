using UnityEngine;
using System.Collections;

public class LevitationBehavior : MonoBehaviour {

	public float levitationDistance;
	public float levitationSpeed;
	public Vector3 startPoint;
	private bool movingUp;
	Vector3 top;
	Vector3 bottom;
	

	void Start () 
	{
		startPoint = transform.position;
		top = new Vector3 (startPoint.x,startPoint.y + levitationDistance, startPoint.z);
		bottom = new Vector3 (startPoint.x,startPoint.y - levitationDistance, startPoint.z);
		movingUp = true;
	}
	

	void Update () 
	{
		if (transform.position.y >= top.y) 
		{
			movingUp = false;
		}
		else if (transform.position.y <= bottom.y) 
		{
			movingUp = true;
		}
		if(movingUp)
		{
			transform.position = Vector3.MoveTowards(transform.position, top, levitationSpeed * Time.deltaTime);
			//transform.position = new Vector3(transform.position.x,transform.position.y + levitationSpeed * Time.deltaTime, transform.position.z);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, bottom, levitationSpeed * Time.deltaTime);
			//GetComponent<Rigidbody> ().MovePosition(bottom);
			//transform.position = new Vector3(transform.position.x,transform.position.y - levitationSpeed * Time.deltaTime, transform.position.z);
		}
	}
}
