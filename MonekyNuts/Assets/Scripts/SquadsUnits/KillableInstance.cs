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

    public GameObject HealthBar;






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
        if (currHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    public virtual void Damage(float amount) //need to pass in negative number for this to work
    {
        ChangeHealth(amount * damageMultiplier);  
    }






    public void UpdateHealthBar(float amount)
    {
        ChangeHealth(-amount * damageMultiplier);  
    }

	protected abstract void Die ();

	
    


}
