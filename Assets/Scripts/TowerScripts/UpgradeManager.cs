using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UpgradeManager : MonoBehaviour
{ 
    private TowerStatsManager stats;
    private TowerHealthManager hp;
    private int rangeLevel = 1;
    private int rateLevel = 1;
    private int dmgLevel = 1;

    private void Start()
    {
        stats = gameObject.GetComponent<TowerStatsManager>();
        hp = gameObject.GetComponent<TowerHealthManager>();
    }
    private void Update()
    {
        if (rangeLevel == 2 && rateLevel == 2 && dmgLevel == 2)
        {
            hp.SetHealth(2);
        }
        if (rangeLevel == 3 && rateLevel == 3 && dmgLevel == 3)
        {
            hp.SetHealth(3);
        }
        if (rangeLevel == 4 && rateLevel == 4 && dmgLevel == 4)
        {
            hp.SetHealth(4);
        }
    }

    private void UpgradeRange()
    {
        if (rangeLevel == 4)
        {
            return;
        }
        else
        {
            rangeLevel++;
            stats.SetRange(rangeLevel);
        }
    }

    private void UpgradeRate()
    {
        if (rateLevel == 4)
        {
            return;
        }
        else
        {
            rateLevel++;
            stats.SetRate(rateLevel);
        }
    }

    private void UpgradeDmg()
    {
        if (dmgLevel == 4)
        {
            return;
        }
        else
        {
            dmgLevel++;
            stats.SetDmg(dmgLevel);
        }
    }
}