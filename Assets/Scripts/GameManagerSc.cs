using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSc : MonoBehaviour
{
	private GameStopMenuSc gameStopMenuSc;

	public GameObject gameStopMenuObj;

	private bool isPanelActive;

	private void Start()
	{
		gameStopMenuSc = gameStopMenuObj.GetComponent<GameStopMenuSc>();
	}

	private void Update()
	{
		isPanelActive = gameStopMenuSc.isGamePanelActive;

		if (Input.GetKeyDown(KeyCode.Escape) && isPanelActive == false)
		{
			Time.timeScale = 0f;

			gameStopMenuObj.gameObject.SetActive(true);
			gameStopMenuSc.isGamePanelActive = true;
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && isPanelActive == true)
		{
			Time.timeScale = 1f;

			gameStopMenuObj.gameObject.SetActive(false);
			gameStopMenuSc.isGamePanelActive = false;
		}
	}
}
