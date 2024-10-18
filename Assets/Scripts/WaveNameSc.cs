using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveNameSc : MonoBehaviour
{
	public EnemySpawnerSc enemySpawnerSc;

	public TMP_Text waveNameText;

	[SerializeField] int waveIndex;

	private void Start()
	{
		enemySpawnerSc = FindObjectOfType<EnemySpawnerSc>();
	}

	private void Update()
	{
		waveIndex = enemySpawnerSc.currentWaveCount + 1;

		waveNameText.SetText("Wave " + waveIndex.ToString());
	}
}
