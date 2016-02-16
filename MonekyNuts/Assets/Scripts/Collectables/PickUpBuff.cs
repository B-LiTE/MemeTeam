using UnityEngine;
using System.Collections;

public class PickUpBuff : MonoBehaviour {

    public string buffType; //the type of thing it is that we are picking up
	public int buffAmount;
    private GameStats stats;

    void Awake()
    {
		stats = FindObjectOfType<GameStats>().GetComponent<GameStats>();
		AmountRandomizer();
    }
	void AmountRandomizer()
	{
		buffAmount = (int)Random.Range (5,15);
	}
    void OnTriggerEnter(Collider Player)
    {

        if (personCanPickUp(Player.gameObject))
        {
            
            if (buffType == "gold")
            {
                stats.ChangeGoldCount(buffAmount);
				Destroy(gameObject);
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
