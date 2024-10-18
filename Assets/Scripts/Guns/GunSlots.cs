using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSlots : MonoBehaviour
{
	public bool[] isFullSlots;

	public GameObject[] gunSlots;
	[SerializeField] GameObject characterObj;

	public Vector3 slotOffset;

	private void FixedUpdate()
	{
		transform.position = characterObj.transform.position + slotOffset; 
	}
}
