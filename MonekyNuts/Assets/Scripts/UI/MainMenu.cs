using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string[] buttonReferences;

    public void startGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("ImmortalMusic"));
        Application.LoadLevel(buttonReferences[0]);
    }

    public void helpManual()
    {
        Application.LoadLevel(buttonReferences[1]);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
