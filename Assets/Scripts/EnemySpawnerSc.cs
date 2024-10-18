using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSc : MonoBehaviour
{
	[System.Serializable]
	public class Wave
	{
		public string waveName;
		public List<EnemyGroups> enemyGroups;  //List of groups of enemies to spawn in this wave
		public int waveQuota;  //Total number of enemies to spawn in this wave
		public int spawnCount;  //Number of enemies already spawned in this wave
		public float spawnInterval;  //Spawner time
	}

	[System.Serializable]
	public class EnemyGroups
	{
		public string enemyName;
		public int enemyCount;	
		public int spawnCount;
		public GameObject enemyPrefab;
	}

	public List<Wave> waves;  //All list of the waves in game

	public int currentWaveCount = 0;
	int currentWaveQuota = 0;

	float spawnTimer;
	public float waveInterval;

	private bool waveStarted = false;

	private void Awake()
	{
		CalculateWaveQuota();
	}

	private void Start()
	{
		//CalculateWaveQuota();
		
		SpawnEnemies();
	}

	private void Update()
	{
		//currentWaveCount < waves.Count && 
		Debug.Log("spawnCount: " + waves[currentWaveCount].spawnCount + "    waveQuota: " + waves[currentWaveCount].waveQuota + "    currentWaveCount: " + currentWaveCount + "    currentWaveQuota: " + currentWaveQuota);

		if (!waveStarted && waves[currentWaveCount].waveQuota == currentWaveQuota) //check if the wave has ended
		{
			Debug.Log("Next");
			StartCoroutine(BeginNextWave());
			waveStarted = true;
		}

		spawnTimer += Time.deltaTime;

		if(spawnTimer >= waves[currentWaveCount].spawnInterval)
		{
			spawnTimer = 0f;
			SpawnEnemies();
		}
	}

	IEnumerator BeginNextWave()
	{
		yield return new WaitForSeconds(waveInterval);

		if(currentWaveCount < waves.Count - 1)
		{
			currentWaveCount++;
			CalculateWaveQuota();
			//waveStarted = false;
		}
	}

	void CalculateWaveQuota()
	{
		currentWaveQuota = 0;
		foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
		{
			currentWaveQuota += enemyGroup.enemyCount;
		}
		waves[currentWaveCount].waveQuota = currentWaveQuota;
	}

	void SpawnEnemies()
	{
		if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
		{
			foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
			{
				if (enemyGroup.spawnCount < enemyGroup.enemyCount)
				{
					//Spawn Point Warn then the enemy
					float randomX = Random.Range(-6, 14);
					float randomY = Random.Range(-18, 2);

					Vector2 spawnArea = new Vector2(randomX, randomY);

					Instantiate(enemyGroup.enemyPrefab, spawnArea, Quaternion.identity);
					//Increasing Counts
					enemyGroup.spawnCount++;
					waves[currentWaveCount].spawnCount++;
				}
			}
			waveStarted = false;
		}
	}
}
