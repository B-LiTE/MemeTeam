using UnityEngine;
using System.Collections;

public class MarketArrays : MonoBehaviour {

	public int[] levelOneItems; //array of item ids
	public int[] levelTwoItems;
	public int[] levelThreeItems;

	void Awake()
	{
		levelOneItems = new int[] {1, 2, 3, 4, 7, 8 , 13, 15, 17};
		levelTwoItems = new int[] {6,7,8,9,10,12,13,14,16,18,20,26,27};
		levelThreeItems = new int[]{5,12,13,14,16,19,20,21,22,23,24,25,28,29};
	}
}
