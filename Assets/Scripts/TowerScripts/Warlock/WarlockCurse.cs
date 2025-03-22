	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

public class WarlockCurseScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float curseSpeed = 5f;        // Speed of the curse projectile
    [SerializeField] private float damageMultiplier = 1.5f; // Damage increase factor
    [SerializeField] private float debuffDuration = 2f;     // Duration of the debuff

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }       

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * curseSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other == null) return;

        Health targetHealth = other.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.ApplyDebuff(damageMultiplier, debuffDuration);
        }

        Destroy(gameObject); 
    }
}       