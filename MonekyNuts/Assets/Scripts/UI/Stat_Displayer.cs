using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stat_Displayer : MonoBehaviour {

	public PlayerStats playerStats;

	void Start () 
	{
		playerStats = FindObjectOfType<PlayerStats>().GetComponent<PlayerStats>();
	}
	void Update()
	{
		GetComponent<Text>().text = "Attack: " + playerStats.activeDamage + "\nArmor: " + playerStats.armor;
	}
	

}
