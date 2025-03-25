using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TowerStatsManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float targetingRange;
    [SerializeField] private float range2;
    [SerializeField] private float range3;
    [SerializeField] private float range4;
    [SerializeField] public float attackRate;
    [SerializeField] private float rate2;
    [SerializeField] private float rate3;
    [SerializeField] private float rate4;
    [SerializeField] public int dmg;
    [SerializeField] private int dmg2;
    [SerializeField] private int dmg3;
    [SerializeField] private int dmg4;

    public float GetTargetingRange()
    {
        return targetingRange;
    }
    public float GetAttackRate()
    {
        return attackRate;
    }
    public int GetDmg()
    {
        return dmg;
    }

    public void SetRange(int level)
    {
        switch (level)
        {
            case 2:
            {
                targetingRange = range2;
                return;
            }
            case 3:
            {
                targetingRange = range3;
                return;
            }
            case 4:
            {
                targetingRange = range4;
                return;
            }
        }
    }

    public void SetRate(int level)
    {
        switch (level)
        {
            case 2:
            {
                attackRate = rate2;
                return;
            }
            case 3:
            {
                attackRate = rate3;
                return;
            }
            case 4:
            {
                attackRate = rate4;
                return;
            }
        }
    }

    public void SetDmg(int level)
    {
        switch (level)
        {
            case 2:
            {
                dmg = dmg2;
                return;
            }
            case 3:
            {
                dmg = dmg3;
                return;
            }
            case 4:
            {
                dmg = dmg4;
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
	{
		Handles.color = Color.yellow;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
}
