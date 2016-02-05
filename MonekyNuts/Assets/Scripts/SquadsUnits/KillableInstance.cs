using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class KillableInstance : MonoBehaviour{

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
    public virtual void ChangeHealth(float amount)
    {
        
        currHealth += amount;
        
        if (currHealth > totHealth) currHealth = totHealth;
        if (currHealth < 0)
        {
            isAlive = false;
            Die();
        }
    }
    public virtual void Damage(float amount) //need to pass in negativer number for this to work
    {
        ChangeHealth(-amount * damageMultiplier);  
    }
	public abstract void Die ();

	
    


}
