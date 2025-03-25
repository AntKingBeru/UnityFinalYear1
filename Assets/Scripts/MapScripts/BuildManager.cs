using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    
    [Header("References")]
    [SerializeField] private SetTower[] towers;

    private int selectedTower = 11;

    private void Awake()
    {
        main = this;
    }

    public SetTower GetSelectedTower()
    {
	    if (towers[selectedTower] == null)
	    {
		    return null;
	    }
        return towers[selectedTower];
    }

    public void SetSelectedTower(int index)
    {
	    selectedTower = index;
    }
}
