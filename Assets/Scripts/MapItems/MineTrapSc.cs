using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrapSc : MonoBehaviour
{
	public GameObject mineExplosionParticle;

	[SerializeField]
	private LayerMask enemiesLayer;

	public int explosionDamage;

	public float explosionDelay;
	public float explosionArea;

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Enemy"))
		{
			Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionArea, enemiesLayer);

			foreach (Collider2D enemy in enemies)
			{
				Rigidbody2D rbEnemy = enemy.GetComponent<Rigidbody2D>();
				if (rbEnemy != null &&  rbEnemy.tag == "Enemy")
				{
					CinemachineShake.Instance.ShakeCamera(10f, 0.25f);
					if (rbEnemy.GetComponent<RangeEnemySc>() != null)
					{
						RangeEnemySc rangeEnemy = rbEnemy.GetComponent<RangeEnemySc>();
						rangeEnemy.RangeEnemyTakeDamage(explosionDamage, false);
					}
					if (rbEnemy.GetComponent<BasicEnemySc>() != null)
					{
						BasicEnemySc rangeEnemy = rbEnemy.GetComponent<BasicEnemySc>();
						rangeEnemy.BasicEnemyTakeDamage(explosionDamage, false);
					}
					if(rbEnemy.GetComponent<CaseHolderEnemySc>() != null)
					{
						CaseHolderEnemySc rangeEnemy = rbEnemy.GetComponent<CaseHolderEnemySc>();
						rangeEnemy.CaseHolderEnemyTakeDamage(explosionDamage, false);
					}

					Instantiate(mineExplosionParticle, transform.position, Quaternion.identity);

					Destroy(gameObject);
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, explosionArea);
	}
}
