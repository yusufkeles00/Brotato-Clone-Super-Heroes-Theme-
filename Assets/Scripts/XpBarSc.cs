using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
	private Slider xpBarSlider;

	private GameObject characterObject;

	public int xpValueCurrent;
	public int xpValueMax;

	private void Start()
	{
		xpBarSlider = GetComponent<Slider>();
		characterObject = GameObject.FindGameObjectWithTag("Player");
		xpBarSlider.maxValue = 100;
	}

	private void Update()
	{
		xpValueCurrent = characterObject.GetComponent<CharacterMovementScript>().currentXp;
		xpBarSlider.value = xpValueCurrent;
		xpBarSlider.maxValue = characterObject.GetComponent<CharacterMovementScript>().xpRequired;
	}
}
