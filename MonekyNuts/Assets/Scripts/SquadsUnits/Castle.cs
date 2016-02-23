using UnityEngine;
using System.Collections;

public class Castle : KillableInstance {

    public RectTransform tempHealthBar;
	
	// Update is called once per frame
    // DEBUG
	void Update () {

        if (Input.GetKeyDown(KeyCode.K)) Damage(-10, this.gameObject);
        
	}
    public override void Damage(float amount, GameObject attacker)
    {
        base.Damage(amount, attacker);
        UpdateHealthBar();
    }

    protected override void Die()
    {
        References.stateManager.loadLoseLevel();
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
