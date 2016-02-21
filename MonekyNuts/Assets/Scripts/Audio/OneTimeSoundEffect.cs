using UnityEngine;
using System.Collections;

public class OneTimeSoundEffect : MonoBehaviour {

	AudioSource sound;

	void Start () 
	{
		sound = GetComponent<AudioSource> ();
		sound.loop = false;
	}
	void Update()
	{
		if (!sound.isPlaying) 
		{
			Destroy(gameObject);
		}
	}

}
