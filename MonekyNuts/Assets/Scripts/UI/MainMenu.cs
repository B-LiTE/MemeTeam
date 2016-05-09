using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string[] buttonReferences;

    [SerializeField]
    GameObject loading;

    public void startGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("ImmortalMusic"));
        loading.SetActive(true);
        Application.LoadLevel(buttonReferences[0]);
    }

    public void helpManual()
    {
        loading.SetActive(true);
        Application.LoadLevel(buttonReferences[1]);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
