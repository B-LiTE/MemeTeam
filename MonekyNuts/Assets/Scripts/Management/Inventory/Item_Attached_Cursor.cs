using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Attached_Cursor : MonoBehaviour {

	public int itemId;
	public int itemStackCount;
	
	void Update()
	{
		transform.position =  Input.mousePosition;
	}
	public void ChangeSprite(int spriteID)
	{
		if(spriteID == -1 || spriteID == 0) GetComponent<Image>().sprite = null;
		else GetComponent<Image>().sprite = FindObjectOfType<Item_Database>().inventorySprites[spriteID];
	}
}
