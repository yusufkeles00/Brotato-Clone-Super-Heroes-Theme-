using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaseSc : MonoBehaviour
{
	public GameObject enemyCaseSpawnPointWarn;
	public GameObject enemyCaseDestroyParticleObj;

	private Vector2 enemyCaseSpawnArea;

	private float lootCaseSpawnAreaX;
	private float lootCaseSpawnAreaY;

	[SerializeField] private float enemyCaseLifeTime;

	private void Start()
	{
		StartCoroutine("DestroyCase");
		StartCoroutine("A");
	}

	private void Update()
	{
		lootCaseSpawnAreaX = Random.Range(transform.position.x - 2, transform.position.x + 2);
		lootCaseSpawnAreaY = Random.Range(transform.position.y - 2, transform.position.y + 2);
	}

	IEnumerator DestroyCase()
	{
		yield return new WaitForSeconds(enemyCaseLifeTime);
		Instantiate(enemyCaseDestroyParticleObj, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	IEnumerator A()
	{
		yield return new WaitForSeconds(enemyCaseLifeTime - 0.35f);

		for (int i = 0; i < 3; i++)
		{
			enemyCaseSpawnArea = new Vector2(lootCaseSpawnAreaX, lootCaseSpawnAreaY);

			Instantiate(enemyCaseSpawnPointWarn, enemyCaseSpawnArea, Quaternion.identity);

			yield return new WaitForSeconds(0.1f);
		}
	}
}
