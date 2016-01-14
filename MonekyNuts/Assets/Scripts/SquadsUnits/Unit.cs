using UnityEngine;
using System.Collections;

public class Unit : KillableInstance  
{

    public float attack;
    public float movementSpeed;
    public float attackSpeed;

    public override void Die()
    {
        Debug.Log("DEAD");
    }

}
