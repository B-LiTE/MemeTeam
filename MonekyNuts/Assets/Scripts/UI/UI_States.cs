using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_States : MonoBehaviour {

	public GameObject inventoryStorage;
	public GameObject inventoryWheelStorage;
	public GameObject market;
	public GameObject showInventoryButton;
	public GameObject showMarketButton;

	public GameObject[] strategyUI;

	void Start()
	{
		References.stateManager.changeState += onChangeState;
		//strategyUI = GameObject.Findg("StrategyUI");
	}

	void onChangeState()
	{
		if (References.stateManager.CurrentState == StateManager.states.realtime)
			hide ();
		else
			show ();
	}

	void show()
	{
		showInventoryButton.SetActive (true);
		showInventoryButton.GetComponentInChildren<Text>().text = "Show Inventory";
		showMarketButton.SetActive (true);
		showMarketButton.GetComponentInChildren<Text>().text = "Show Market";
	}

	void hide()
	{
		/*
		for (int i = 0; i < strategyUI.Length; i++) 
		{
			strategyUI[i].SetActive(false);
		}*/
		inventoryStorage.SetActive (false);
		inventoryWheelStorage.SetActive (false);
		//market.SetActive(false);
		showInventoryButton.SetActive(false);
		showMarketButton.SetActive (false);
	}
}
