using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FillScreen : MonoBehaviour {

	RectTransform image; //the image you are scaling

	public int widthBuffer;
	public int heightBuffer;

	void Start()
	{
		image = GetComponent<RectTransform>();
	}
	void Update () 
	{
		image.sizeDelta = new Vector2(Screen.width - widthBuffer,Screen.height - heightBuffer); //adjusts this image to fit the screen every frame
	}
}
