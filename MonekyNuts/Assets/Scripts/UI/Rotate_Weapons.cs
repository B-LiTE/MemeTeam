using UnityEngine;
using System.Collections;

public class Rotate_Weapons : MonoBehaviour {

    bool isRotating = false;
    float startingRotation;
    float amountRotated;
    float stoppedRotation;
    public float rotationRate = 40;

    public Inventory inventory;

	void Update () 
    {
        if (isRotating)
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
        }
	
	}
    void RotateWeapons()
    {
        if (!isRotating)
        {
            stoppedRotation = 45;
            isRotating = true;
            amountRotated = 0;
        }

        if (inventory.activeSlot < 7)
        {
            inventory.activeSlot++;
        }
        else inventory.activeSlot = 0;

    }
}
