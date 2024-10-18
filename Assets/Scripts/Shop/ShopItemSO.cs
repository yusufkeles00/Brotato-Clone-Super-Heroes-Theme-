using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "Scriptable Objects/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{
	public Sprite itemPicture;

	public string itemTitle;
	public string itemDescription;
	public int itemCost;
}
