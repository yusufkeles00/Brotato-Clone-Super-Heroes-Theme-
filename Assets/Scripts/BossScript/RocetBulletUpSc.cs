using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocetBulletUpSc : MonoBehaviour
{
	public Rigidbody2D rocketBulletRb;

	public GameObject rocketBulletFallObj;

	public float rocketBulletSpeed;

	private void Start()
	{
		rocketBulletRb.velocity = new Vector3(0f, rocketBulletSpeed, 0f);

		StartCoroutine(SpawnRocketBuletFallObject());
	}

	IEnumerator SpawnRocketBuletFallObject()
	{
		yield return new WaitForSeconds(2.5f);

		Instantiate(rocketBulletFallObj, new Vector3(transform.position.x, 10f, transform.position.z), Quaternion.identity);
		Destroy(gameObject);
	}
}
