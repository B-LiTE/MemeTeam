using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Purchase_Castle_Repair : MonoBehaviour {

	public int repairPrice;

	void Start()
	{
		repairPrice = 10;
		GetComponentInChildren<Text>().text = "Repair Castle 5% \n<color=#9C0101>Buy Price: " + repairPrice + "</color>";
	}
	void PurchaseRepair()
	{
		if(References.gameStats.goldCount >= repairPrice && References.castle.GetComponent<Castle>().currHealth < 
		   													References.castle.GetComponent<Castle>().totHealth)
		{
			References.gameStats.ChangeGoldCount(-repairPrice);
			References.castle.GetComponent<Castle>().ChangeHealth(.05f * References.castle.GetComponent<Castle>().totHealth);
		}
	}
}
