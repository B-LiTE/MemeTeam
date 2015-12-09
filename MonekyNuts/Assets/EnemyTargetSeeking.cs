using UnityEngine;
using System.Collections;

public class EnemyTargetSeeking : MonoBehaviour {

    [SerializeField]
    bool isScanning;

    [SerializeField]
    float fieldOfVision;

    Coroutine scanningCoroutine;

    public void startScanning()
    {
        scanningCoroutine = StartCoroutine(test());
    }

    public void stopScanning()
    {
        StopCoroutine(scanningCoroutine);
    }

    IEnumerator test()
    {
        while (true)
        {
            // If it is scanning...
            if (isScanning)
            {
                // ...and it found an attackable entity...
                // If it's within range, start attacking
                // If it's outside of range, set the destination to move towards it
            }
            // If it isn't scanning...
            // ...and it isn't moving or rotating...
            // Set a random destination
        }
    }
}
