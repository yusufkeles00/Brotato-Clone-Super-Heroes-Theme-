using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class ProfessorBossSc : MonoBehaviour
{
	public SortingGroup EnemySortingGroup;

	public Rigidbody2D professorBossRb;
	public Animator professorBossAnim;

	public GameObject rocketBulletObj;
	public GameObject rocketBulletLaunchParticle;
	private GameObject popUpTextManagerObj;
	public GameObject targetCharacter;
	public GameObject deathParticle;
	public GameObject deathBloodSplashObj;
	public GameObject damageBloodSplashObj;

	public Transform[] rocketSpawnPositions;

	private Vector3 targetPos;

	public int professorBossHealth;
	public int rocketDamage;

	public float professorBossMoveSpeed;
	public float maxLaunchCooldown;
	public float currentLaunchCooldown;
	public float delayBetweenLaunch;

	private bool canMove = true;
	private bool isFaceRightEnemy = false;

	private void Start()
	{
		targetCharacter = GameObject.FindGameObjectWithTag("Player");
		popUpTextManagerObj = GameObject.FindGameObjectWithTag("PopUpManager");

		currentLaunchCooldown = maxLaunchCooldown;
	}

	private void Update()
	{
		EnemiesLayer();

		if (professorBossHealth <= 0)
		{
			EnemyDie();
		}

		currentLaunchCooldown -= Time.deltaTime;
	}

	private void FixedUpdate()
	{
		EnemyMovement();
	}

	private void RocketLaunch()
	{
		StartCoroutine(LaunchDelay());
	}

	IEnumerator LaunchDelay()
	{
		for (int i = 0; i < 4; i++)
		{
			yield return new WaitForSeconds(delayBetweenLaunch);

			professorBossAnim.SetTrigger("LaunchStart");
			Instantiate(rocketBulletLaunchParticle, rocketSpawnPositions[i].position, Quaternion.identity);
			Instantiate(rocketBulletObj, rocketSpawnPositions[i].position, Quaternion.identity);

			yield return new WaitForSeconds(0.4f);
		}

		professorBossAnim.SetTrigger("LaunchEnd");

		currentLaunchCooldown = maxLaunchCooldown;
		targetPos = PickUpTargetPosition();
		canMove = true;
	}

	void EnemyMovement()
	{
		if (currentLaunchCooldown <= 0 && canMove)
		{
			canMove = false;
			targetPos = transform.position;
			professorBossAnim.SetBool("isWalking", false);
			// Launch rocket
			RocketLaunch();
		}
		else if (transform.position == targetPos && canMove)
		{
			// go random position
			targetPos = PickUpTargetPosition();
		}

		if (canMove)
		{
			professorBossAnim.SetBool("isWalking", true);
			transform.position = Vector2.MoveTowards(transform.position, targetPos, professorBossMoveSpeed * Time.deltaTime);
		}

		if (transform.position.x > targetPos.x && isFaceRightEnemy == true)
		{
			EnemyFlipFace();
		}
		else if (transform.position.x < targetPos.x && isFaceRightEnemy == false)
		{
			EnemyFlipFace();
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

	void EnemyDie()
	{
		Instantiate(deathBloodSplashObj, transform.position, Quaternion.identity);
		Instantiate(deathParticle, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
