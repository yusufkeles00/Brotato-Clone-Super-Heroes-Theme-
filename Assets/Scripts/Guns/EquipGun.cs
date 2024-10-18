using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGun : MonoBehaviour
{
	GunSlots slots;
	public GameObject item;
	

	private void Start()
	{
		slots = GameObject.FindGameObjectWithTag("GunSlots").GetComponent<GunSlots>();
	}

	public void AddGun()
	{
		for (int i = 0; i < slots.gunSlots.Length; i++)
		{
			if (slots.isFullSlots[i] == false)
			{
				//Add Gun to Slots
				GameObject itemObj = Instantiate(item, slots.gunSlots[i].transform.position, Quaternion.identity);
				itemObj.transform.SetParent(slots.gunSlots[i].transform,true);

				slots.isFullSlots[i] = true;
				break;
			}
		}
	}
}
