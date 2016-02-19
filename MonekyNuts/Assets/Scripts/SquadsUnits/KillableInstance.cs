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

    public GameObject FloatingHealthBar;

    Coroutine regenerationCoroutine;

    public delegate void voidDelegate();
    public event voidDelegate alertOnDeath;






    protected void Awake()
    {
		RefreshDamageMultiplier();
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
            if (alertOnDeath != null) alertOnDeath();
            Die();
        }
    }

    public virtual void Damage(float amount)
    {
		
		RefreshDamageMultiplier();
        ChangeHealth(-Mathf.Abs(amount) * damageMultiplier);
    }
	public void RefreshDamageMultiplier()
	{
		damageMultiplier = 100 / (float)(100 + armor);
	}






    public void UpdateHealthBar(float amount)
    {
        ChangeHealth(-amount * damageMultiplier);  
    }

	protected abstract void Die ();

	
    


}
