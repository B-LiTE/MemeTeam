using UnityEngine;
using System.Collections;

public class SquadVariables : MonoBehaviour {

	public int unitCount; // how many unites in a squad;
	public float unitHealth; // how much health each individual soldier has;
	private float baseHealth; //base health - if it's only Dr. Meme left he's stronger than usual.
	public float TotHealth; //health of the squad - base health added to all of the unites;
	public float armor;
	public float attackPower;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
