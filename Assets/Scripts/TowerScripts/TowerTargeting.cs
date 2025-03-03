using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TowerTargeting : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int id;

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletPrefab;

    private TowerStatsManager towerStats;
    private Transform target;
    private float attackCooldown;

    private void Start()
    {
        towerStats = gameObject.GetComponent<TowerStatsManager>();
    }
    
    private void Update()
    {
        if(target.IsUnityNull())
        {
            FindTarget();
            return;
        }
        rotateTowardsTarget();
        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            attackCooldown += Time.deltaTime;
        }

        if(attackCooldown > 1f / towerStats.GetAttackRate())
        {
            Attack(id);
            attackCooldown = 0f;
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerStats.GetTargetingRange(), (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerStats.GetTargetingRange();
    }

    private void rotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, towerStats.GetTargetingRange());
    }

    private void Attack(int num)
    {  
        switch (num)
	    {
		    case 0:
		    {
			    GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
                RangerArrow bulletScript = bullet.GetComponent<RangerArrow>();
                bulletScript.SetTarget(target);
			    break;
		    }
		    case 1:
		    {
                break;
		    }
            case 2:
		    {
                break;
		    }
            case 3:
		    {
                break;
		    }
            case 4:
		    {
                break;
		    }
            case 5:
		    {
                break;
		    }
            case 6:
		    {
                break;
		    }
            case 7:
		    {
                break;
		    }
            case 8:
		    {
                break;
		    }
            case 9:
		    {
                break;
		    }
            case 10:
		    {
                break;
		    }
            case 11:
		    {
                break;
		    }
	    }
    }
}
