using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
	public UpgradeItemSO[] upgradeItemSO;

	public UpgradeTemplate[] upgradeTemplate;

	int a;
	int[] nums = {-1, -1, -1, -1};

	private void Awake()
	{
		LoadUpgradePanels();
	}

	public void LoadUpgradePanels()//firstly this method going to start at the beginning
	{
		nums[0] = -1;
		nums[1] = -1;
		nums[2] = -1;
		nums[3] = -1;

		for (int i = 0; i < 4; i++)
		{
			a = Random.Range(0, upgradeItemSO.Length - 1);

			for (int j = 0; j < nums.Length; j++)
			{
				if(a == nums[j])
				{
					a = Random.Range(0, upgradeItemSO.Length);
				}
			}

			nums[i] = a;

			//Assigning values ​​in Scriptable objects to values ​​of real objects
			upgradeTemplate[i].upgradePictureImage.sprite = upgradeItemSO[a].upgradePictureSO;

			upgradeTemplate[i].upgradeTitleTxt.text = upgradeItemSO[a].upgradeTitleSO + " " + string.Concat(System.Linq.Enumerable.Repeat("I", upgradeItemSO[a].upgradeVersionSO));
			upgradeTemplate[i].upgradeDescriptionTxt.text = "+" + upgradeItemSO[a].upgradeValueSO.ToString() + " " + upgradeItemSO[a].upgradeDescriptionSO;

			upgradeTemplate[i].upgradeValue = upgradeItemSO[a].upgradeValueSO;

			upgradeTemplate[i].upgradeIndex = upgradeItemSO[a].upgradeIndexSO;

			upgradeTemplate[i].upgradeTag = upgradeItemSO[a].upgradeTagSO;
		}
	}

	public void RerollUpgradePanels()//then this method going to start when player click the "reroll" button to
	{
		LoadUpgradePanels();
	}

	public void CloseUpgradePanel()
	{
		Time.timeScale = 1;
		gameObject.SetActive(false);
	}
}
