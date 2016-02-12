using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Castle_Health_Display : MonoBehaviour {


	public Castle castle;
	public Sprite[] sprites = new Sprite[10];

	void Start()
	{
		castle = FindObjectOfType<Castle>().GetComponent<Castle>();

	}
	void Update()
	{
		UpdateCastleHealth(castle.currHealth / castle.totHealth);
		Debug.Log (castle.currHealth / castle.totHealth);
	}
	public void UpdateCastleHealth(float ratio)
	{

			if(ratio < .9f)
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
												GetComponent<Image>().sprite = sprites[9];	
											}else  GetComponent<Image>().sprite = sprites[8];
										}else  GetComponent<Image>().sprite = sprites[7];
									}else  GetComponent<Image>().sprite = sprites[6];
								}else  GetComponent<Image>().sprite = sprites[5];
							}else  GetComponent<Image>().sprite = sprites[4];
						}else  GetComponent<Image>().sprite = sprites[3];
					}else  GetComponent<Image>().sprite = sprites[2];
				}else  GetComponent<Image>().sprite = sprites[1];
			}
			else  GetComponent<Image>().sprite = sprites[0];

	}
}
