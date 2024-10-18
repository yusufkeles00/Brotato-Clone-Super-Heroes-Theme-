using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public ShopItemSO[] shopItemSO;

	public ShopTemplate[] shopTemplates;

	private void Start()
	{
		LoadItemPanels();
	}

	public void LoadItemPanels()
	{
		for (int i = 0; i < 4; i++)
		{
			shopTemplates[i].itemTitleTxt.text = shopItemSO[i].itemTitle;
			shopTemplates[i].itemDescriptionTxt.text = shopItemSO[i].itemDescription;
			shopTemplates[i].itemCostTxt.text = shopItemSO[i].itemCost.ToString();

			shopTemplates[i].itemPictureImage.sprite = shopItemSO[i].itemPicture;
		}
	}
}
