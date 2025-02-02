using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    
    [Header("References")]
    [SerializeField] private SetTower[] towers;

    private int selectedTower;

    private void Awake()
    {
        main = this;
    }

    public SetTower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int index)
    {
	    selectedTower = index;
    }
}
