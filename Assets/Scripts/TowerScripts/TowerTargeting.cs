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
    [SerializeField] private TowerStatsManager towerStats;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject bulletPrefab;


    private Transform target;
    private float attackCooldown;

    private void Update()
    {
        if(target.IsUnityNull())
        {
            FindTarget();
            return;
        }
        rotateTowardsTarget()
        if (!CheckTargetInRange()
        {
            target = null;
        }
        else
        {
            attackCooldown += Time.deltaTime;
        }

        if(attackCooldown > 1f / towerStats.attackRate)
        {
            Attack(id);
            attackCooldown = 0f;
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerStats.targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerStats.targetingRange;
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
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
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

		    }
            case 2:
		    {
                
		    }
            case 3:
		    {
                
		    }
            case 4:
		    {
                
		    }
            case 5:
		    {
                
		    }
            case 6:
		    {
                
		    }
            case 7:
		    {
                
		    }
            case 8:
		    {
                
		    }
            case 9:
		    {
                
		    }
            case 10:
		    {
                
		    }
            case 11:
		    {
                
		    }
	    }
    }
}
