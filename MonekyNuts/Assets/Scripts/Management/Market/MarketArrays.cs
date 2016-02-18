using UnityEngine;
using System.Collections;

public class MarketArrays : MonoBehaviour {

	public int[] testItems = new int[] {1,2,3,4,7,8,13,15,17}; //array of item ids

	void Awake()
	{
		testItems = new int[] {1, 2, 3, 4, 7, 8 , 13, 15, 17};
	}
}
