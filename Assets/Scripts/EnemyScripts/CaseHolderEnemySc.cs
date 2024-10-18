using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CaseHolderEnemySc : MonoBehaviour
{
	EnemySpawnerSc enemySpawner;

	public SortingGroup EnemySortingGroup;

	public Rigidbody2D enemyRb;
	public Animator enemyAnim;

	private GameObject popUpTextManagerObj;
	public GameObject targetCharacter;
	public GameObject moneyDrop;
	public GameObject caseObjectDrop;
	public GameObject deathParticle;
	public GameObject deathBloodSplashObj;
	public GameObject damageBloodSplashObj;

	public Vector3 popUpTextOffset;

	public int enemyHealth;
	[SerializeField] private int waveIndex;

	public float enemyMoveSpeed;
	public float maxDistanceToCharacter;
	private float distanceToCharacter;

	private bool isFaceRightEnemy = false;

	private void Start()
	{
		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");

		enemySpawner = FindObjectOfType<EnemySpawnerSc>();

		waveIndex = enemySpawner.currentWaveCount + 1;
		enemyHealth += 1 * (waveIndex - 1);

		Invoke("EnemyDie", 4f);
	}

	private void Update()
	{
		distanceToCharacter = Vector2.Distance(transform.position, targetCharacter.transform.position);

		EnemiesLayer();

		if (enemyHealth <= 0)
		{
			EnemyDie();
		}
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

			bulletSc.DestroyBullet();
			CaseHolderEnemyTakeDamage(bulletSc.bulletDamage, bulletSc.criticalDamageEnabled);
		}
	}
	public void CaseHolderEnemyTakeDamage(int damage, bool isCritical)
	{
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
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popUpTextOffset, Color.yellow);
		}
		else
		{
			popUpTextManagerObj.GetComponent<PopUpText>().CreateText("", damage, gameObject.transform, popUpTextOffset, Color.white);
		}
		Instantiate(damageBloodSplashObj, transform.position, Quaternion.identity);
	}

	void EnemyMovement()
	{
		if (distanceToCharacter >= maxDistanceToCharacter)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetCharacter.transform.position, enemyMoveSpeed * Time.deltaTime);
		}
		else if (distanceToCharacter < maxDistanceToCharacter)
		{
			//Move to further position from player
			
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

	void EnemyDie()
	{
		Instantiate(deathBloodSplashObj, transform.position, Quaternion.identity);
		Instantiate(deathParticle, transform.position, Quaternion.identity);
		Instantiate(moneyDrop, transform.position, Quaternion.identity);
		Instantiate(caseObjectDrop, transform.position, Quaternion.identity);

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

	void EnemyFlipFace()
	{
		isFaceRightEnemy = !isFaceRightEnemy;
		transform.Rotate(0f, 180f, 0f);
	}
}
