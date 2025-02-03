using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    
    private GameObject towerObj;
    private RangerScript ranger;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI()) return;
        
        if (towerObj != null)
        {
            ranger.OpenUpgradeUI();
            return;
        }

        SetTower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.gold)
        {
	        return;
        }
        
        LevelManager.main.Buy(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        ranger = towerObj.GetComponent<RangerScript>();
    }
}
