using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public int selectedCharacterIndex = 0;

	public void NextCharacter()
	{
		characters[selectedCharacterIndex].gameObject.SetActive(false);
		selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Length;
		characters[selectedCharacterIndex].gameObject.SetActive(true);
	}

	public void PreviousCharacter()
	{
		characters[selectedCharacterIndex].gameObject.SetActive(false);
		selectedCharacterIndex--;
		if(selectedCharacterIndex < 0)
		{
			selectedCharacterIndex += characters.Length;
		}
		characters[selectedCharacterIndex].gameObject.SetActive(true);
	}

	public void ChooseAndStartGame()
	{
		PlayerPrefs.SetInt("selectedCharacterIndex", selectedCharacterIndex);
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}

}
