using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Market : MonoBehaviour {

	public MarketArrays marketArrays;
	public Item_Database itemDatabase;
	public Show_Market showMarket;

	public int[] activeArray;

	public int[] buyableItems = new int[3];

	public GameObject[] purchaseButtons = new GameObject[3];
	public GameObject[] purchaseDisplays = new GameObject[3];

	void Start () 
	{
		References.stateManager.changeState += RandomizeMarket;

		marketArrays = GetComponent<MarketArrays>();
		itemDatabase = GetComponent<Item_Database>();
		showMarket = FindObjectOfType<Show_Market> ().GetComponent<Show_Market> ();
		ChangeMarket ();
		RandomizeMarket();
	}
	void Update()
	{
	}
	public void ChangeMarket() //need to call this when moving to next level
	{
        activeArray = marketArrays.levelOneItems;
		/*if (Application.loadedLevelName.Contains("Kenny")) 
		{
			activeArray = marketArrays.levelOneItems;
		} 
		else if (Application.loadedLevelName.Contains("Paul"))
		{ 
			activeArray = marketArrays.levelTwoItems;
		}
		else if (Application.loadedLevelName.Contains("Alex")) 
		{ 
			activeArray = marketArrays.levelThreeItems;
		}*/


	}
	public void UpdateMarketDisplay()
	{
		for(int i = 0;i < buyableItems.Length;i++)
		{
			purchaseButtons[i].GetComponentInChildren<Text>().text = itemDatabase.allItems[buyableItems[i]].ToString() + "<color=#9C0101>\nBuy Price: " + itemDatabase.allItems[buyableItems[i]].goldPrice + "</color>";
			purchaseButtons[i].GetComponent<Purchase_Item>().itemID = buyableItems[i];
			purchaseDisplays[i].GetComponent<Image>().sprite = itemDatabase.inventorySprites[buyableItems[i]];
		}
	}
	public void RandomizeMarket()
	{
		buyableItems = new int[3];
		List<int> alreadyRandomized = new List<int> ();
		int number = 0;
		bool addable;
		for (int i = 0; i < buyableItems.Length; i++) 
		{
				addable = true;

				number = (int)Random.Range(0,activeArray.Length);
				for(int j = 0;j < alreadyRandomized.Count && addable;j++)
				{
					if(number == alreadyRandomized[j])
					{
						addable = false;
					}
				}
				if(addable)
				{
					alreadyRandomized.Add(number);
				}
				else if (!addable)
				{
					bool isGood = false;
					for(int k = 0;k < activeArray.Length && !isGood;k++)
					{
						isGood = true;
						for(int l = 0;l < alreadyRandomized.Count && isGood;l++)
						{
							if(activeArray[k] == alreadyRandomized[l])
							{
								isGood = false;
							}
						}
						if(isGood)
						{
							alreadyRandomized.Add(activeArray[k]);
							number = activeArray[k];
						}
					}
				}
			buyableItems[i] = activeArray[number];
		}

	}

}
