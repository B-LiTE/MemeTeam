using UnityEngine;
using System.Collections;

public class LevitationBehavior : MonoBehaviour {

	public float levitationDistance;
	public float levitationSpeed;
	public float levitationSpeedCurrent;
	public float speedChangeRate;
	
	public Vector3 startPoint;
	private bool movingUp;

	private bool slowing;

	Vector3 top;
	Vector3 bottom;
	

	void Start () 
	{
		startPoint = transform.position;
		top = new Vector3 (startPoint.x,startPoint.y + levitationDistance, startPoint.z);
		bottom = new Vector3 (startPoint.x,startPoint.y - levitationDistance, startPoint.z);
		movingUp = true;
		slowing = true;
		levitationSpeedCurrent = levitationSpeed;
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
		if (transform.position.y > startPoint.y) {
			if (movingUp) {
				slowing = true;
			} else
				slowing = false;
		} 
		else 
		{
			if(movingUp)
			{
				slowing = false;
			}
			else slowing = true;
		}
		if (slowing && levitationSpeedCurrent > levitationSpeed * 0.2) {
			levitationSpeedCurrent -= speedChangeRate;
		} 
		else if (levitationSpeedCurrent < levitationSpeed) 
		{
			levitationSpeedCurrent += speedChangeRate;
		}
			if(movingUp)
		{
			transform.position = Vector3.MoveTowards(transform.position, top, levitationSpeedCurrent * Time.deltaTime);
			//transform.position = new Vector3(transform.position.x,transform.position.y + levitationSpeed * Time.deltaTime, transform.position.z);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, bottom, levitationSpeedCurrent * Time.deltaTime);
			//GetComponent<Rigidbody> ().MovePosition(bottom);
			//transform.position = new Vector3(transform.position.x,transform.position.y - levitationSpeed * Time.deltaTime, transform.position.z);
		}
	}
}
