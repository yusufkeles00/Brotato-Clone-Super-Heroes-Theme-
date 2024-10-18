using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketBulletFallSc : MonoBehaviour
{
	public Rigidbody2D rocketBulletFallRb;

	private GameObject characterObject;
	public GameObject targetAreaObj;
	private GameObject targetAreaObjTemp;
	public GameObject explosionParticle;

	public Transform targetPos;

	public float rocketBulletFallSpeed;


	private void Start()
	{
		characterObject = GameObject.FindGameObjectWithTag("Player");
		targetPos = characterObject.transform;

		StartCoroutine(TargetDelay());
	}

	private void Update()
	{
		if (transform.position.y <= targetAreaObjTemp.transform.position.y)
		{
			DestroyRocketBullet();
		}
	}

	IEnumerator TargetDelay()
	{
		targetAreaObjTemp = Instantiate(targetAreaObj, targetPos.position, Quaternion.identity);

		yield return new WaitForSeconds(1f);

		transform.position = new Vector3(targetAreaObjTemp.transform.position.x, targetAreaObjTemp.transform.position.y + 25f, transform.position.z);
		rocketBulletFallRb.velocity = new Vector3(0f, -rocketBulletFallSpeed, 0f);
	}

	private void DestroyRocketBullet()
	{
		CinemachineShake.Instance.ShakeCamera(10f, 0.25f);
		Instantiate(explosionParticle, transform.position, Quaternion.identity);
		Destroy(targetAreaObjTemp);
		Destroy(gameObject);
	}
}
