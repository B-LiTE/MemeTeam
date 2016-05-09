using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Castle_Health_Display : MonoBehaviour {


	Castle castle;

    [SerializeField]
	public Sprite[] sprites = new Sprite[10];

    [SerializeField]
    bool useHealthText;
    Text healthText;

    [SerializeField]
	RectTransform healthBar;

	Sprite castleSprite;

	void Start()
	{
		castle = FindObjectOfType<Castle>().GetComponent<Castle>();
        if (useHealthText)
        {
            healthText = GetComponentInChildren<Text>();
            healthText.enabled = true;
        }
		castleSprite = GetComponent<Image> ().sprite;
	}
	void Update()
	{
		UpdateCastleHealth(castle.currHealth / castle.totHealth);
	}
	public void UpdateCastleHealth(float ratio)
	{
        if (useHealthText) healthText.text = (int)(ratio * 100) + "%";
		healthBar.localScale = new Vector2 (ratio, 1);
        castleSprite = sprites[10 - (int)(ratio * 10)];

		/*if(ratio < .9f)
		{
			if(ratio < .8f)
			{
				if(ratio < .7f)
				{
					if(ratio < .6f)
					{
						if(ratio < .5f)
						{
							if(ratio < .4f)
							{
								if(ratio < .3f)
								{
									if(ratio < .2f)
									{
										if(ratio < .1f)
										{
										    castleSprite = sprites[9];	
									    }else  castleSprite = sprites[8];
								    }else  castleSprite = sprites[7];
							    }else  castleSprite = sprites[6];
						    }else  castleSprite = sprites[5];
					    }else  castleSprite = sprites[4];
				    }else  castleSprite = sprites[3];
			    }else  castleSprite = sprites[2];
		    }else  castleSprite = sprites[1];
		}
		else castleSprite = sprites[0];*/

	}
}
