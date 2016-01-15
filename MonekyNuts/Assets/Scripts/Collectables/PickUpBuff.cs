using UnityEngine;
using System.Collections;

public class PickUpBuff : MonoBehaviour {

    public string buffType; //the type of thing it is that we are picking up
    private GameStats thing;

    void Awake()
    {

    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.CompareTag("Player"))
        {
            
            if (buffType == "gold")
            {
                FindObjectOfType<GameStats>().GetComponent<GameStats>().ChangeGoldCount(1);
            }
            
        }
    }
}
