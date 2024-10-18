using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterSprites : MonoBehaviour
{
	public GameObject characterObj;
	private GameObject headObj;
	private GameObject bodyObj;

	public Sprite[] headSprites;
	public Sprite[] bodySprites;

	public int selectedCharacterIndex;

	private void Start()
	{
		selectedCharacterIndex = PlayerPrefs.GetInt("selectedCharacterIndex");

		headObj = characterObj.transform.GetChild(0).gameObject;
		bodyObj = characterObj.transform.GetChild(1).gameObject;

		headObj.GetComponent<SpriteRenderer>().sprite = headSprites[selectedCharacterIndex];
		bodyObj.GetComponent<SpriteRenderer>().sprite = bodySprites[selectedCharacterIndex];

		
	}
}
