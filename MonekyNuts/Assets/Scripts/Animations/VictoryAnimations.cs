using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryAnimations : MonoBehaviour {

	[SerializeField]
	Button button;
	[SerializeField]
	Text text;


	float fadeRate = .5f;

	public void Awake()
	{
		button.gameObject.SetActive(false);
		text.color = new Color(text.color.r,text.color.g,text.color.b,0);
		StartCoroutine(Wait ());
	}
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(3);
		StartCoroutine(FadeInVictory());
	}
	IEnumerator FadeInVictory()
	{
		while(text.color.a < 1)
		{
			text.color = new Color(text.color.r,text.color.g,text.color.b,text.color.a + (fadeRate * Time.deltaTime));
			yield return null;
		}
		button.gameObject.SetActive(true);
		
		yield return null;
	}
	public void BackToMenu()
	{
		Application.LoadLevel("Menu");
	}
}
