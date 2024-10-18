using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SpawnPointWarn : MonoBehaviour
{
	SpawnManager spawnManager;

	private int a;

	private void Start()
	{
		spawnManager = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<SpawnManager>();

		a = Random.Range(0, spawnManager.enemyList.Length);

		StartCoroutine("SpawnEnemy");
	}

	private void Update()
	{

	}

	IEnumerator SpawnEnemy()
	{
		yield return new WaitForSeconds(2f);

		Instantiate(spawnManager.enemyList[a], transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
