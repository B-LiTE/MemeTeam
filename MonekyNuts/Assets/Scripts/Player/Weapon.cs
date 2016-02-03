using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	//NEVER MIND THIS IS THE WRONG METHOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOD

	//seperate management script will put values into this script. 
    //This just holds the data for the weapon that is currently equipped to the player/enemy this is attatched to

    public string weaponName;
    public float damage; //this value will be added to unit's base attack
    public float attackSpeed; //this value will represent the cooldown time(in seconds) between each attack(unless better idea of handling it)
    public bool isMelee; //I assume we only have melee or ranged, if we have more this weapon type can be a string
    public float range; //how close the user has to be to hit its target
    public float criticalHitChance; //value 0 - 99%, I have a way of calculating if its a critical hit or not
    public float criticalDamage;

    //possible extras// not really needed

    public float lifeSteal;
    public float recoilDamage;
    public string statusEffect;
}
