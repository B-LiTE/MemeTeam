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
		if (amount > 0) 
		{
			GameObject sound = (GameObject)Instantiate(Resources.Load ("OneTimeSoundEffect", typeof (GameObject)));
			sound.GetComponent<AudioSource>().clip = References.soundEffects.soundEffects[2];
		}
    }
    public void ChangeScore(int amount)
    {
        score += amount;
    }
}
