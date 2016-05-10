using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    GameObject loading;

    [SerializeField]
    string[] buttonReferences;

    void Awake()
    {
        assign(false);
    }

    void assign(bool value)
    {
        loading = GameObject.FindGameObjectWithTag("Loading");
        loading.GetComponentInChildren<Image>().enabled = value;
        loading.GetComponentInChildren<Text>().enabled = value;
    }

    public void startGame()
    {
        References.resetLives();
        Destroy(GameObject.FindGameObjectWithTag("ImmortalMusic"));
        assign(true);
        Application.LoadLevel(buttonReferences[0]);
    }

    public void helpManual()
    {
        assign(true);
        Application.LoadLevel(buttonReferences[1]);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
