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

    Coroutine regenerationCoroutine;






    void Awake()
    {
        damageMultiplier = 100 / (100 + armor);
    }

    void Start()
    {
        if (healthRegenRate != 0) regenerationCoroutine = StartCoroutine(regeneration());
    }

    IEnumerator regeneration()
    {
        while (true)
        {
            ChangeHealth(healthRegenRate * Time.deltaTime);
            yield return null;
        }
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
