using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;
    public int gold;
    public int life;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
	    gold = 650;
	    life = 150;
    }

    public void AddGold(int amount)
    {
	    gold += amount;
    }

    public void LoseLife(int amount)
    {
	    if (amount <= life)
	    {
		    life -= amount;
		    return;
	    }

	    return;
    }

    public bool Buy(int amount)
    {
	    if (amount <= gold)
	    {
		    //buy
		    gold -= amount;
		    return true;
	    }
	    return false;
    }
}
