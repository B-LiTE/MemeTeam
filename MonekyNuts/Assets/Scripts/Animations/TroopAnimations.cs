using UnityEngine;
using System.Collections;

public class TroopAnimations : MonoBehaviour {
    
    TroopBehavior troopBehavior;
    private Animator anim;

    void Start()
    {
        troopBehavior = GetComponentInParent<TroopBehavior>();
        troopBehavior.changeOfActions += onActionChange;
        anim = GetComponent<Animator>();
    }

    void onActionChange()
    {
        if (troopBehavior.getAction() == TroopBehavior.actions.move) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);

        if (troopBehavior.getAction() == TroopBehavior.actions.attack) StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        anim.SetBool("Punch", true);
        yield return new WaitForSeconds(0.75f);
        anim.SetBool("Punch", false);
    }
}
