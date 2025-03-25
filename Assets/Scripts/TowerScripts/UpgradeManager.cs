using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class UpgradeManager : MonoBehaviour
{ 
    [SerializeField] private GameObject upgrades;
    [SerializeField] private Button[] clicks;
    private TowerStatsManager stats;
    private TowerHealthManager hp;
    private int rangeLevel = 1;
    private int rateLevel = 1;
    private int dmgLevel = 1;
    [SerializeField] TextMeshProUGUI weaponCost;
    [SerializeField] TextMeshProUGUI gearCost;
    [SerializeField] TextMeshProUGUI classCost;
    private int weaponPrice = 100;
    private int gearPrice = 100;
    private int classPrice = 100;

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

    public void UpgradeRange()
    {
        if (rangeLevel == 4)
        {
            clicks[1].interactable = false;
            return;
        }
        else
        {
            if (LevelManager.main.gold >= gearPrice)
            {
                LevelManager.main.Buy(gearPrice);
                rangeLevel++;
                gearPrice += 50;
                stats.SetRange(rangeLevel);
            }
        }
    }

    public void UpgradeRate()
    {
        if (rateLevel == 4)
        {
            clicks[2].interactable = false;
            return;
        }
        else
        {
            if (LevelManager.main.gold >= classPrice)
            {
                LevelManager.main.Buy(classPrice);
                rateLevel++;
                classPrice += 50;
                stats.SetRate(rateLevel);
            }
        }
    }

    public void UpgradeDmg()
    {
        if (dmgLevel == 4)
        {
            clicks[0].interactable = false;
            return;
        }
        else
        {
            if (LevelManager.main.gold >= weaponPrice)
            {
                LevelManager.main.Buy(weaponPrice);
                dmgLevel++;
                weaponPrice += 50;
                stats.SetDmg(dmgLevel);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            upgrades.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            upgrades.SetActive(false);
        }
    }

    private void OnGUI()
	{
		weaponCost.text = weaponPrice.ToString() + " GP";
		gearCost.text = gearPrice.ToString() + " GP";
		classCost.text = classPrice.ToString() + " GP";
	}
}