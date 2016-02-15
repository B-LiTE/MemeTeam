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
        if (personCanPickUp(Player.gameObject))
        {
            
            if (buffType == "gold")
            {
                FindObjectOfType<GameStats>().GetComponent<GameStats>().ChangeGoldCount(1);
            }
            
        }
    }

    bool personCanPickUp(GameObject person)
    {
        switch (person.tag)
        {
            case "Player":
            case "Troop":
                return true;

            default:
                return false;
        }
    }
}
