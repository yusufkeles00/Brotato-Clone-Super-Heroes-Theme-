using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
	public TMP_Text moneyText;

	public int money;

	private void Update()
	{
		moneyText.SetText(money.ToString());
	}

}
