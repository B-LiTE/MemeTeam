using UnityEngine;
using System.Collections;

public class OneTimeSoundEffect : MonoBehaviour {

	AudioSource sound;

	void Start () 
	{
		sound = GetComponent<AudioSource> ();
		sound.loop = false;
		sound.Play ();
		StartCoroutine (destoySelf ());
	}

	IEnumerator destoySelf()
	{
		yield return new WaitForSeconds(sound.clip.length + 0.25f);
		Destroy (gameObject);
	}

}
