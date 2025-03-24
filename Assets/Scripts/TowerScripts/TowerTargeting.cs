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
    [SerializeField] private Transform? turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private Transform? firingPoint;
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

    private IEnumerator ResetEnemySpeed(EnemyPathing em)
	{
		yield return new WaitForSeconds((float)towerStats.GetDmg());
		
		em.ResetSpeed();
	}

    private void Attack(int num)
    {  
        switch (num)
	    {
            // Ranger
		    case 0:
		    {
			    GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
                RangerArrowScript bulletScript = bullet.GetComponent<RangerArrowScript>();
                bulletScript.SetTarget(target);
			    break;
		    }
            // Barbarian
		    case 1:
		    {
                break;
		    }
            // Bard
            case 2:
		    {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerStats.GetTargetingRange(), (Vector2)transform.position, 0f, enemyMask);

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
                break;
		    }
            // Cleric
            case 3:
		    {
                GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
                HealingOrbScript bulletScript = bullet.GetComponent<HealingOrbScript>();
                bulletScript.SetTarget(target);
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
                GameObject SneakAttack = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
                RougeSneakAttackScript sneakAttackScript = SneakAttack.GetComponent<RougeSneakAttackScript>();
                sneakAttackScript.SetTarget(target);
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
                GameObject curse = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
                WarlockCurseScript curseScript = curse.GetComponent<WarlockCurseScript>();
                curseScript.SetTarget(target);
                break;
		    }
            // Wizard
            case 11:
		    {
                GameObject lightning = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
                WizardLightningScript lightningScript = lightning.GetComponent<WizardLightningScript>();
                lightningScript.SetTarget(target);
                break;
		    }
	    }
    }
}
