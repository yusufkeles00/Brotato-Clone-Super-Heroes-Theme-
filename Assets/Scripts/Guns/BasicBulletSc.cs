using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletSc : MonoBehaviour
{
	public CharacterMovementScript characterMovementScript;

	public Rigidbody2D bulletRb;

	public Transform bulletTargetEnemy;

	public GameObject bulletDestroyParticle;
	public GameObject criticalDamageParticle;
	public GameObject gunGameObj;

	public int baseBulletDamage;
	public int bulletDamage;

	public int lifeStealChance;
	public int lifeStealRandomNum;

	public int criticalDamageChance;
	public int criticalDamageRandomNum;
	public int criticalDamageCoefficient;
	public bool criticalDamageEnabled = false;

	public float bulletSpeed;

	private void Start()
	{
		lifeStealRandomNum = Random.Range(0, 100);
		criticalDamageRandomNum = Random.Range(0, 100);

		characterMovementScript = FindObjectOfType<CharacterMovementScript>();

		lifeStealChance = characterMovementScript.characterLifeSteal;
		criticalDamageChance = characterMovementScript.characterCriticalDamage;

		bulletTargetEnemy = gunGameObj.GetComponent<BasicGunSc>().nearestEnemy;
	}

	private void Update()
	{
		bulletDamage = baseBulletDamage + (baseBulletDamage * (characterMovementScript.characterDamage / 100)); // Damage formula

		if(criticalDamageRandomNum < criticalDamageChance)
		{
			criticalDamageEnabled = true;
		}
	}

	private void FixedUpdate()
	{
		GoToTarget();
	}

	void GoToTarget()
	{
		if (bulletTargetEnemy != null)
		{
			transform.position = Vector2.MoveTowards(transform.position, bulletTargetEnemy.position, bulletSpeed * Time.deltaTime);
		}
		else if (bulletTargetEnemy == null)
		{
			Destroy(gameObject);
		}
	}

	public void DestroyBullet()
	{
		if(criticalDamageEnabled)
		{
			Instantiate(criticalDamageParticle, transform.position, Quaternion.identity);
			//Instantiate(bulletDestroyParticle,transform.position,Quaternion.identity);
		}
		else
		{
			Instantiate(bulletDestroyParticle,transform.position,Quaternion.identity);
		}
		Destroy(gameObject);
	}

}
