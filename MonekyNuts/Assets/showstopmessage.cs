using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showstopmessage : MonoBehaviour {

    [SerializeField]
    GameObject stopMessage;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(show());
        }
    }

    IEnumerator show()
    {
        stopMessage.SetActive(true);
        yield return new WaitForSeconds(2);
        stopMessage.SetActive(false);
    }

}
