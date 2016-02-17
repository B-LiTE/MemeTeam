using UnityEngine;
using System.Collections;

public class GameOverAnimations : MonoBehaviour {

    GameObject castle;
    Vector3 startPos;

    [SerializeField]
    UnityEngine.UI.Text text;

    [SerializeField]
    UnityEngine.UI.Button button;

    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
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

        StartCoroutine(fadeInWords());
    }

    IEnumerator fadeInWords()
    {
        float t = 0;
        while (t < 1)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);
            t += 0.05f;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        button.gameObject.SetActive(true);
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

    public void mainMenu()
    {
        Application.LoadLevel("Menu");
    }

}
