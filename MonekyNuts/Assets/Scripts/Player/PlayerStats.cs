﻿using UnityEngine;
using System.Collections;

public class PlayerStats : KillableInstance {
    
    protected override void Die()
    {
        Debug.LogError("he dead");
    }

}