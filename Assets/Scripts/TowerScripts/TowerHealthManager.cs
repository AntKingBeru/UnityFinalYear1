using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TowerHealthManager : MonoBehaviour
{
	[Header("Attributes")]
	[SerializeField] private float hitPoints;
    [SerializeField] private float hitPoints2;
    [SerializeField] private float hitPoints3;
    [SerializeField] private float hitPoints4;
	[SerializeField] private Slider hp;

    public float _hitPointsMax;
    private bool _isDead;
    private bool _isDestroyed;
    private float _deadTimer;

    //Initializing the maximum HP to level 1 (base HP)
    private void Start()
    {
	    _hitPointsMax = hitPoints;
		UpdateHealthBar(hitPoints, _hitPointsMax);
    }

	public void UpdateHealthBar(float currentHp, float maxHp)
    {
        hp.value = currentHp / maxHp;
    }

    //Checking in the tower's health is 0 to start a timer for tower death. if healed during the process, reset timer
    private void Update()
    {
		UpdateHealthBar(hitPoints, _hitPointsMax);
	    if (_isDead)
	    {
		    _deadTimer += Time.deltaTime;
		    if (_deadTimer > 60)
			    Destroy(gameObject);
	    }
	    else
	    {
		    _deadTimer = 0;
	    }
    }

    //Taking damage, public so the mobs can access it when needed
    public void TakeDamage(float dmg)
    {
		Debug.Log("Ouch");
	    if (dmg > hitPoints)
	    {
		    hitPoints = 0;
		    _isDead = true;
	    }
	    else
	    {
		    hitPoints -= dmg;
	    }
		UpdateHealthBar(hitPoints, _hitPointsMax);
    }

    //Healing the tower, public so the Cleric tower can access it when needed
    public void Heal(float heal)
    {
	    if (heal + hitPoints > _hitPointsMax)
		    hitPoints = _hitPointsMax;
	    else
	    {
		    hitPoints += heal;
	    }
    }

    //Upgrading health status by overall tower level, public so upgrade manager can access it when needed
    public void SetHealth(int level)
    {
	    switch (level)
	    {
		    case 2:
		    {
			    hitPoints = hitPoints2;
			    _hitPointsMax = hitPoints;
			    break;
		    }
		    case 3:
		    {
			    hitPoints = hitPoints3;
			    _hitPointsMax = hitPoints;
			    break;
		    }
		    case 4:
		    {
			    hitPoints = hitPoints4;
			    _hitPointsMax = hitPoints;
			    break;
		    }
	    }
    }

	public float GetHealth()
	{
		return hitPoints;
	}
}