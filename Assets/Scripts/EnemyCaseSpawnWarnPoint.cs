using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCaseSpawnWarnPoint : MonoBehaviour
{
	public GameObject caseEnemy;

	private void Start()
	{
		StartCoroutine("SpawnCaseEnemy");
	}

	IEnumerator SpawnCaseEnemy()
	{
		yield return new WaitForSeconds(1.5f);

		Instantiate(caseEnemy, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
