using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {

    [HideInInspector]
    public Animator anim;
    PlayerStats playerStats;
    NavMeshAgent playerNavigation;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerStats = References.player.GetComponent<PlayerStats>();
        playerNavigation = References.player.GetComponent<NavMeshAgent>();
    }

	
	// Update is called once per frame
	/*void Update () {

        if (playerNavigation.velocity.magnitude > 0)
        {
            if (!anim.GetBool("Run")) anim.SetBool("Run", true);
        }
        else if (!anim.GetBool("Punch") && !anim.GetBool("Arrow"))
        {
            if (anim.GetBool("Run")) anim.SetBool("Run", false);
        }
	
	}*/

    public void turnOffAll()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Punch", false);
        anim.SetBool("Arrows", false);
    }

    public void playRunningAnimation()
    {
        turnOffAll();
        anim.SetBool("Run", true);
    }

    public void playPunchingAnimation()
    {
        turnOffAll();
        anim.SetBool("Punch", true);
    }

    public void playArrowAnimation()
    {
        turnOffAll();
        anim.SetBool("Arrows", true);
    }

    public void attackAnimation(bool isAttacking)
    {
        /*if (playerStats.attackRange >= 12) anim.SetBool("Arrow", isAttacking);
        else */anim.SetBool("Punch", isAttacking);
    }
}
