using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicEnemySc : MonoBehaviour
{
	[SerializeField] AudioSource punchAttackSound;

	public SortingGroup EnemySortingGroup;
	private EnemySpawnerSc enemySpawner;

	public Rigidbody2D enemyRb;
	public Animator enemyAnim;

	private GameObject popUpTextManagerObj;
	public GameObject targetCharacter;
	public GameObject moneyDrop;
	public GameObject deathParticle;
	public GameObject deathBloodSplashObj;
	public GameObject damageBloodSplashObj;

	private Vector2 enemyMoveDirection;

	public Vector3 popTextOffset;

	public int enemyHealth;
	public int enemyDamage;
	[SerializeField] private int waveIndex;

	private float distanceToCharacter;
	private float hitCooldown;
	public float enemyMoveSpeed;
	public float maxDistanceToCharacter;
	public float maxHitCooldown;

	private bool isFaceRightEnemy = false;

	private void Start()
	{
		hitCooldown = maxHitCooldown;

		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");

		enemySpawner = FindObjectOfType<EnemySpawnerSc>();

		waveIndex = enemySpawner.currentWaveCount + 1;// Wave Index is equal to currentWaveCount + 1. Because currentWaveCount is 0 at the beginning
		enemyHealth += 2 * (waveIndex - 1);
		enemyDamage += Convert.ToInt32(0.6f * (waveIndex - 1));
	}

	private void Update()
	{
		enemyMoveDirection = targetCharacter.transform.position - transform.position;
		distanceToCharacter = Vector2.Distance(transform.position, targetCharacter.transform.position);

		EnemiesLayer();

		if (enemyHealth <= 0)
		{
			EnemyDie();
		}

		hitCooldown -= Time.deltaTime;

		EnemyAttack();
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

			BasicEnemyTakeDamage(bulletSc.bulletDamage, bulletSc.criticalDamageEnabled);
			bulletSc.DestroyBullet();
		}
	}

	void EnemyAttack()
	{
		if (hitCooldown <= 0 && distanceToCharacter <= maxDistanceToCharacter)
		{
			enemyAnim.SetTrigger("Attack");
			punchAttackSound.Play();
			targetCharacter.GetComponent<CharacterMovementScript>().CharacterTakeDamage(enemyDamage);
			hitCooldown = maxHitCooldown;
		}
	}

	public void BasicEnemyTakeDamage(int damage, bool isCritical)
	{
		//enemyRb.AddForce(enemyMoveDirection * -50f);
		enemyAnim.SetTrigger("HitTrigger");

		if (isCritical)
		{
			damage *= 2;
		}

		if (damage > enemyHealth && isCritical == false)
		{
			damage = enemyHealth;
		}
		enemyHealth -= damage;

		if(isCritical)
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

	void EnemyMovement()
	{
		if (distanceToCharacter >= maxDistanceToCharacter)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetCharacter.transform.position, enemyMoveSpeed * Time.deltaTime);
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
			enemyAnim.SetBool("isWalking", true);
		}
		else
		{
			enemyAnim.SetBool("isWalking", false);
		}
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

	void EnemyFlipFace()
	{
		isFaceRightEnemy = !isFaceRightEnemy;
		transform.Rotate(0f, 180f, 0f);
	}
}
