using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGunSc : MonoBehaviour
{
	public RangeEnemySc rangeEnemy;

	private GameObject playerTargetObj;
	public GameObject bulletObjReference;
	private GameObject bulletObj;

	private Transform m_transform;
	public Transform shotPos;

	private SpriteRenderer gunSprite;

	public int gunDamage;

	public float gunRange;
	private float currentShotCooldown;
	public float maxShotCooldown;

	private void Start()
	{
		m_transform = this.transform;
		gunSprite = GetComponent<SpriteRenderer>();
		currentShotCooldown = maxShotCooldown;
	}

	private void Update()
	{
		playerTargetObj = GameObject.FindGameObjectWithTag("Player");
		GunRotation();
		currentShotCooldown -= Time.deltaTime;
		GunShot();
	}

	public void GunShot()
	{
		float distanceToCharacter = Vector2.Distance(transform.position, rangeEnemy.targetCharacter.transform.position);

		if(currentShotCooldown < 0 && distanceToCharacter <= gunRange)
		{
			//shot!
			bulletObj = Instantiate(bulletObjReference, shotPos.position, Quaternion.identity);
			bulletObj.GetComponent<BulletRedSc>().bulletSpeed = 7f;
			bulletObj.GetComponent<BulletRedSc>().bulletRedDamage = gunDamage;
			currentShotCooldown = maxShotCooldown;
		}
	}

	void GunRotation()
	{
		//Vector2 direction = Camera.main.ScreenToWorldPoint(playerTargetObj.transform.position) - m_transform.position; //This one is for the mouse!
		/*
		Vector2 direction = playerTargetObj.transform.position - m_transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
		m_transform.rotation = rotation;
		*/

		Vector2 direction = playerTargetObj.transform.position - transform.position;
		float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, angleZ + 180f);

		if(angleZ < 90 && angleZ > -90)
		{
			gunSprite.flipY = true;
		}
		else
		{
			gunSprite.flipY = false;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, gunRange);
		Gizmos.color = Color.red;
	}
}
