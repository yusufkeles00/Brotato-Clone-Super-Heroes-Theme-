using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class DasherEnemySc : MonoBehaviour
{
	public SortingGroup EnemySortingGroup;
	private EnemySpawnerSc enemySpawner;

	public Rigidbody2D dasherEnemyRb;
	public Animator dasherAnim;

	public ParticleSystem dashParticle;
	public ParticleSystem.EmissionModule dashEmission;
	public ParticleSystem dashCircleParticle;
	public ParticleSystem.EmissionModule dashCircleEmission;

	private GameObject popUpTextManagerObj;
	public GameObject targetCharacter;
	public GameObject moneyDrop;
	public GameObject deathParticle;
	public GameObject deathBloodSplashObj;
	public GameObject damageBloodSplashObj;

	private Vector2 dashDir;
	private Vector3 targetPos;
	public Vector3 popTextOffset;

	public int enemyHealth;
	public int enemyDamage;
	[SerializeField] private int waveIndex;

	public float dashTime;
	public float dashForce;
	public float dashRange;
	public float maxDashCooldown;
	public float currentDashCooldown;
	public float dashFreezeTime;
	private float distanceToCharacter;
	public float enemyMoveSpeed;

	private bool isFaceRightEnemy = false;
	private bool isDashing = false;
	public bool canMove = true;

	private void Start()
	{
		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");

		enemySpawner = FindObjectOfType<EnemySpawnerSc>();
		dashEmission = dashParticle.emission;
		dashCircleEmission = dashCircleParticle.emission;
		dashEmission.rateOverTime = 0f;
		dashCircleEmission.rateOverTime = 0f;

		waveIndex = enemySpawner.currentWaveCount + 1;// Wave Index is equal to currentWaveCount + 1. Because currentWaveCount is 0 at the beginning
		enemyHealth += 11 * (waveIndex - 1);
		enemyDamage += Convert.ToInt32(0.85f * (waveIndex - 1));

		currentDashCooldown = maxDashCooldown;
	}

	private void Update()
	{
		distanceToCharacter = Vector2.Distance(transform.position, targetCharacter.transform.position);
		//dashDir = targetCharacter.transform.position - transform.position;

		EnemiesLayer();

		if (enemyHealth <= 0)
		{
			EnemyDie();
		}

		currentDashCooldown -= Time.deltaTime;
	}

	private void FixedUpdate()
	{
		EnemyMovement();
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

			DasherEnemyTakeDamage(bulletSc.bulletDamage, bulletSc.criticalDamageEnabled);
			bulletSc.DestroyBullet();
		}
	}

	IEnumerator EnemyDash()
	{
		dasherAnim.SetTrigger("DashStart");

		yield return new WaitForSeconds(dashFreezeTime);

		isDashing = true;
		dashEmission.rateOverTime = 15f;
		dashCircleEmission.rateOverTime = 8f;

		dashDir = targetCharacter.transform.position - transform.position;
		dasherEnemyRb.velocity = new Vector2(dashDir.x, dashDir.y).normalized * dashForce;
		//dasherEnemyRb.AddForce(dashDir * dashForce);

		yield return new WaitForSeconds(dashTime);

		targetPos = PickUpTargetPosition();

		dasherEnemyRb.velocity = new Vector2(0f, 0f);

		canMove = true;
		isDashing = false;
		dasherAnim.SetTrigger("DashEnd");
	}

	void EnemyMovement()
	{

		if (currentDashCooldown <= 0)
		{
			// go to player and dash
			targetPos = targetCharacter.transform.position;

			if (distanceToCharacter < dashRange && isDashing == false)
			{
				canMove = false;
				// dash
				StartCoroutine(EnemyDash());

				currentDashCooldown = maxDashCooldown;
			}
		}
		else if (transform.position == targetPos)
		{
			// go random position
			targetPos = PickUpTargetPosition();
		}

		// move to target
		if (canMove)
		{
			dasherAnim.SetBool("isWalking", true);
			dashEmission.rateOverTime = 0f;
			dashCircleEmission.rateOverTime = 0f;
			transform.position = Vector2.MoveTowards(transform.position, targetPos, enemyMoveSpeed * Time.deltaTime);
		}

		// flipping the face to player
		if (transform.position.x > targetPos.x && isFaceRightEnemy == true)
		{
			if(!isDashing)
			{
				EnemyFlipFace();
			}
		}
		else if (transform.position.x < targetPos.x && isFaceRightEnemy == false)
		{
			if (!isDashing)
			{
				EnemyFlipFace();
			}
		}
	}

	public void DasherEnemyTakeDamage(int damage, bool isCritical)
	{
		//enemyRb.AddForce(enemyMoveDirection * -50f);

		if (isCritical)
		{
			damage *= 2;
		}

		if (damage > enemyHealth && isCritical == false)
		{
			damage = enemyHealth;
		}
		enemyHealth -= damage;

		if (isCritical)
		{
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popTextOffset, Color.yellow);
		}
		else
		{
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popTextOffset, Color.white);
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


	void EnemiesLayer()
	{
		if (transform.position.y > targetCharacter.transform.position.y)
		{
			EnemySortingGroup.sortingOrder = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder - 1;
		}
		else
		{
			EnemySortingGroup.sortingOrder = targetCharacter.gameObject.GetComponent<SortingGroup>().sortingOrder + 1;
		}
	}

	Vector2 PickUpTargetPosition()
	{
		float randomX = Random.Range(-6, 14);
		float randomY = Random.Range(-18, 2);

		return new Vector2(randomX, randomY);
	}

	void EnemyFlipFace()
	{
		isFaceRightEnemy = !isFaceRightEnemy;
		transform.Rotate(0f, 180f, 0f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, targetPos);
		Gizmos.color = Color.green;
	}
}
