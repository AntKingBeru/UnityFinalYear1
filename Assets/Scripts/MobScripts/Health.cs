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
    [SerializeField] private float damageMultiplier = 1f; // Default no multiplier
    [SerializeField] private float debuffDuration = 0f; 
    [SerializeField] private float dotDamagePerTick = 0f; // Damage per DoT tick
    [SerializeField] private float dotInterval = 0.5f;    // Time between DoT ticks
    [SerializeField] private float dotDuration = 0f;      // Remaining DoT duration
    private float dotTimer = 0f;  // Time remaining for debuff

    private bool isDestroyed = false;
    private int hitPointsMax;

    private void Start()
    {
	    hitPointsMax = hitPoints;
    }

    public void TakeDamage(int dmg)
    {
        int adjustedDamage = Mathf.RoundToInt(dmg * damageMultiplier);
        hitPoints -= adjustedDamage;

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

        void Update()
    {
        if (debuffDuration > 0)
        {
            debuffDuration -= Time.deltaTime;
            if (debuffDuration <= 0)
            {
                damageMultiplier = 1f; // Reset multiplier when debuff expires
            }   
        }

        if (dotDuration > 0)
        {
            dotTimer -= Time.deltaTime;
            dotDuration -= Time.deltaTime;

            if (dotTimer <= 0)
            {
                int damage = Mathf.RoundToInt(dotDamagePerTick);
                hitPoints -= damage;
                dotTimer = dotInterval; // Reset for next tick
            }

            if (dotDuration <= 0)
            {
                dotDamagePerTick = 0f; // Clear DoT
            }

            if (hitPoints <= 0 && !isDestroyed)
            {
                EnemySpawner.OnEnemyDestroy.Invoke();
                LevelManager.main.AddGold(goldDrop);
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }

    public void ApplyDot(int totalDamage, float duration)
    {
        float ticks = duration / dotInterval;
        dotDamagePerTick = totalDamage / ticks; // Damage per tick
        dotDuration = duration;
        dotTimer = dotInterval; // Start ticking immediately
    }

    public void ApplyDebuff(float multiplier, float duration)
    {
        damageMultiplier = multiplier;
        debuffDuration = duration;
    }
}
