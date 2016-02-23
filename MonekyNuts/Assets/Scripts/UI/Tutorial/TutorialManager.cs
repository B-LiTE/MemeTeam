using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	//the different buttons in the tutorial
	public GameObject previous;
	public GameObject backMenu;
	public GameObject next;

	public GameObject slide;

	public Sprite[] slides; //the slides for each part of the tutorial
	public int slidesCount;
	int slideIndex;

	void Start()
	{
		previous.SetActive(false);
		slideIndex = 0;
		slide.GetComponent<Image>().sprite = slides[slideIndex];
	}
	void ChangeSlideLeft() //displays the previous tutorial slide
	{
		if(slideIndex > 0)
		{
			slideIndex--;
			slide.GetComponent<Image>().sprite = slides[slideIndex];
			next.SetActive(true);
			if(slideIndex == 0)
			{
				previous.SetActive(false);
			}
		}
	}
	void ChangeSlideRight() //displays the next slide of the tutorial
	{
		if(slideIndex < slidesCount - 1)
		{
			slideIndex++;
			slide.GetComponent<Image>().sprite = slides[slideIndex];
			previous.SetActive(true);
			if(slideIndex == slidesCount - 1)
			{
				next.SetActive(false);
			}
		}
	}
	void BackToMenu()
	{
		Application.LoadLevel("Menu");
	}
}
