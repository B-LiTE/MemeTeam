using UnityEngine;
using System.Collections;

public class Rotate_Weapons : MonoBehaviour {

    bool isRotating = false;
    float startingRotation;
    float amountRotated;
    float stoppedRotation;
    public float rotationRate = 40;
	 
	int activeSlot;

    public Inventory inventory;

	void Start()
	{
		activeSlot = 1;
	}
    void RotateWeaponsLeft()
    {
        if (!isRotating)
        {
			//ThrowActiveItem();
            stoppedRotation = 45;
            isRotating = true;
            amountRotated = 0;
			StartCoroutine(RotateLeft());
			ThrowActiveItem();
        }
    }
	IEnumerator RotateLeft()
	{
		if (activeSlot > 1)
			activeSlot -= 1;
		else
			activeSlot = 8;
		while(isRotating)
		{
			
			if (amountRotated < stoppedRotation)
			{
				transform.Rotate(new Vector3(0, 0, -(rotationRate * Time.deltaTime)));
				amountRotated += rotationRate * Time.deltaTime;
			}
			else
			{
				isRotating = false;
				transform.Rotate(new Vector3(0, 0, -(stoppedRotation - amountRotated)));
			}
			yield return null;
		}
	}
	void RotateWeaponsRight()
	{
		if (!isRotating)
		{
			stoppedRotation = -45;
			isRotating = true;
			amountRotated = 0;
			StartCoroutine(RotateRight());
			ThrowActiveItem();
		}
	}
	IEnumerator RotateRight()
	{
		if (activeSlot < 8)
			activeSlot += 1;
		else
			activeSlot = 1;
		while(isRotating)
		{
			
			if (amountRotated > stoppedRotation)
			{
				transform.Rotate(new Vector3(0, 0, (rotationRate * Time.deltaTime)));
				amountRotated -= rotationRate * Time.deltaTime;
			}
			else
			{
				isRotating = false;
				transform.Rotate(new Vector3(0, 0, -(stoppedRotation - amountRotated)));
			}
			yield return null;
		}
	}
	public void ThrowActiveItem()
	{
		FindObjectOfType<PlayerStats> ().ChangeActiveWeapon (activeSlot);
	}
}
