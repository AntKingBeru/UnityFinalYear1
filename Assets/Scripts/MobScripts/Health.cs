using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 7;
    [SerializeField] private int goldDrop = 25;
    [SerializeField] private int citizenKills = 1;
    [SerializeField] private Slider hp;

    private bool isDestroyed = false;
    private int hitPointsMax;

    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        hp.value = currentHp / maxHp;
    }
    
    private void Start()
    {
	    hitPointsMax = hitPoints;
        UpdateHealthBar(hitPoints, hitPointsMax);
    }

    void Update()
    {
        UpdateHealthBar(hitPoints, hitPointsMax);
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
