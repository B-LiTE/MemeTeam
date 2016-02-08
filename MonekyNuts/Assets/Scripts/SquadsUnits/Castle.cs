using UnityEngine;
using System.Collections;

public class Castle : KillableInstance {

    public RectTransform tempHealthBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K)) Damage(-10);
        
	}
    public override void Damage(float amount)
    {
        base.Damage(amount);
        UpdateHealthBar();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
    public void UpdateHealthBar()
    {
        if (currHealth / totHealth > 0)
        {
            tempHealthBar.localScale = new Vector3(currHealth / totHealth,
                                            tempHealthBar.localScale.y,
                                            tempHealthBar.localScale.z);
        }
        else
        {
            tempHealthBar.localScale = new Vector3(0,
                                                tempHealthBar.localScale.y,
                                                tempHealthBar.localScale.z);
        }
    }
}
