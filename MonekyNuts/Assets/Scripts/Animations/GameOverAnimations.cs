using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverAnimations : MonoBehaviour {

    GameObject castle;
    Vector3 startPos;

    [SerializeField]
    UnityEngine.UI.Text youLoseText, livesText;

    [SerializeField]
    UnityEngine.UI.Button mainMenuButton, lastLevelButton;

    void Start()
    {
        youLoseText.color = new Color(youLoseText.color.r, youLoseText.color.g, youLoseText.color.b, 0);
        livesText.color = new Color(livesText.color.r, livesText.color.g, livesText.color.b, 0);
        mainMenuButton.gameObject.SetActive(false);
        lastLevelButton.gameObject.SetActive(false);
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

        if (References.lives > 0) livesText.text = "Or, you have\n" + References.lives + " " + ((References.lives > 1) ? "lives" : "life") + " left...";
        StartCoroutine(fadeInWords());
    }

    IEnumerator fadeInWords()
    {
        float t = 0;
        while (t < 1)
        {
            youLoseText.color = new Color(youLoseText.color.r, youLoseText.color.g, youLoseText.color.b, t);
            if (References.lives > 0) livesText.color = new Color(livesText.color.r, livesText.color.g, livesText.color.b, t);

            t += 0.05f;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        mainMenuButton.gameObject.SetActive(true);
        if (References.lives > 0) lastLevelButton.gameObject.SetActive(true);
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

    public void loadLastLevel()
    {
        GameObject loading = GameObject.FindGameObjectWithTag("Loading");
        loading.GetComponentInChildren<Image>().enabled = true;
        loading.GetComponentInChildren<Text>().enabled = true;
        Application.LoadLevel(References.lastLevelIndex);
    }

}
