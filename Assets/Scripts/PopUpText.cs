using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
	public GameObject popUpText;

	public void CreateText(string symbole, int number, Transform textTranform, Vector3 offset, Color textColor)
	{
		GameObject popUpTextObj = Instantiate(popUpText, textTranform.position + offset, Quaternion.identity);

		popUpTextObj.GetComponent<TextMeshPro>().text = symbole + number.ToString();
		popUpTextObj.GetComponent<TextMeshPro>().color = textColor;

		Destroy(popUpTextObj, 1.1f);
	}
}
