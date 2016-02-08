using UnityEngine;
using System.Collections;

public class FadeOutDestroy : MonoBehaviour {

	SpriteRenderer sprite;
	float fadeRate = 0.008f;

	void Start()
	{
		sprite = GetComponent<SpriteRenderer> ();
	}
	void Update () 
	{
		sprite.color = new Color (sprite.color.r,sprite.color.g,sprite.color.b,sprite.color.a - fadeRate);

		if(sprite.color.a <= 0) Destroy (this.gameObject);
	}
}
