using UnityEngine;
using System.Collections;

public class LastingAudio : MonoBehaviour {


	void Start () 
	{
		if(GameObject.FindGameObjectsWithTag("ImmortalMusic").Length > 1) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
	

}
