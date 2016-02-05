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

<<<<<<< HEAD
=======
    public GameObject HealthBar;






>>>>>>> origin/master
    void Awake()
    {
        damageMultiplier = 100 / (100 + armor);
    }

    void Update()
    {
        ChangeHealth(healthRegenRate * Time.deltaTime);
    }
<<<<<<< HEAD
    public virtual void ChangeHealth(float amount)
=======


    


    public void ChangeHealth(float amount)
>>>>>>> origin/master
    {
        
        currHealth += amount;
        
        if (currHealth > totHealth) currHealth = totHealth;
        if (currHealth < 0)
        {
            isAlive = false;
            Die();
        }
    }
<<<<<<< HEAD
    public virtual void Damage(float amount) //need to pass in negativer number for this to work
=======


    public void Damage(float amount) //need to pass in negative number for this to work
    {
        ChangeHealth(amount * damageMultiplier);  
    }







    protected abstract void Die();






    public void UpdateHealthBar()
>>>>>>> origin/master
    {
        ChangeHealth(-amount * damageMultiplier);  
    }
	public abstract void Die ();

	
    


}
