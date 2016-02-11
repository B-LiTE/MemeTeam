using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Show_Market : MonoBehaviour {

	public bool isMarketVisible = false;
	public GameObject market;
	public GameObject showMarketButton;
	public Market marketData;

	void Start()
	{
		//References.stateManager.changeState += onChangeState;
		marketData = FindObjectOfType<Market> ().GetComponent<Market> ();
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
			marketData.UpdateMarketDisplay ();
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
