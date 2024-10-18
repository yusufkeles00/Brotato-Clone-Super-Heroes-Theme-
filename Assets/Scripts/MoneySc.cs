using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySc : MonoBehaviour
{
	public int moneyCost;

	public Color moneyTextColor;

	public GameObject MoneyDestroyParticle;
	private GameObject moneyManagerObj;
	private GameObject popUpTextManagerObj;
	private GameObject characterObj;

	private void Start()
	{
		moneyManagerObj = GameObject.FindGameObjectWithTag("MoneyManager");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");
		characterObj = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (Vector2.Distance(transform.position,characterObj.transform.position) < 1.5)
		{
			transform.position = Vector2.Lerp(transform.position, characterObj.transform.position, 0.1f);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			moneyManagerObj.GetComponent<MoneyManager>().money += moneyCost;
			collision.GetComponent<CharacterMovementScript>().currentXp++;
			Instantiate(MoneyDestroyParticle, transform.position, Quaternion.identity);

			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("+", moneyCost, gameObject.transform, new Vector3(1.3f, 0.8f, 0f), moneyTextColor);

			Destroy(gameObject);
		}
	}
}
