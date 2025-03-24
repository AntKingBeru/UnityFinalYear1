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
            // Ranger
		    case 0:
		    {
			    GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
                RangerArrowScript bulletScript = bullet.GetComponent<RangerArrowScript>();
                bulletScript.SetTarget(target);
			    break;
		    }
            // Barbarian
		    case 1:
		    {
                GameObject lightning = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
                WizardLightningScript lightningScript = lightning.GetComponent<WizardLightningScript>();
                lightningScript.SetTarget(target);
                break;
		    }
            // Bard
            case 2:
		    {
                GameObject curse = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
                WarlockCurseScript curseScript = curse.GetComponent<WarlockCurseScript>();
                curseScript.SetTarget(target);
                break;
		    }
            // Cleric
            case 3:
		    {
                break;
		    }
            // Druid
            case 4:
		    {
                break;
		    }
            // Fighter
            case 5:
		    {
                break;
		    }
            // Monk
            case 6:
		    {
                break;
		    }
            // Paladin
            case 7:
		    {
                break;
		    }
            // Rogue
            case 8:
		    {
                break;
		    }
            // Sorcerer
            case 9:
		    {
                break;
		    }
            // Warlock
            case 10:
		    {
                break;
		    }
            // Wizard
            case 11:
		    {
                break;
		    }
	    }
    }
}
