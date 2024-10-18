using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignalTowerScript : MonoBehaviour
{
	public Slider collectFillSlider;

	public GameObject collectBar;
	public GameObject dropObject;

	public float collectFill;
    public float collectFill_ { get; set; }

    public bool isInside = false;

	private void Start()
	{
		collectFill = 0f;
		collectBar.gameObject.SetActive(false);
	}

	private void Update()
	{
		collectFillSlider.value = collectFill;

		if(isInside && collectFill < 100)
		{
			collectFill += Time.deltaTime * 35f;
		}

		if (collectFill > 0 && isInside == false)
		{
			collectFill -= Time.deltaTime * 25f;
		}
		else if (collectFill <= 0 && isInside == false)
		{
			collectBar.gameObject.SetActive(false);
		}

		if(collectFill >= 100)
		{
			DropTheLoot();
		}
	}

	void DropTheLoot()
	{
		Instantiate(dropObject, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		isInside = true;
		collectBar.gameObject.SetActive(true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isInside = false;
	}
}
