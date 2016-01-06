using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

    
    public int score;
    public int goldCount;

    public Text goldCountText;
    
    public void ChangeGoldCount(int amount)
    {
        goldCount += amount;
        goldCountText.text = goldCount.ToString();
    }
    public void ChangeScore(int amount)
    {
        score += amount;
    }
}
