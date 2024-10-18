using System.Collections;	
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarSc : MonoBehaviour
{
	public TMP_Text healthText;
	public Slider healthSlider;

	public int healthPoint;

	GameObject playerObj;

	private void Start()
	{
		playerObj = GameObject.FindGameObjectWithTag("Player");
		healthSlider.maxValue = playerObj.GetComponent<CharacterMovementScript>().characterMaxHealth;
	}

	private void Update()
	{
		healthSlider.maxValue = playerObj.GetComponent<CharacterMovementScript>().characterMaxHealth;

		healthText.SetText(healthPoint.ToString());
		healthPoint = playerObj.GetComponent<CharacterMovementScript>().characterCurrentHealth;
		healthSlider.value = healthPoint;
	}
}
