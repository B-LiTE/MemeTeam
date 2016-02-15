using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoTimer : MonoBehaviour {

    public Text timer;

    public int time;
    Coroutine timeroutine;
    public bool strat;

    void Awake()
    {
        strat = true;
        time = 4;
        timeroutine = StartCoroutine(doTime());
    }

    IEnumerator doTime()
    {
        while (true)
        {
            timer.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;

            if (time <= 0)
            {
                if (strat) time = 30;
                else time = 4;
                strat = !strat;
            }
        }
    }
}
