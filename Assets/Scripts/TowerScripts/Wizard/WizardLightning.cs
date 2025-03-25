using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLightningScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    [SerializeField] private float lightningSpeed = 5f;
    [SerializeField] private int lightningDamage = 10;
    [SerializeField] private float jumpRange = 3f; // Max distance to find next target
    [SerializeField] private int maxJumps = 2; // Max additional targets after first hit

    private Transform currentTarget;
    private int jumpsRemaining;
    private List<Transform> hitTargets = new List<Transform>(); // Track hit targets to avoid repeats

    public void SetTarget(Transform _target)
    {
        currentTarget = _target;
        jumpsRemaining = maxJumps;
        hitTargets.Clear();
        if (currentTarget != null)
        {
            hitTargets.Add(currentTarget); // Add initial target to hit list
        }
    }

    private void FixedUpdate()
    {
        if (!currentTarget) return;

        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.velocity = direction * lightningSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other == null || other.gameObject != currentTarget.gameObject) return;

        
        Health targetHealth = other.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(lightningDamage);
        }

        
        if (jumpsRemaining > 0)
        {
            Transform nextTarget = FindClosestEnemy();
            if (nextTarget != null)
            {
                currentTarget = nextTarget;
                hitTargets.Add(nextTarget);
                jumpsRemaining--;
                return; 
            }
        }

        
        Destroy(gameObject);
    }

    private Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = jumpRange + 1; 

        
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, jumpRange);
        foreach (Collider2D obj in nearbyObjects)
        {
            
            Health enemyHealth = obj.GetComponent<Health>();
            if (enemyHealth != null && !hitTargets.Contains(obj.transform))
            {
                float distance = Vector2.Distance(transform.position, obj.transform.position);
                if (distance <= jumpRange && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = obj.transform;
                }
            }
        }

        return closestEnemy;
    }
}