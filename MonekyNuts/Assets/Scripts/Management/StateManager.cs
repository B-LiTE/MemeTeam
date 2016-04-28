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
        //References.resetReferences();

        StartCoroutine(gameStartChangeState());
        isPaused = false;
    }

    void Start()
    {
        //StartCoroutine(checkCheats());
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
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isPaused = !isPaused;
                pause(isPaused);
            }

            yield return null;
        }
    }

    IEnumerator checkCheats()
    {
        while (true)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                loadingScreen.SetActive(true);
                Application.LoadLevel("Victory");
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                loadLoseLevel();
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                StartCoroutine(showWinBeforeNext());
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                Cheats.nextLevelWithoutLoad();
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                References.gameStats.ChangeGoldCount(250);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {

            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                References.castle.GetComponent<Castle>().ChangeHealth(1000);
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                References.player.GetComponent<PlayerStats>().ChangeHealth(1000);
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {
                loadingScreen.SetActive(true);
                Application.LoadLevel(0);
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                Time.timeScale = 0.25f;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                Time.timeScale = 1;
            }
            else if (Input.GetKeyUp(KeyCode.V))
            {
                Time.timeScale = 2.5f;
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
