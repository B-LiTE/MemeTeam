using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_States : MonoBehaviour {

	public GameObject showInventoryButton;
	public GameObject inventoryStorage;
	public GameObject inventoryWheelStorage;

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

	void show()
	{
		showInventoryButton.SetActive (true);
		showInventoryButton.GetComponentInChildren<Text>().text = "Show Inventory";
	}

	void hide()
	{
		showInventoryButton.SetActive (false);
		inventoryStorage.SetActive (false);
		inventoryWheelStorage.SetActive (false);
	}
}
