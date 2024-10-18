using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class CaseEnemySc : MonoBehaviour
{
	public Animator caseEnemyAnim;
	public Rigidbody2D caseEnemyRb;

	public GameObject targetCharacter;
	public GameObject bulletRedObj;
	public GameObject bulletRedGameObj;
	public GameObject caseEnemyDeathParticle;

	public Transform shotPos;

	private Vector3 targetPosition;

	public int caseEnemyHealth;

	public float caseEnemyMoveSpeed;
	public float caseEnemyMaxCooldown;
	private float caseEnemyCurrentCooldown;

	private void Start()
	{
		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		caseEnemyCurrentCooldown = caseEnemyMaxCooldown;
		PickUpTargetPosition();
	}

	private void Update()
	{
		if (transform.position == targetPosition)
		{
			PickUpTargetPosition();
		}

		GunShot();
		caseEnemyCurrentCooldown -= Time.deltaTime;
		if (caseEnemyHealth <= 0)
		{
			DestroyCaseEnemy();
		}
	}

	private void FixedUpdate()
	{
		transform.position = Vector2.MoveTowards(transform.position, targetPosition, caseEnemyMoveSpeed * Time.deltaTime);
	}

	void GunShot()
	{
		if (caseEnemyCurrentCooldown <= 0)
		{
			bulletRedGameObj = Instantiate(bulletRedObj, shotPos.position, Quaternion.identity);
			//bulletRedGameObj.GetComponent<BasicBulletSc>().gunGameObj = gameObject;

			caseEnemyCurrentCooldown = caseEnemyMaxCooldown;
		}
	}

	void CaseEnemyTakeDamage(int damage)
	{
		caseEnemyAnim.SetTrigger("HitTrigger");
		caseEnemyHealth -= damage;
	}

	void DestroyCaseEnemy()
	{
		Instantiate(caseEnemyDeathParticle, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bullet"))
		{
			collision.GetComponent<BasicBulletSc>().DestroyBullet();
			CaseEnemyTakeDamage(4);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, targetPosition);
		Gizmos.color = Color.green;
	}

	void EnemiesLayer()
	{
		if (transform.position.y > targetCharacter.transform.position.y)
		{
			//SpriteRenderer. = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder - 1;
		}
		else
		{
			//EnemySortingGroup.sortingOrder = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder + 1;
		}
	}

	void PickUpTargetPosition()
	{
		float randomX = Random.Range(-6, 14);
		float randomY = Random.Range(-18, 2);

		targetPosition = new Vector2(randomX, randomY);
	}
}
