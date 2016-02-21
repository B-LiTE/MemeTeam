using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string[] buttonReferences = new string[] { "Kenny's Level", "Help Manual", "Quit" };

    public void startGame()
    {
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
