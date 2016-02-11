using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class KillableInstance : MonoBehaviour{

    public float currHealth;
    public float totHealth;
    public float armor = 0;

    public float healthRegenRate; //per second

    public bool isAlive = true;
    private float damageMultiplier;

    public GameObject HealthBar;

    Coroutine regenerationCoroutine;

    public delegate void voidDelegate();
    public event voidDelegate alertOnDeath;






    protected void Awake()
    {
        Debug.Log(this.gameObject.name + " 100 + armor = " + (100 + armor));
        damageMultiplier = 100 / (float)(100 + armor);
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
        Debug.Log("after " + amount + " change in health, curHeal = " + currHealth);
        if (currHealth > totHealth) currHealth = totHealth;
        if (currHealth <= 0)
        {
            isAlive = false;
            if (alertOnDeath != null) alertOnDeath();
            Die();
        }
    }

    public virtual void Damage(float amount)
    {
        Debug.Log(this.gameObject.name + " took " + amount + " damage??");
        Debug.Log("damageMultiplier = " + damageMultiplier);
        Debug.Log("-Mathf.abs = " + (-Mathf.Abs(amount)));
        ChangeHealth(-Mathf.Abs(amount) * damageMultiplier);
    }






    public void UpdateHealthBar(float amount)
    {
        ChangeHealth(-amount * damageMultiplier);  
    }

	protected abstract void Die ();

	
    


}
