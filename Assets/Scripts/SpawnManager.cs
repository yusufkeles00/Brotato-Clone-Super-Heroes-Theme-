using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public GameObject[] enemyList;
	public GameObject spawnPointWarn;

	private Vector2 spawnArea;

	public float spawnCooldown;

	private void Start()
	{
		StartCoroutine(Spawner());
	}

	private void Update()
	{
		float randomX = Random.Range(-6, 14);
		float randomY = Random.Range(-18, 2);

		spawnArea = new Vector2(randomX, randomY);
	}

	IEnumerator Spawner()
	{
		yield return new WaitForSeconds(spawnCooldown);
		Instantiate(spawnPointWarn, spawnArea, Quaternion.identity);
		StartCoroutine(Spawner());
	}
}
