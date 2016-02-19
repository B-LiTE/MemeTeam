using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public AudioSource audioSource;

  	[SerializeField]
	AudioClip[] backgroundMusic = new AudioClip[10];
	//0 == strategy
	//1 ==  realtime

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		References.stateManager.changeState += ChangeMusic;
	}
	void ChangeMusic()
	{
		if (References.stateManager.CurrentState == StateManager.states.realtime) 
		{
			RealtimeMusic();
		} 
		else if (References.stateManager.CurrentState == StateManager.states.strategy) 
		{
			StrategicMusic();
		}
	}
	void RealtimeMusic()
	{
		audioSource.Stop();
		audioSource.clip = backgroundMusic[1];
		audioSource.Play();
		audioSource.loop = true;
	}
	void StrategicMusic()
	{
		audioSource.Stop();
		audioSource.clip = backgroundMusic[0];
		audioSource.Play();
		audioSource.loop = true;
	}


}
