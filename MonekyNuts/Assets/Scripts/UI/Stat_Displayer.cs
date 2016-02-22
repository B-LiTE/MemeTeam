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
		//GetComponent<Text>().text = "Attack: " + playerStats.activeDamage + "\nArmor: " + playerStats.armor;
		GetComponent<Text>().text = string.Format("Attack: {0,-6} Range: {1,-6}\nArmor: {2,-6}  Attack Speed: {3,-6}",
		                                          playerStats.activeDamage,playerStats.attackRange,playerStats.Armor,playerStats.attackSpeed);
	}
	

}
