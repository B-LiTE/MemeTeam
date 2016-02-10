using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Market : MonoBehaviour {

	public MarketArrays marketArrays;
	public int[] activeArray;

	public int[] buyableItems = new int[3];

	public GameObject[] purchaseButton = new GameObject[3];
	public GameObject[] purchaseDisplay = new GameObject[3];

	void Start () 
	{
		marketArrays = GetComponent<MarketArrays>();
		ChangeMarket ();
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.R))
			RandomizeMarket ();
	}
	public void ChangeMarket() //need to call this when moving to next level
	{
		//if Level == 1
		activeArray = marketArrays.testItems;

	}
	public void RandomizeMarket()
	{
		buyableItems = new int[3];
		List<int> alreadyRandomized = new List<int> ();
		int number = 0;
		bool added = false;
		for (int i = 0; i < activeArray.Length; i++) 
		{
			while(!added)
			{
				number = (int)Random.Range(0,activeArray.Length - 1);
				added = true;
				for(int j = 0;j < alreadyRandomized.Count;j++)
				{
					if(number == alreadyRandomized[j]) added = false;
				}

			}
			buyableItems[i] = activeArray[number];

		}
	}

}
