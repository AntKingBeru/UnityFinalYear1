using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hp;
    
    private TowerHealthManager health;
    
    public void UpdateHealthBar(float currentHp, float maxHp)
    {
        hp.value = currentHp / maxHp;
    }
    
    void Start()
    {
        health = gameObject.GetComponent<TowerHealthManager>();
        UpdateHealthBar(health.GetHealth(), health._hitPointsMax);
    }
    
    void Update()
    {
        UpdateHealthBar(health.GetHealth(), health._hitPointsMax);
    }
}
