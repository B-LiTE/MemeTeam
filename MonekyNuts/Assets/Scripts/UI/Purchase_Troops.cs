using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Purchase_Troops : MonoBehaviour {

	int troopCount;
	int troopCost;
	public GameObject troop;

	[SerializeField]
	AudioClip clip;

	void Start () 
	{
		RefreshCost ();
	}
	public void RefreshCost()
	{
		troopCost = 10 + (5 * GameObject.FindGameObjectsWithTag ("Troop").Length/2);
		GetComponentInChildren<Text> ().text = "Buy Troop: \n<color=#9C0101>Buy Price: " + troopCost + "</color>";
	}
	void PurchaseTroop()
	{
		if (References.gameStats.goldCount >= troopCost) 
		{
			References.gameStats.ChangeGoldCount(-troopCost);
			GameObject troopObject = Instantiate(troop);
			troopObject.transform.position = new Vector3((int)Random.Range (-100,100),(int)Random.Range (-100,100),100);
			RefreshCost();
			GameObject sound = (GameObject)Instantiate(Resources.Load ("OneTimeSoundEffect", typeof (GameObject)));
			sound.GetComponent<AudioSource>().clip = References.soundEffects.soundEffects[0];
		}
	}

}
