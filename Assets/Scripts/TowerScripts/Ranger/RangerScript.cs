using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class RangerScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private int baseUpgradeCost = 100;

    private float targetingRangeBase;
    private float attackRateBase;
    
    private Transform target;
    private float attackCooldown;

    private int level = 1;

    private void Start()
    {
        targetingRangeBase = targetingRange;
        attackRateBase = attackRate;
        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {
        if (target.IsUnityNull())
        {
            FindTarget();
            return;
        }

        RotateTowardTarget();
        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            attackCooldown += Time.deltaTime;

            if (attackCooldown >= 1f / attackRate)
            {
                Shoot();
                attackCooldown = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, quaternion.identity);
        RangerArrowScript bulletScript = bullet.GetComponent<RangerArrowScript>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
            0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

	private void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = targetRotation;
    }

    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade()
    {
        if (CalcCost() > LevelManager.main.gold)
        {
            return;
        }

        LevelManager.main.Buy(CalcCost());
        level++;

        attackRate = CalcAttackRate();
        targetingRange = CalcRange();
        
        CloseUpgradeUI();
    }

    private int CalcCost()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalcAttackRate()
    {
        return attackRateBase * Mathf.Pow(level, 0.5f);
    }
    
    private float CalcRange()
    {
        return targetingRange * Mathf.Pow(level, 0.6f);
    }
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
