using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryAnimations : MonoBehaviour {

	//objects we're making appear
	[SerializeField]
	Button button;
	[SerializeField]
	Text text;
	[SerializeField]
	GameObject castle;

	float beatRate = .8f;

	float fadeRate = .5f; //the rate at which the text appears

	public void Awake() //objects start off transparent
	{
		button.gameObject.SetActive(false);
		text.color = new Color(text.color.r,text.color.g,text.color.b,0);
		StartCoroutine(Wait ());
	}
	IEnumerator Wait() //duration waits before the objects begin to appear
	{
		yield return new WaitForSeconds(3);
		StartCoroutine(FadeInVictory());
	}
	IEnumerator FadeInVictory()
	{
		while(text.color.a < 1) //color becomes more opaque every frame
		{
			text.color = new Color(text.color.r,text.color.g,text.color.b,text.color.a + (fadeRate * Time.deltaTime));
			yield return null;
		}
		button.gameObject.SetActive(true);
		
		yield return null;
	}
	IEnumerator Beat()
	{
		while(true)
		{
			castle.transform.localScale = new Vector3(1,1,1);
			yield return new WaitForSeconds(beatRate);
			castle.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
			yield return null;
		}
	}
	public void BackToMenu() //upon clicking the button, return to menu
	{
		Application.LoadLevel("Menu");
	}
}
