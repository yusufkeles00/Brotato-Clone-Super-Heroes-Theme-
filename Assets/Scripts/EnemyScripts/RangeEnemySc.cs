using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RangeEnemySc : MonoBehaviour
{
	private EnemySpawnerSc enemySpawner;

	public SortingGroup rangeEnemySortingGroup;

	public Rigidbody2D rangeEnemyRb;
	public Animator rangeEnemyAnim;

	public GameObject rangeEnemysGun;
	private GameObject popUpTextManagerObj;
	public GameObject targetCharacter;
	public GameObject moneyDrop;
	public GameObject deathParticle;
	public GameObject deathBloodSplashObj;
	public GameObject damageBloodSplashObj;

	public Vector3 popUpTextOffset;

	public int rangeEnemyHealth;
	[SerializeField] private int waveIndex;

	public float rangeEnemyMoveSpeed;
	public float maxDistanceToCharacter;
	private float distanceToCharacter;

	private bool isFaceRightEnemy = false;

	private void Start()
	{
		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");

		enemySpawner = FindObjectOfType<EnemySpawnerSc>();

		waveIndex = enemySpawner.currentWaveCount + 1;
		rangeEnemyHealth += 1 * (waveIndex - 1);

		rangeEnemysGun.GetComponent<PistolGunSc>().gunDamage += Convert.ToInt32(0.6f * (waveIndex - 1));
	}

	private void FixedUpdate()
	{
		EnemyMovement();

	}

	private void Update()
	{
		distanceToCharacter = Vector2.Distance(transform.position, targetCharacter.transform.position);

		EnemiesLayer();

		if (rangeEnemyHealth <= 0)
		{
			EnemyDie();
		}
	}

	void EnemyMovement()
	{
		if (distanceToCharacter > maxDistanceToCharacter)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetCharacter.transform.position, rangeEnemyMoveSpeed * Time.deltaTime);
		}
		else if (distanceToCharacter < maxDistanceToCharacter - 1f)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetCharacter.transform.position, -1 * rangeEnemyMoveSpeed * Time.deltaTime);
		}

		if (transform.position.x > targetCharacter.transform.position.x && isFaceRightEnemy == true)
		{
			EnemyFlipFace();
		}
		else if (transform.position.x < targetCharacter.transform.position.x && isFaceRightEnemy == false)
		{
			EnemyFlipFace();
		}

		if (distanceToCharacter > maxDistanceToCharacter)
		{
			rangeEnemyAnim.SetBool("isWalking", true);
		}
		else
		{
			rangeEnemyAnim.SetBool("isWalking", false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Bullet"))
		{
			var bulletSc = collision.GetComponent<BasicBulletSc>();

			// life steal
			if (bulletSc.lifeStealRandomNum < bulletSc.lifeStealChance && bulletSc.characterMovementScript.lifeStealCooldown < 0)
			{
				bulletSc.characterMovementScript.characterCurrentHealth += 1;
				bulletSc.characterMovementScript.lifeStealCooldown = 1;
			}

			bulletSc.DestroyBullet();
			RangeEnemyTakeDamage(bulletSc.bulletDamage, bulletSc.criticalDamageEnabled);
		}
	}

	public void RangeEnemyTakeDamage(int damage, bool isCritical)
	{
		rangeEnemyAnim.SetTrigger("HitTrigger");

		if (isCritical)
		{
			damage *= 2;
		}


		if (damage > rangeEnemyHealth && isCritical == false)
		{
			damage = rangeEnemyHealth;
		}
		rangeEnemyHealth -= damage;

		if (isCritical)
		{
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popUpTextOffset, Color.yellow);
		}
		else
		{
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popUpTextOffset, Color.white);
		}
		Instantiate(damageBloodSplashObj, transform.position, Quaternion.identity);
	}

	void EnemyDie()
	{
		Instantiate(deathBloodSplashObj, transform.position, Quaternion.identity);
		Instantiate(deathParticle, transform.position, Quaternion.identity);
		Instantiate(moneyDrop, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}


	void EnemyFlipFace()
	{
		isFaceRightEnemy = !isFaceRightEnemy;
		transform.Rotate(0f, 180f, 0f);
	}

	void EnemiesLayer()
	{
		if (transform.position.y > targetCharacter.transform.position.y)
		{
			rangeEnemySortingGroup.sortingOrder = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder - 1;
		}
		else
		{
			rangeEnemySortingGroup.sortingOrder = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder + 1;
		}
	}
}
