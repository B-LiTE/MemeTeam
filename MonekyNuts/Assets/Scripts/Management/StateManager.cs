using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    [SerializeField]
    string nextLevelName;

    [SerializeField]
    UnityEngine.UI.Text winText;

    [SerializeField]
    GameObject loadingScreen;

    public enum states { strategy, realtime };
    [SerializeField]
    states currentState;
    public states CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            if (changeState != null) changeState();
            //pause(currentState != states.realtime);
        }
    }

    bool isPaused;

    public delegate void changeStateDelegate();
    public event changeStateDelegate changeState;

    void Awake()
    {
        References.resetReferences();

        StartCoroutine(gameStartChangeState());
        isPaused = false;
    }

    void Start()
    {
        StartCoroutine(checkPause());
        StartCoroutine(checkEnemyCount());
    }

    IEnumerator gameStartChangeState()
    {
        yield return new WaitForEndOfFrame();
        CurrentState = states.strategy;
    }

    public void pause(bool shouldPause)
    {
        isPaused = shouldPause;
        if (shouldPause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }












    IEnumerator checkPause()
    {
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isPaused = !isPaused;
                pause(isPaused);
            }

            yield return null;
        }
    }















    IEnumerator checkEnemyCount()
    {
        while(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(1);
        }
        
        StartCoroutine(showWinBeforeNext());
    }



    IEnumerator showWinBeforeNext()
    {
        float t = 0;
        winText.enabled = true;

        while (t <= 1)
        {
            winText.color = new Color(winText.color.r, winText.color.g, winText.color.b, t);

            t += 0.05f;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        loadNextLevel();
    }



    public void loadNextLevel()
    {
        loadingScreen.SetActive(true);

        if (nextLevelName != null) Application.LoadLevel(nextLevelName);
        else Application.LoadLevel(0);
    }


















    public void loadLoseLevel()
    {
        References.lives--;
        Application.LoadLevel("Game Over");
    }
}
