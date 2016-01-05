using UnityEngine;
using System.Collections;

public class KillableInstance : MonoBehaviour {

    public float currHealth;
    public float totHealth;
    public float armor;

    public float healthRegenRate; //per second

    public bool isAlive = true;
    private float damageMultiplier;

    void Awake()
    {
        damageMultiplier = 100 / (100 + armor);
    }
    void Update()
    {
        ChangeHealth(healthRegenRate * Time.deltaTime);
    }
    public void ChangeHealth(float amount)
    {
        
        currHealth += amount;
        
        if (currHealth > totHealth) currHealth = totHealth;
        if (currHealth < totHealth) isAlive = false;
    }
    public void Damage(float amount) //need to pass in negativer number for this to work
    {
        ChangeHealth(amount * damageMultiplier);  
    }
}
