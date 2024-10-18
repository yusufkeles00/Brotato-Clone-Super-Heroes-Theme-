using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitSc : MonoBehaviour
{
	public GameObject healthKitDestroyParticle;

	public int healthPower;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<CharacterMovementScript>().characterCurrentHealth += healthPower;

			Instantiate(healthKitDestroyParticle, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
