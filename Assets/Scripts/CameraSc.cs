using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSc : MonoBehaviour
{
	public float minX;
	public float maxX;

	public float minY;
	public float maxY;

	private void FixedUpdate()
	{
		Mathf.Clamp(transform.position.x, minX, maxX);
		Mathf.Clamp(transform.position.y, minY, maxY);
	}
}
