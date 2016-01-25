using UnityEngine;
using System.Collections;

public class FadeInFromClear : MonoBehaviour {
	
	SpriteRenderer sprite;
	float fadeRate;
	
	void Start () 
	{
		fadeRate = 0.1f;
		sprite = GetComponent<SpriteRenderer>();
		StartCoroutine(FadeIn ());
	}
	IEnumerator FadeIn()
	{
		while(sprite.color.a < 1)
		{
			sprite.color = new Color(sprite.color.r + fadeRate,sprite.color.g + fadeRate,sprite.color.b + fadeRate, sprite.color.a + fadeRate);
			yield return null;
		}
		
	}
	
}

