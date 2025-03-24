using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EnemyPathing : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float dmg = 1f;
    [SerializeField] public float range = 2f;
    [SerializeField] private LayerMask enemyMask;

    private Transform target;
    private Transform targetTower;
    private int pathIndex = 0;
    private float attackRate = 10f;
    private float attackCooldown = 0.1f;
    private float baseSpeed;

    private void Start()
    {
	    baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.OnEnemyDestroy.Invoke();
                gameObject.GetComponent<Health>().DoDamage();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
                FindTarget();
                attackCooldown += Time.deltaTime;
                if(attackCooldown > 1f / attackRate)
                {
                    targetTower.GetComponent<TowerHealthManager>().TakeDamage(dmg);
                    Debug.Log($"Pow!!!! {targetTower} took {dmg} dmg");
                    attackCooldown = 0f;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
	    moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
	    moveSpeed = baseSpeed;
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        if (hits.Length > 0)
        {
            targetTower = hits[0].transform;
        }
        else
        {
            targetTower = null;
        }
    }
    private bool CheckTargetInRange()
    {
        return Vector2.Distance(targetTower.position, transform.position) <= range;
    }

    private void OnDrawGizmosSelected()
	{
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(transform.position, transform.forward, range);
	}
}
