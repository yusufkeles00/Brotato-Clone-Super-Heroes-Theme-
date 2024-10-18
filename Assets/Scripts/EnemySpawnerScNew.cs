using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScNew : MonoBehaviour
{
	[System.Serializable]
	public class Wave
	{
		public List<EnemyGroups> enemyGroups;

		public string waveNo;
		public int waveQuota;
	}

	[System.Serializable]
	public class EnemyGroups
	{
		public string enemyName;
		public int enemyCount;
		public GameObject enemyObj;
	}

	public List<Wave> waves;
}
