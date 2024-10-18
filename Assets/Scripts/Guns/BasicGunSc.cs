using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunSc : MonoBehaviour
{
	private CharacterMovementScript characterMovementScript;

	private AudioSource shotSound;

	public Rigidbody2D gunRb;
	public Animator gunAnim;

	public GameObject bulletObj;
	public GameObject bulletGameObj;

	public Transform nearestEnemy = null;
	Transform a_transform;

	[SerializeField]
	private LayerMask enemiesLayer;

	public float baseGunRange;
	public float gunRangeAdditional;

	public float baseGunCooldown;
	public float gunCooldown;
	private float currentCooldown;
	public float attackSpeed;

	private bool isGunFaceRight = false;


	private void Start()
	{
		characterMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovementScript>();

		currentCooldown = baseGunCooldown;

		shotSound = GetComponent<AudioSource>();
	}

	private void Update()
	{
		GunFlip();

		attackSpeed = baseGunCooldown - (baseGunCooldown * (characterMovementScript.characterAttackSpeed / 100));
		gunCooldown = attackSpeed;

		gunRangeAdditional = baseGunRange + (characterMovementScript.characterRange * 0.12f);

		currentCooldown -= Time.deltaTime;
	}

	private void FixedUpdate()
	{
		TargetEnemy();
		//BasicGunRotation();
		GunShot();
	}

	void TargetEnemy()
	{
		Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, gunRangeAdditional, enemiesLayer);

		float minDist = Mathf.Infinity;

		foreach (Collider2D enemy in collider)
		{
			float dist = Vector3.Distance(transform.position, enemy.transform.position);

			if (dist < minDist)
			{
				// If it's not work. Use it as a GameObject
				nearestEnemy = enemy.transform;
				minDist = dist;
				//don't worry bro. It's worked :)
			}
		}
	}

	void BasicGunRotation()
	{
		if (nearestEnemy != null)
		{
			Vector2 direction = nearestEnemy.position - a_transform.position;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
			a_transform.rotation = rotation;
		}
	}

	void GunShot()
	{
		if (currentCooldown <= 0 && nearestEnemy != null)
		{
			gunAnim.SetTrigger("shot");
			shotSound.Play();
			bulletGameObj = Instantiate(bulletObj, transform.position, Quaternion.identity);
			bulletGameObj.GetComponent<BasicBulletSc>().gunGameObj = gameObject;

			currentCooldown = gunCooldown;
		}
	}

	void GunFlip()
	{
		if (nearestEnemy != null)
		{
			if (transform.position.x < nearestEnemy.transform.position.x && isGunFaceRight == false)
			{
				isGunFaceRight = !isGunFaceRight;
				transform.Rotate(0f, 180f, 0f);
			}
			else if (transform.position.x > nearestEnemy.transform.position.x && isGunFaceRight == true)
			{
				isGunFaceRight = !isGunFaceRight;
				transform.Rotate(0f, 180f, 0f);
			}
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, gunRangeAdditional);
		if (nearestEnemy != null)
		{
			Gizmos.DrawLine(transform.position, nearestEnemy.position);
		}
		Gizmos.color = Color.green;
	}
}
