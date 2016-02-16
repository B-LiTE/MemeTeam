using UnityEngine;
using System.Collections;

public class GameOverAnimations : MonoBehaviour {

    GameObject castle;
    Vector3 startPos;

    void Start()
    {
        StartCoroutine(startGameOverAnimations());
    }

    IEnumerator startGameOverAnimations()
    {
        castle = GameObject.FindGameObjectWithTag("Castle");
        startPos = castle.transform.position;

        StartCoroutine(vibrate());

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(sink());

        yield return new WaitForSeconds(2.5f);

        Debug.Log("show you lose");
    }

    IEnumerator vibrate()
    {
        while (true)
        {
            castle.transform.position = newPosition();
            yield return null;
        }
    }

    IEnumerator sink()
    {
        while (true)
        {
            castle.transform.position = new Vector3(castle.transform.position.x, castle.transform.position.y - 0.05f, castle.transform.position.z);
            yield return null;
        }
    }

    Vector3 newPosition()
    {
        float ranX = Random.Range(-.1f, .1f);
        float ranZ = Random.Range(-.1f, .1f);

        return new Vector3(startPos.x + ranX, castle.transform.position.y, startPos.z + ranZ);
    }

}
