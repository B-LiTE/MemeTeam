using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    private bool isAttacking;
    private Weapon currentWeapon; 

    void UpdateWeapon() //called when weapons are toggled
    {
        currentWeapon = GetComponent<Weapon>();
    }
	// this class is attached to the player, will active if the player has clicked on an enemy and is in range
	void Update () 
    {
        if (isAttacking)
        {
            //if(Vector3.Distance(transform.position, ))
            
                if (currentWeapon.isMelee)
                {

                }
                else if (!currentWeapon.isMelee)
                {
                    
                }
            
        }
	}
    void StartAttacking() //player movement will use Attack.SendMessage("StartAttacking") when clicked on an enemy
    {
        isAttacking = true;
    }
    void StopAttacking() //player movement will use Attack.SendMessage("StopAttacking") when clicked on something other than an enemy
    {
        isAttacking = false;
    }
}
