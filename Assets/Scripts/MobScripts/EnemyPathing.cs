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
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private LayerMask enemyMask;

    private Transform target;
    private Transform targetTower = null;
    private int pathIndex = 0;
    private float attackRate = 2.5f;
    private float attackCooldown;
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
            }
        }
        if(targetTower.IsUnityNull())
        {
            FindTarget();
            return;
        }
        rotateTowardsTarget();
        if (!CheckTargetInRange())
        {
            targetTower = null;
        }
        else
        {
            attackCooldown += Time.deltaTime;
        }

        if(attackCooldown > 1f / attackRate)
        {
            AttackTower();
            attackCooldown = 0f;
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
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

    private void rotateTowardsTarget()
    {
        float angle = Mathf.Atan2(targetTower.position.y - transform.position.y, targetTower.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = targetRotation;
    }

    private void AttackTower()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, rotationPoint.rotation);
        GoblinAttack bulletScript = bullet.GetComponent<GoblinAttack>();
        bulletScript.SetTarget(targetTower);
    }

    private void OnDrawGizmosSelected()
	{
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(transform.position, transform.forward, range);
	}
}
