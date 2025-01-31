using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BardScript : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private LayerMask enemyMask;
	
	[Header("Attributes")]
	[SerializeField] private float targetingRange = 2f;
	[SerializeField] private float attackRate = 0.5f;
	[SerializeField] private float slowTime = 1f;

	private float attackCooldown;
	
	private void Update()
	{
		attackCooldown += Time.deltaTime;
		
		if (attackCooldown >= 1f / attackRate)
		{
			SlowEnemies();
			attackCooldown = 0f;
		}
	}

	private void SlowEnemies()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
			0f, enemyMask);

		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit2D hit = hits[i];
				EnemyPathing em = hit.transform.GetComponent<EnemyPathing>();
				em.UpdateSpeed(0.33f);

				StartCoroutine(ResetEnemySpeed(em));
			}
		}
	}

	private IEnumerator ResetEnemySpeed(EnemyPathing em)
	{
		yield return new WaitForSeconds(slowTime);
		
		em.ResetSpeed();
	}
	
	private void OnDrawGizmosSelected()
	{
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
}
