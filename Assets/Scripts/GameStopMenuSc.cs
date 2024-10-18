using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStopMenuSc : MonoBehaviour
{
	public GameObject gameStopMenu;

	public bool isGamePanelActive = false;

	private void Start()
	{

	}

	public void ResumeGame()
	{
		Time.timeScale = 1;

		gameStopMenu.SetActive(false);
		isGamePanelActive = false;
	}

	public void SettingsMenu()
	{
		// go settings
		Debug.Log("Settings");
	}

	public void ExitGame()
	{
		// exit game
		Debug.Log("Exit");
	}
}
