using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
	[Header("Attributes")]
	[SerializeField] private int hitPoints = 15;

	private bool isDestroyed = false;
	private int hitPointsMax;
    
	public void Heal(int heal)
	{
		hitPoints += heal;
		if (hitPoints > hitPointsMax)
			hitPoints = hitPointsMax;
	}
}
