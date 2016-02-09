using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Show_Market : MonoBehaviour {

	bool isMarketVisible = false;
	public GameObject market;
	public GameObject showMarketButton;

	void Start()
	{
		References.stateManager.changeState += onChangeState;
	}
	
	void onChangeState()
	{
		if (References.stateManager.CurrentState == StateManager.states.realtime)
			hide ();
		else
			show ();
	}
	public void ChangeDisplay()
	{
		if (!isMarketVisible) 
		{
			show ();
		} 
		else 
		{
			hide ();
		}

	}
	void hide()
	{
		market.SetActive (false);
		showMarketButton.GetComponentInChildren<Text>().text = "Show Market";
		isMarketVisible = false;
	}
	void show()
	{
		market.SetActive(true);
		showMarketButton.GetComponentInChildren<Text> ().text = "Hide Market";
		isMarketVisible = true;
	}
}
