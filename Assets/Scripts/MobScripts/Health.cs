using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 7;
    [SerializeField] private int goldDrop = 25;
    [SerializeField] private int citizenKills = 1;

    private bool isDestroyed = false;
    private int hitPointsMax;

    private void Start()
    {
	    hitPointsMax = hitPoints;
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.OnEnemyDestroy.Invoke();
            LevelManager.main.AddGold(goldDrop);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
	    hitPoints += heal;
	    if (hitPoints > hitPointsMax)
		    hitPoints = hitPointsMax;
    }

    public void DoDamage()
    {
	    LevelManager.main.LoseLife(citizenKills);
    }
}
