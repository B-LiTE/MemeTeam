using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

    void Start()
    {
        if (GameObject.FindObjectsOfType<Quit>().Length > 1) Destroy(this.gameObject);
        else DontDestroyOnLoad(this.gameObject);

        StartCoroutine(checkQuit());
    }

    IEnumerator checkQuit()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape)) Application.Quit();

            yield return null;
        }
    }
}
