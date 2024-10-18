using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeTemplate", menuName = "Scriptable Objects/New Upgrade Item", order = 1)]
public class UpgradeItemSO : ScriptableObject
{
	public Sprite upgradePictureSO;

	public string upgradeTitleSO;
	public string upgradeDescriptionSO;
	public string upgradeTagSO = "None";

	public int upgradeVersionSO;
	public int upgradeValueSO = 10;

	public int upgradeIndexSO = 0;
}
