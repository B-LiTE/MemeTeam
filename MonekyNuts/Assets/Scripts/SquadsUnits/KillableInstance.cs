using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class KillableInstance : MonoBehaviour{

    public float currHealth;
    public float totHealth;
    float armor = 0;
    public float Armor
    {
        get { return armor; }
        set
        {
            armor = value;
            RefreshDamageMultiplier();
        }
    }

    public float healthRegenRate; //per second

    public bool isAlive = true;
    private float damageMultiplier;

    public RectTransform FloatingHealthBar;

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
        // DEBUG
        Debug.LogWarning(gameObject.name + ": " + currHealth + " + " + amount + " = " + (currHealth + amount));
        currHealth += amount;
        if (currHealth > totHealth) currHealth = totHealth;
        if (FloatingHealthBar != null) UpdateHealthBar();
        if (currHealth <= 0)
        {
            isAlive = false;
            if (alertOnDeath != null) alertOnDeath();
            Die();
        }
        // DEBUG
        //if (currHealth <= 0) Debug.LogError("ENEMY " + gameObject.name + " IS BELOW 0 HEALTH");
    }

    public virtual void Damage(float amount, GameObject attacker)
    {
        ChangeHealth(-Mathf.Abs(amount) * damageMultiplier);
    }
	public void RefreshDamageMultiplier()
	{
		damageMultiplier = 100 / (float)(100 + Armor);
	}






    public void UpdateHealthBar()
    {
        // If our health is greater than zero, scale the health bar to represent our health
        if (currHealth / totHealth > 0)
        {
            FloatingHealthBar.localScale = new Vector3(currHealth / totHealth, FloatingHealthBar.localScale.y, FloatingHealthBar.localScale.z);
        }
        else
        {
            // Otherwise, show none of the health bar
            FloatingHealthBar.localScale = new Vector3(0, FloatingHealthBar.localScale.y, FloatingHealthBar.localScale.z);
        }
    }

	protected abstract void Die ();

	
    


}
