using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeTemplate : MonoBehaviour
{
	private CharacterStatsSc characterStatsSc;

	public GameObject UpgradePanelObj;

	public Image upgradePictureImage;

	public TMP_Text upgradeTitleTxt;
	public TMP_Text upgradeDescriptionTxt;

	public string upgradeTag = null;

	public int upgradeValue;

	public int upgradeIndex;

	private void Start()
	{
		characterStatsSc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsSc>();
	}

	private void Update()
	{
		gameObject.tag = upgradeTag;
	}

	public void Deneme()
	{
		Debug.Log(gameObject.name);
	}

	public void UpgradeStuff()
	{
		gameObject.SetActive(false);
	}

	private void DecideUpgrade()
	{
		//Finding upgrade type with tags
		if(gameObject.tag == "MaxHPTag")
			characterStatsSc._maxHP += upgradeValue;
		else if(gameObject.tag == "HPRegenerationTag")
			characterStatsSc._hpRegeneration += upgradeValue;
		else if(gameObject.tag == "LifeStealTag")
			characterStatsSc._lifeSteal += upgradeValue;
		else if(gameObject.tag == "DamageTag")
			characterStatsSc._damage += upgradeValue;
		else if(gameObject.tag == "CriticalDamageTag")
			characterStatsSc._criticalDamage += upgradeValue;
		else if(gameObject.tag == "RangeTag")
			characterStatsSc._range += upgradeValue;
		else if(gameObject.tag == "MoveSpeedTag")
			characterStatsSc._moveSpeed += upgradeValue;
		else if(gameObject.tag == "AttackSpeedTag")
			characterStatsSc._attackSpeed += upgradeValue;
		else if(gameObject.tag == "ArmorTag")
			characterStatsSc._armor += upgradeValue;
		else if(gameObject.tag == "DodgeTag")
			characterStatsSc._dodge += upgradeValue;
		else if(gameObject.tag == "HarvestingTag")
			characterStatsSc._harvesting += upgradeValue;
	}

}
