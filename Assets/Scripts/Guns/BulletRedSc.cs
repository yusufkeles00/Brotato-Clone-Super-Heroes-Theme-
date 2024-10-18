using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRedSc : MonoBehaviour
{
	public Rigidbody2D bulletRb;

	public Transform bulletTargetEnemy;

	public GameObject bulletDestroyParticle;

	public int bulletRedDamage;

	public float bulletSpeed;
	public float bulletLifeTime;

	private void Start()
	{
		bulletTargetEnemy = GameObject.FindGameObjectWithTag("Player").transform;

		GoToTarget();
	}

	private void Update()
	{
		bulletLifeTime -= Time.deltaTime;
		if (bulletLifeTime <= 0)
		{
			DestroyBullet();
		}
	}

	private void FixedUpdate()
	{
		
	}

	void GoToTarget()
	{
		if (bulletTargetEnemy != null)
		{
			//transform.position = Vector2.MoveTowards(transform.position, bulletTargetEnemy.position, bulletSpeed * Time.deltaTime);

			Vector3 shotDir = bulletTargetEnemy.transform.position - transform.position;
			bulletRb.velocity = new Vector2(shotDir.x, shotDir.y).normalized * bulletSpeed;
		}
		else if (bulletTargetEnemy == null)
		{
			Destroy(gameObject);
		}
	}

	public void DestroyBullet()
	{
		Instantiate(bulletDestroyParticle, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<CharacterMovementScript>().CharacterTakeDamage(bulletRedDamage);
			DestroyBullet();
		}
	}

}
