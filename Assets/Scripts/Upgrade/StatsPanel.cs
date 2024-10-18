using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPanel : MonoBehaviour
{
	private GameObject characterStats;

	public TMP_Text maxHpTxt;
	public TMP_Text hpRegenerationTxt;
	public TMP_Text lifeStealTxt;

	public TMP_Text damageTxt;
	public TMP_Text criticalDamageTxt;

	public TMP_Text rangeTxt;

	public TMP_Text moveSpeedTxt;
	public TMP_Text attackSpeedTxt;

	public TMP_Text armorTxt;
	public TMP_Text dodgeTxt;
	public TMP_Text harvestingTxt;

	private void Start()
	{
		characterStats = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		maxHpTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._maxHP.ToString());
		hpRegenerationTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._hpRegeneration.ToString());
		lifeStealTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._lifeSteal.ToString());

		damageTxt.SetText(characterStats.GetComponent <CharacterStatsSc>()._damage.ToString());
		criticalDamageTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._criticalDamage.ToString());

		rangeTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._range.ToString());

		moveSpeedTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._moveSpeed.ToString());
		attackSpeedTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._attackSpeed.ToString());

		armorTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._armor.ToString());
		dodgeTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._dodge.ToString());
		harvestingTxt.SetText(characterStats.GetComponent<CharacterStatsSc>()._harvesting.ToString());

	}
}
