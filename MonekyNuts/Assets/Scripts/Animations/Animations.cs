using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    private Animator anim;
    PlayerStats playerStats;
    NavMeshAgent playerNavigation;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = References.player.GetComponent<PlayerStats>();
        playerNavigation = References.player.GetComponent<NavMeshAgent>();
    }

	
	// Update is called once per frame
	void Update () {

        if (playerNavigation.velocity.magnitude > 0)
        {
            if (!anim.GetBool("Run")) anim.SetBool("Run", true);
        }
        else if (!anim.GetBool("Punch") && !anim.GetBool("Arrow"))
        {
            if (anim.GetBool("Run")) anim.SetBool("Run", false);
        }
	
	}

    public void attackAnimation(bool isAttacking)
    {
        /*if (playerStats.attackRange >= 12) anim.SetBool("Arrow", isAttacking);
        else */anim.SetBool("Punch", isAttacking);
    }
}
