using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton0 : MonoBehaviour
{
	CharacterStatsSc characterStatsSc;

	private UpgradeTemplate upgradeTemplate;

	private GameObject LevelUpPanelObj;

	private void Start()
	{
		characterStatsSc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsSc>();

		upgradeTemplate = GetComponent<UpgradeTemplate>();
		LevelUpPanelObj = GameObject.FindGameObjectWithTag("LevelUpPanel");
	}

	public void ClickButton0()
	{
		Debug.Log(gameObject.tag);
		Debug.Log(upgradeTemplate.upgradeTag);

		//Finding upgrade type with tags
		if (gameObject.tag == "MaxHPTag")
			characterStatsSc._maxHP += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "HPRegenerationTag")
			characterStatsSc._hpRegeneration += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "LifeStealTag")
			characterStatsSc._lifeSteal += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "DamageTag")
			characterStatsSc._damage += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "CriticalDamageTag")
			characterStatsSc._criticalDamage += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "RangeTag")
			characterStatsSc._range += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "MoveSpeedTag")
			characterStatsSc._moveSpeed += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "AttackSpeedTag")
			characterStatsSc._attackSpeed += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "ArmorTag")
			characterStatsSc._armor += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "DodgeTag")
			characterStatsSc._dodge += upgradeTemplate.upgradeValue;
		else if (gameObject.tag == "HarvestingTag")
			characterStatsSc._harvesting += upgradeTemplate.upgradeValue;

		LevelUpPanelObj.gameObject.SetActive(false);
		Time.timeScale = 1;
	}
}
